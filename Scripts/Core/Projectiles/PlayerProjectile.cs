using System;
using ConfigProviders;
using Core.Characters.Players;
using Core.Weapons;
using DevConfigs;
using UnityEngine;
using VisualEffects;
using Zenject;

namespace Core.Projectiles
{
  public class PlayerProjectile : MonoBehaviour
  {
    [SerializeField] private LayerMask _layerMask;
    
    [Inject] private VisualEffectFactory _visualEffectFactory;
    [Inject] private BalanceConfigProvider _balanceConfigProvider;
    [Inject] private PlayerProvider _playerProvider;

    private ProjectileMover _projectileMover;
    private int _count;
    private float _lifeTime;
    private WeaponId _weaponTypeId;

    public void Init(WeaponId weaponTypeId)
    {
      _weaponTypeId = weaponTypeId;
      CheckInitialCollision();
    }

    private void Start()
    {
      _projectileMover = new ProjectileMover(MoveSpeed());
    }

    private void Update()
    {
      if (LifeTime() == false)
        return;

      if (Move(out RaycastHit hit))
        return;

      TryDamageTarget(hit);
      transform.position = hit.point;
      ImpactEffect();

      Destroy(gameObject);
    }

    private void CheckInitialCollision()
    {
      // ReSharper disable once Unity.PreferNonAllocApi
      Collider[] colliders = Physics.OverlapSphere(transform.position, DevConfig.ProjectileRadiusChecker);

      foreach (Collider other in colliders)
      {
        if (other.gameObject == gameObject)
          continue;

        ProcessInitialHit(other);
        break;
      }
    }

    private void ProcessInitialHit(Collider other)
    {
      if (!other.TryGetComponent(out ITargetTrigger enemyTargetTrigger))
        return;

      if (_count == 0 && _playerProvider.Instance)
      {
        enemyTargetTrigger.TakeDamage(_playerProvider.Instance.Damage.Get(_weaponTypeId));
        _count++;
      }

      ImpactEffect();
      Destroy(gameObject);
    }

    private bool LifeTime()
    {
      if (_lifeTime >= DevConfig.ProjectileLifeTime)
      {
        Destroy(gameObject);
        return false;
      }
      else
      {
        _lifeTime += Time.deltaTime;
        return true;
      }
    }

    private float MoveSpeed()
    {
      return _balanceConfigProvider.Weapons[(_weaponTypeId)].BulletSpeed;
    }

    private void TryDamageTarget(RaycastHit hit)
    {
      if (!hit.collider.gameObject.TryGetComponent(out ITargetTrigger enemyTargetTrigger))
        return;

      if (_count != 0)
        return;

      _count++;

      if (!_playerProvider.Instance)
        return;

      enemyTargetTrigger.TakeDamage(_playerProvider.Instance.Damage.Get(_weaponTypeId));
    }
    
    private bool Move(out RaycastHit hit)
    {
      return _projectileMover.MoveProjectile(transform, _layerMask, out hit);
    }

    private void ImpactEffect()
    {
      VisualEffectId bulletImpactId = _weaponTypeId
        switch
        {
          WeaponId.Unknown => throw new Exception("WeaponId is unknown"),
          WeaponId.DesertEagle => VisualEffectId.ExplosionSphereSmallYellow,
          WeaponId.Famas or WeaponId.Ak47 => VisualEffectId.ExplosionStandardYellow,
          WeaponId.Xm1014 => VisualEffectId.ExplosionSphereSmallYellow,
          _ => throw new ArgumentOutOfRangeException()
        };

      _visualEffectFactory.CreateAndDestroy(bulletImpactId, transform.position, Quaternion.identity);
    }
  }
}