using System;
using ConfigProviders;
using Core.Bombs;
using Meta;
using Prefabs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using ZenjectFactories.SceneContext;

namespace LevelDesign
{
  public class BombSpawnMarker : MonoBehaviour
  {
    public float ExplosionRadius = 5f;

    [Inject]
    private SimpleQuestStorage _simpleQuestStorage;

    [Inject]
    private DevConfigProvider _configProvider;

    [Inject]
    private HubZenjectFactory _hubZenjectFactory;

    [field: SerializeField]
    public SimpleQuestId QuestId { get; private set; }

    [field: SerializeField]
    [field: Tooltip("Полное время в секундах")]
    [field: ValidateInput(nameof(IsPositive), "Значение должно быть положительным")]
    public float BombTimer { get; private set; }

    public float BombTimerLeft { get; private set; }
    public bool Spawned { get; private set; }
    public Bomb Bomb { get; private set; }

    private void Awake()
    {
      if (QuestId == SimpleQuestId.Unknown)
        Debug.LogWarning("BombSpawnMarker has unknown quest id");
    }

    private void OnEnable()
    {
      BombTimerLeft = BombTimer;
    }

    private void Update()
    {
      UpdateSpawn();
      TickBombTimer();
      TickDestroy();
    }

    private void TickBombTimer()
    {
      if (!Spawned)
      {
        BombTimerLeft = BombTimer;
        return;
      }      

      BombTimerLeft -= Time.deltaTime;

      if (BombTimerLeft <= 0)
        Explode();
    }

    private void Explode()
    {
      Bomb.Explode();
      RemoveBomb();
    }

    private void RemoveBomb()
    {
      Spawned = false;
      Destroy(Bomb.gameObject);
      Bomb = null;
      BombTimerLeft = BombTimer;
    }

    private void UpdateSpawn()
    {
      if (Spawned)
        return;

      if (_simpleQuestStorage.Get(QuestId).State.Value == QuestState.Activated)
      {
        Spawned = true;
        Bomb = _hubZenjectFactory.InstantiatePrefabForComponent(_configProvider.GetPrefabForComponent<Bomb>(PrefabId.Bomb), transform);
        Bomb.SpawnMarker = this;
        Bomb.gameObject.name = name;
      }
    }

    private void TickDestroy()
    {
      if (!Spawned)
        return;

      if (Bomb.IsDefused())
      {
        _simpleQuestStorage.Get(QuestId).CompletedQuantity.Value++;
        RemoveBomb();
      }
    }

    private bool IsPositive(float value)
    {
      return value > 0;
    }
  }
}