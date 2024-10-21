using System;
using ConfigProviders;
using Core.Characters.Companions.Configs;
using Core.Characters.Enemies;
using Core.Weapons;
using Prefabs;
using UnityEngine;
using VisualEffects;
using ZenjectFactories.SceneContext;

namespace Core.Projectiles.Scripts
{
  public class ProjectileFactory
  {
    private readonly HubZenjectFactory _zenjectFactory;
    private readonly VisualEffectFactory _visualEffectFactory;
    private readonly EnemyVisualsProvider _artConfigProvider;

    public ProjectileFactory(HubZenjectFactory zenjectFactory,
      VisualEffectFactory visualEffectFactory, EnemyVisualsProvider artConfigProvider)
    {
      _zenjectFactory = zenjectFactory;
      _visualEffectFactory = visualEffectFactory;
      _artConfigProvider = artConfigProvider;
    }

    public void CreatePlayerProjectile(Transform parent, Vector3 rotation, WeaponId weaponTypeId)
    {
      PlayerProjectile playerProjectile = _zenjectFactory.InstantiatePrefabForComponent<PlayerProjectile>(PrefabId.PlayerProjectile, parent.position, Quaternion.LookRotation(rotation), parent);
      playerProjectile.Init(weaponTypeId);
      VisualEffectId bulletEffectId;

      switch (weaponTypeId)
      {
        case WeaponId.DesertEagle:
        case WeaponId.Famas:
        case WeaponId.Ak47:
        case WeaponId.Xm1014:
          bulletEffectId = VisualEffectId.BulletYellow;
          break;

        default:
        case WeaponId.Unarmed:
        case WeaponId.Unknown:
        case WeaponId.Knife:
          throw new ArgumentOutOfRangeException();
      }

      GameObject bulletEffectObject = _visualEffectFactory.Create(bulletEffectId, playerProjectile.transform.position, playerProjectile.transform);
      bulletEffectObject.transform.SetParent(playerProjectile.transform, false);
      bulletEffectObject.transform.position = playerProjectile.transform.position;

      playerProjectile.transform.SetParent(null);
    }

    public void CreateEnemyProjectile(Transform parent, Vector3 position, Vector3 rotation, EnemyConfig enemyConfig, bool isPhased)
    {
      EnemyProjectile enemyProjectile = _zenjectFactory.InstantiatePrefabForComponent<EnemyProjectile>
      (
        PrefabId.EnemyProjectile,
        position,
        Quaternion.LookRotation(rotation),
        parent,
        new object[]
        {
          enemyConfig, isPhased
        }
      );

      enemyProjectile.transform.SetParent(null);
      CreateEnemyBulletEffect(enemyProjectile.transform, enemyConfig);
    }

    public void CreateCompanionProjectile(Transform parent, Vector3 startPosition, Vector3 addAngle, CompanionConfig companionConfig)
    {
      CompanionProjectile companionProjectile = _zenjectFactory.InstantiatePrefabForComponent<CompanionProjectile>
      (
        PrefabId.CompanionProjectile,
        startPosition,
        Quaternion.LookRotation(addAngle),
        parent,
        new object[]
        {
          companionConfig
        }
      );
      
      companionProjectile.transform.SetParent(null);
      CreateCompanionBulletEffect(companionProjectile.transform);
    }

    private void CreateEnemyBulletEffect(Transform parent, EnemyConfig enemyConfig)
    {
      VisualEffectId id = _artConfigProvider.Bullet(enemyConfig.Id);

      GameObject muzzleEffectObject = _visualEffectFactory.Create(id, parent.position, parent);

      muzzleEffectObject.transform.SetParent(parent);
    }

    private void CreateCompanionBulletEffect(Transform parent)
    {
      GameObject muzzleEffectObject = _visualEffectFactory.Create(VisualEffectId.BulletYellow, parent.position, parent);

      muzzleEffectObject.transform.SetParent(parent);
    }
  }
}