using System;
using System.Collections.Generic;
using AudioServices;
using ConfigProviders;
using Core.PickUpTreasures;
using Meta;
using Meta.BackpackStorages;
using Meta.Currencies;
using Meta.Loots;
using UnityEngine;
using UnityEngine.AI;
using Utilities;
using Zenject;
using ZenjectFactories.SceneContext;

namespace Core.AimObstacles
{
  public class AimObstacleHealth : MonoBehaviour, IHealth
  {
    [Inject] private AimObstacle _obstacle;
    [Inject] private AudioService _audioService;
    [Inject] private ArtConfigProvider _artConfigProvider;
    [Inject] private NavMeshObstacle _navMeshObstacle;
    [Inject] private QuestCompleter _questCompleter;
    [Inject] private BackpackStorage _backpackStorage;
    [Inject] private PickUpTreasureSpawner _pickUpTreasureSpawner;
    
    [Inject] private HubZenjectFactory _hubZenjectFactory;
    [Inject] private DevConfigProvider _devConfigProvider;

    public event Action<IHealth, int, float> Died;
    public event Action<float> Damaged;

    public ReactiveProperty<float> Current { get; } = new();

    public float Initial => _obstacle.InitialHealth;
    public bool IsFull => Current.Value >= Initial;
    public bool IsDead { get; private set; }

    private void Start()
    {
      Current.Value = Initial;
    }

    public void TakeDamage(float damage)
    {
      if (damage <= 0)
        throw new ArgumentOutOfRangeException(nameof(damage));

      if (IsLastHit(damage) == false)
        _audioService.Play(_artConfigProvider.AimObstacles[_obstacle.Id].HitSound);
      
      // var go = _hubZenjectFactory.InstantiatePrefab(_devConfigProvider.GetPrefab(PrefabId.HitPopupNumber), transform.position, Quaternion.identity, null);
      // go.GetComponent<DamageNumberMesh>().number = damage;
     
      Current.Value -= damage;
      Damaged?.Invoke(Current.Value);

      if (Current.Value <= 0)
        Die();
    }

    public void Hit()
    {
    }

    public void NotifyOtherEnemies()
    {
    }

    private bool IsLastHit(float damage)
    {
      return damage >= Current.Value;
    }

    private void Die()
    {
      if (IsDead)
        return;

      IsDead = true;

      _navMeshObstacle.enabled = false;
      _audioService.Play(_artConfigProvider.AimObstacles[_obstacle.Id].DestroySound);

      AddLoot();

      if (_obstacle.IsQuestTarget)
        _questCompleter.OnAimObstacleDestroyed(_obstacle.Id);

      if (_obstacle.PickUpTreasure != PickUpTreasureId.Unknown)
        _pickUpTreasureSpawner.Spawn(_obstacle.PickUpTreasure, transform.position, _obstacle.PickUpTreasureDestroyAfterTime, _obstacle.PickUpTreasureDestroyTimer);

      Died?.Invoke(this, _obstacle.Expirience, _obstacle.CorpseRemoveDelay);
    }

    private void AddLoot()
    {
      List<LootDrop> lootDrops = new();

      foreach (var lootDrop in _obstacle.LootDrops)
      {
        if (lootDrop.Id != CurrencyId.Money)
        {
          lootDrops.Add(lootDrop);
        }
      }

      if (_obstacle.LootDrops.Count == 0)
        return;

      _backpackStorage.AddLoot(lootDrops);
    }
  }
}