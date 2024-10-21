using System.Collections.Generic;
using AudioServices;
using AudioServices.Sounds;
using Core;
using Core.AimObstacles;
using Core.Characters.Enemies;
using Core.Spawners.Enemies;
using HeadsUpDisplays.BackpackBars;
using LevelDesign.Maps;
using Meta.BackpackStorages;
using Meta.Currencies;
using Meta.Loots;
using UnityAssetsTools.ParticleImage.Runtime;
using UnityEngine;
using VisualEffects.ParticleImages;
using Zenject;

namespace HeadsUpDisplays.MoneyAttractions
{
  public class MoneyCollection : MonoBehaviour
  {
    public Transform Target;
    public ParticleImage ParticleImage;
    public BackpackBarBounceEffect BounceEffect;

    // ReSharper disable once InconsistentNaming
    public Camera _camera;

    [Inject]
    private EnemySpawnerProvider _enemySpawnerFactory;

    [Inject]
    private ParticleImageFactory _particleImageFactory;

    [Inject]
    private AudioService _audioService;

    [Inject]
    private MapProvider _mapProvider;

    [Inject]
    private BackpackStorage _backpackStorage;

    [Inject]
    private HeadsUpDisplayProvider _headsUpDisplayProvider;

    private void Awake()
    {
      ParticleImage.gameObject.SetActive(false);

      _camera = Camera.main;
    }

    private void Start()
    {
      foreach (EnemySpawner spawner in _enemySpawnerFactory.Spawners)
        spawner.EnemyDied += OnEnemyDied;

      foreach (AimObstacle aimObstacle in _mapProvider.Map.AimObstacles)
        aimObstacle.GetComponent<AimObstacleHealth>().Died += OnObstacleDestroyed;
    }

    private void OnEnemyDied(EnemyHealth health, EnemyConfig config)
    {
      if (!CanDropMoney(config.LootDrops))
        return;

      if (_backpackStorage.IsFull())
        return;

      PlayMoneyParticle(health.transform.position, config.LootDrops);
      _headsUpDisplayProvider.BackpackBar.Show();
    }

    private void OnObstacleDestroyed(IHealth health, int arg2, float arg3)
    {
      List<LootDrop> lootDrops = health.GetComponent<AimObstacle>().LootDrops;

      if (!CanDropMoney(lootDrops))
        return;

      if (_backpackStorage.IsFull())
        return;

      PlayMoneyParticle(health.transform.position, lootDrops);
      _headsUpDisplayProvider.BackpackBar.Show();
    }

    private void PlayMoneyParticle(Vector3 dropperPosition, List<LootDrop> lootDrops)
    {
      PlayMoneyParticle(_camera.WorldToScreenPoint(dropperPosition))
        .onParticleFinish
        .AddListener(() =>
        {
          PlayBarParticle();
          _backpackStorage.AddLoot(lootDrops);
        });
    }

    private bool CanDropMoney(List<LootDrop> drops)
    {
      bool canDropMoney = false;

      foreach (var lootDrop in drops)
      {
        if (lootDrop.Id == CurrencyId.Money)
        {
          canDropMoney = true;
        }
      }

      return canDropMoney;
    }

    private ParticleImage PlayMoneyParticle(Vector3 position)
    {
      _audioService.Play(SoundId.MoneyFlyStart);

      ParticleImage playerMoneyParticle =
        _particleImageFactory.Create(ParticleImageId.MoneyOneParticle, position, transform, Target);

      Destroy(playerMoneyParticle.gameObject, 10f);

      return playerMoneyParticle;
    }

    private void PlayBarParticle()
    {
      _audioService.Play(SoundId.MoneyFlyFinish);
      ParticleImage.gameObject.SetActive(true);
      ParticleImage.Play();
      BounceEffect.ApplyBounceEffect();
    }
  }
}