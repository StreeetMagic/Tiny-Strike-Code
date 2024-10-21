using AudioServices;
using AudioServices.Sounds;
using Core;
using Core.AimObstacles;
using Core.Characters.Enemies;
using Core.Spawners.Enemies;
using HeadsUpDisplays.BackpackBars;
using LevelDesign.Maps;
using UnityAssetsTools.ParticleImage.Runtime;
using UnityEngine;
using VisualEffects.ParticleImages;
using Zenject;

namespace HeadsUpDisplays.MoneyAttractions
{
  public class ExpirienceCollection : MonoBehaviour
  {
    public Transform Target;
    public ParticleImage ParticleImage;
    public BackpackBarBounceEffect BounceEffect;

    private Camera _camera;

    [Inject] private EnemySpawnerProvider _enemySpawnerFactory;
    [Inject] private ParticleImageFactory _particleImageFactory;
    [Inject] private MapProvider _mapProvider;
    [Inject] private AudioService _audioService;

    private void Awake()
    {
      ParticleImage.gameObject.SetActive(false);
      _camera = Camera.main;
    }

    private void Start()
    {
      foreach (EnemySpawner spawner in _enemySpawnerFactory.Spawners)
        spawner.EnemyDied += OnEnemyDied;
      
      foreach (AimObstacle obstacle in _mapProvider.Map.AimObstacles)
        obstacle.GetComponent<IHealth>().Died += OnObstacleDestroed;
    }

    private void OnObstacleDestroed(IHealth health, int expirience, float corpseRemoveDelay)
    {
      if (expirience == 0)
        return;
      
      Vector3 position = _camera.WorldToScreenPoint(health.transform.position);
      
      // ReSharper disable once UnusedVariable
      ParticleImage particleImage = PlayerExpParticle(position);
      
      particleImage.onParticleFinish.AddListener(PlayBarParticle);
    }

    private void OnEnemyDied(EnemyHealth enemyHealth, EnemyConfig config)
    {
      if (config.Expirience == 0)
        return;
      
      Vector3 position = _camera.WorldToScreenPoint(enemyHealth.transform.position);

      // ReSharper disable once UnusedVariable
      ParticleImage particleImage = PlayerExpParticle(position);

      particleImage.onParticleFinish.AddListener(PlayBarParticle);
    }

    private ParticleImage PlayerExpParticle(Vector3 position)
    {
      _audioService.Play(SoundId.MoneyFlyStart);
      ParticleImage playerMoneyParticle = _particleImageFactory.Create(ParticleImageId.ExpCollection1, position, transform, Target);

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