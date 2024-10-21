using System;
using System.Collections;
using System.Collections.Generic;
using ConfigProviders;
using Core.Characters.Enemies;
using Core.LootSlots;
using Core.PickUpTreasures;
using CoroutineRunners;
using LevelDesign.EnemySpawnMarkers;
using Meta;
using RandomServices;
using UnityEngine;
using Utilities;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core.Spawners.Enemies
{
  public class EnemySpawner : ITickable
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly QuestCompleter _questCompleter;
    private readonly LootSlotFactory _lootSlotFactory;
    private readonly Enemy.Factory _enemyFactory;
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly PickUpTreasureSpawner _pickUpTreasureSpawner;
    private readonly RandomService _randomService;
    private readonly SimpleQuestStorage _simpleQuestStorage;
    private readonly CompositeQuestStorage _compositeQuestStorage;

    private readonly List<Transform> _spawnPoints;
    private readonly EnemySpawnerMarker _myMarker;
    private readonly Queue<IHealth> _enableQueue = new();
    private readonly List<CoroutineDecorator> _coroutineDecorators = new();

    private bool _isRespawning;
    private bool _spawned;

    public EnemySpawner
    (
      ICoroutineRunner coroutineRunner,
      QuestCompleter questCompleter,
      LootSlotFactory lootSlotFactory,
      Enemy.Factory enemyFactory,
      BalanceConfigProvider balanceConfigProvider,
      PickUpTreasureSpawner pickUpTreasureSpawner,
      RandomService randomService,
      SimpleQuestStorage simpleQuestStorage,
      //
      EnemySpawnerMarker myMarker,
      List<Transform> spawnPoints,
      CompositeQuestStorage compositeQuestStorage)
    {
      _coroutineRunner = coroutineRunner;
      _questCompleter = questCompleter;
      _lootSlotFactory = lootSlotFactory;
      _enemyFactory = enemyFactory;
      _balanceConfigProvider = balanceConfigProvider;
      _pickUpTreasureSpawner = pickUpTreasureSpawner;
      _randomService = randomService;
      _simpleQuestStorage = simpleQuestStorage;

      _myMarker = myMarker;
      _spawnPoints = spawnPoints;
      _compositeQuestStorage = compositeQuestStorage;
    }

    public event Action<EnemyHealth, EnemyConfig> EnemyDied;

    public bool Activated { get; private set; }
    public List<Enemy> Enemies { get; } = new();
    public Transform Transform => _myMarker.transform;

    public void Activate(int count)
    {
      if (_myMarker.SpawnerType != EnemySpawnerType.FromStart)
        return;

      Spawn(count);
      Activated = true;
    }

    public void DeSpawnAll()
    {
      foreach (Enemy enemy in Enemies)
        Object.Destroy(enemy.gameObject);

      Enemies.Clear();
    }

    public void Tick()
    {
      if (_spawned)
        return;

      if (_myMarker.SpawnerType == EnemySpawnerType.FromStart)
        return;

      OnOtherSpawnerCleared();
      OnSimpleQuestActivated();
      OnCompositeQuestActivated();
    }

    private void OnOtherSpawnerCleared()
    {
      if (_myMarker.SpawnerType != EnemySpawnerType.OnOtherSpawnerCleared)
        return;

      EnemySpawner observableSpawner = _myMarker.SpawnerToClear.Spawner;

      if (observableSpawner == null)
        return;

      if (!observableSpawner.Activated)
        return;

      if (observableSpawner.Enemies.Count != 0)
        return;

      _spawned = true;
      Activated = true;
      Spawn(_myMarker.Count);
    }

    private void OnSimpleQuestActivated()
    {
      if (_myMarker.SpawnerType != EnemySpawnerType.OnSimpleQuestActivated)
        return;

      SimpleQuestId simpleQuestId = _myMarker.SpawnerToClear.SimpleQuestToActivate;

      if (_simpleQuestStorage.Get(simpleQuestId).State.Value == QuestState.UnActivated)
        return;

      _spawned = true;
      Activated = true;
      Spawn(_myMarker.Count);
    }

    private void OnCompositeQuestActivated()
    {
      if (_myMarker.SpawnerType != EnemySpawnerType.OnCompositeQuestActivated)
        return;

      CompositeQuestId compositeQuestId = _myMarker.SpawnerToClear.CompositeQuestToActivate;

      if (_compositeQuestStorage.Get(compositeQuestId).State.Value == QuestState.UnActivated)
        return;

      _spawned = true;
      Activated = true;
      Spawn(_myMarker.Count);
    }

    private void Spawn(int count)
    {
      if (count <= 0)
        throw new ArgumentOutOfRangeException(nameof(count));

      for (int i = 0; i < count; i++)
      {
        Enemy enemy = CreateEnemy();

        Enemies.Add(enemy);

        enemy.Installer.Health.Died += OnEnemyDied;
      }
    }

    private void OnEnemyDied(IHealth health, int expirience, float corpseRemoveDelay)
    {
      _coroutineRunner.StartCoroutine(EnableLater(health, corpseRemoveDelay));

      health.Died -= OnEnemyDied;

      EnemyConfig config = _balanceConfigProvider.Enemies[_myMarker.EnemyId];

      _questCompleter.OnEnemyKilled(config.Id);

      EnemyDied?.Invoke(health as EnemyHealth, config);
      var enemy = health.GetComponent<Enemy>();

      if (_myMarker.PickUpTreasure != PickUpTreasureId.Unknown)
      {
        float chance = _randomService.GetRandomFloat(1);

        if (chance > _myMarker.PickUpTreasureDropChance)
          return;

        _pickUpTreasureSpawner.Spawn(_myMarker.PickUpTreasure, enemy.transform.position, _myMarker.PickUpTreasureDestroyAfterTime, _myMarker.PickUpTreasureDestroyTimer);
      }
    }

    private IEnumerator EnableLater(IHealth health, float delay)
    {
      yield return new WaitForSeconds(delay);
      
      health.transform.gameObject.SetActive(false);
      health.Died -= OnEnemyDied;
      
      if (!_myMarker.Respawns)
         yield break;

      _enableQueue.Enqueue(health);

      if (_isRespawning)
        yield break; 

      var coroutineDecorator = new CoroutineDecorator(_coroutineRunner, WaitAndEnable);
      _coroutineDecorators.Add(coroutineDecorator);
      coroutineDecorator.Start(OnCoroutineComplete);
    }

    private IEnumerator WaitAndEnable()
    {
      while (_enableQueue.Count > 0)
      {
        _isRespawning = true;
        yield return new WaitForSeconds(_myMarker.RespawnTime);

        Enable();
      }

      _isRespawning = false;
    }

    private void OnCoroutineComplete()
    {
      _coroutineDecorators.RemoveAll(c => !c.IsRunning);
    }

    private void Enable()
    {
      IHealth health = _enableQueue.Dequeue();
      health.transform.gameObject.SetActive(true);
      health.Died += OnEnemyDied;
    }

    /// <summary>
    /// Это должно быть в фабрике, а не здесь, в спаунере
    /// </summary>
    private Enemy CreateEnemy()
    {
      int spawnPointNumber = _myMarker.RandomPatroling
        ? Random.Range(0, _spawnPoints.Count - 1)
        : 0;

      EnemyConfig enemyConfig = _balanceConfigProvider.Enemies[_myMarker.EnemyId];
      Enemy enemy = _enemyFactory.Create(enemyConfig, _spawnPoints, this, _myMarker.RandomPatroling);

      enemy.transform.position = _spawnPoints[spawnPointNumber].position;
      enemy.transform.rotation = _spawnPoints[spawnPointNumber].rotation;
      enemy.Installer.NavMeshAgent.enabled = true;
      enemy.transform.SetParent(_myMarker.transform);

      if (enemyConfig.ShowLoot)
      {
        Transform enemyLootSlotsContainer = enemy.GetComponentInChildren<LootSlotsContainer>().transform;
        _lootSlotFactory.Create(enemyLootSlotsContainer, enemyConfig.LootDrops);
      }

      return enemy;
    }
  }
}