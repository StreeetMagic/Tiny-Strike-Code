using Core.Characters.Companions.Configs;
using DevConfigs;
using UnityEngine;
using VisualEffects;
using Zenject;

namespace Core.Projectiles
{
  public class CompanionProjectile : MonoBehaviour
  {
    [Inject] private CompanionConfig _config;

    [Inject] private VisualEffectFactory _visualEffectFactory;

    private int _count;
    private float _lifeTime;
    private ProjectileMover _projectileMover;

    private void Start()
    {
      _projectileMover = new ProjectileMover(_config.BulletSpeed);
      CheckInitialCollision();
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

    private void TryDamageTarget(RaycastHit hit)
    {
      if (!hit.collider.gameObject.TryGetComponent(out ITargetTrigger enemyTargetTrigger))
        return;

      if (_count != 0)
        return;

      _count++;

      enemyTargetTrigger.TakeDamage(_config.BulletDamage);
    }

    private bool Move(out RaycastHit hit)
    {
      return _projectileMover.MoveProjectile(transform, Physics.DefaultRaycastLayers, out hit);
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

      if (_count == 0)
      {
        enemyTargetTrigger.TakeDamage(_config.BulletDamage);
        _count++;
      }

      ImpactEffect();
      Destroy(gameObject);
    }

    private void ImpactEffect()
    {
      _visualEffectFactory.CreateAndDestroy(VisualEffectId.ExplosionStandardYellow, transform.position, Quaternion.identity);
    }
  }
}