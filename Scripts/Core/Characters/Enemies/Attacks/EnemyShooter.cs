using AudioServices;
using ConfigProviders;
using Core.Characters.Enemies.Phases;
using Core.Projectiles.Scripts;
using UnityEngine;
using Utilities;
using VisualEffects;

namespace Core.Characters.Enemies
{
  public class EnemyShooter
  {
    private readonly ProjectileFactory _projectileFactory;
    private readonly AudioService _audioService;
    private readonly EnemyConfig _enemyConfig;
    private readonly VisualEffectFactory _visualEffectFactory;
    private readonly EnemyVisualsProvider _enemyVisualsProvider;
    private readonly EnemyPhase _enemyPhase;

    public EnemyShooter(ProjectileFactory zenjectFactory, AudioService audioService, 
      EnemyConfig enemyConfig, VisualEffectFactory visualEffectFactory, EnemyVisualsProvider enemyVisualsProvider, EnemyPhase enemyPhase)
    {
      _projectileFactory = zenjectFactory;
      _audioService = audioService;
      _enemyConfig = enemyConfig;
      _visualEffectFactory = visualEffectFactory;
      _enemyVisualsProvider = enemyVisualsProvider;
      _enemyPhase = enemyPhase;
    }

    public void Shoot(Transform parent, Vector3 startPosition, Vector3 directionToTarget, EnemyConfig enemyConfig)
    {
      for (int i = 0; i < enemyConfig.BulletsPerShot; i++)
        _projectileFactory.CreateEnemyProjectile(parent, startPosition, directionToTarget.AddAngle(_enemyConfig.BulletSpreadAngle), enemyConfig, _enemyPhase.Passed);

      _visualEffectFactory.CreateAndDestroy(_enemyVisualsProvider.MuzzleFlash(enemyConfig.Id), startPosition, parent.rotation);
      
      _audioService.Play(_enemyVisualsProvider.AttackSound(enemyConfig.Id));
    }
  }
}