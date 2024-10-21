using System;
using System.Collections.Generic;
using AudioServices;
using ConfigProviders;
using Core.CorpseRemovers;
using Core.Spawners.Enemies;
using Meta.BackpackStorages;
using Meta.Currencies;
using Meta.Loots;
using UnityEngine;
using Utilities;
using Zenject;
using ZenjectFactories.SceneContext;

namespace Core.Characters.Enemies
{
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    [Inject] private CorpseRemover _corpseRemover;
    [Inject] private HitStatus _hitStatus;
    [Inject] private EnemyConfig _config;
    [Inject] private EnemySpawner _spawner;
    [Inject] private EnemyAssistCall _assistCall;
    [Inject] private AudioService _audioService;
    [Inject] private ArtConfigProvider _artConfigProvider;
    [Inject] private BackpackStorage _backpackStorage;
    [Inject] private HubZenjectFactory _hubZenjectFactory;
    [Inject] private DevConfigProvider _devConfigProvider;

    public event Action<IHealth, int, float> Died;
    public event Action<float> Damaged;

    public ReactiveProperty<float> Current { get; } = new();

    public float Initial => _config.InitialHealth;
    public bool IsFull => Current.Value >= Initial;
    public bool IsDead { get; private set; }

    private void OnEnable()
    {
      Current.Value = Initial;
      IsDead = false;
    }

    public void TakeDamage(float damage)
    {
      if (damage <= 0)
        throw new ArgumentOutOfRangeException(nameof(damage));

      if (IsLastHit(damage) == false)
        _audioService.Play(_artConfigProvider.Enemies[_config.Id].DamageTakenSound);

      if (_config.IsImmortal)
        damage = 0;

      Current.Value -= damage;
      Damaged?.Invoke(Current.Value);

      NotifyOtherEnemies();

      if (Current.Value <= 0)
        Die();
    }

    public void NotifyOtherEnemies()
    {
      foreach (Enemy enemy in _spawner.Enemies)
        enemy.Installer.Health.Hit();

      _assistCall.Call();
    }

    public void Hit()
    {
      _hitStatus.IsHit = true;
    }

    private void Die()
    {
      if (IsDead)
        return;

      _audioService.Play(_artConfigProvider.Enemies[_config.Id].DieSound);
      AddLoot();
      IsDead = true;

      Died?.Invoke(this, _config.Expirience, _config.CorpseRemoveDelay);
    }

    private bool IsLastHit(float damage)
    {
      return damage >= Current.Value;
    }

    private void AddLoot()
    {
      List<LootDrop> lootDrops = new();

      foreach (var lootDropId in _config.LootDrops)
      {
        if (lootDropId.Id != CurrencyId.Money)
          _backpackStorage.AddLoot(lootDrops);
      }

      if (_config.LootDrops.Count == 0)
        return;

      _backpackStorage.AddLoot(lootDrops);
    }
  }
}