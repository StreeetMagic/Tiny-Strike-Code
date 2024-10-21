using ConfigProviders;
using Core.Characters.Enemies;
using Core.Characters.Players;
using DevConfigs;
using TimeServices;
using UnityEngine;
using VisualEffects;
using Zenject;

namespace Core.Projectiles
{
  public class EnemyProjectile : MonoBehaviour
  {
    [SerializeField] private LayerMask _layerMask;

    [Inject] private VisualEffectFactory _visualEffectFactory;
    [Inject] private EnemyVisualsProvider _artConfigs;
    [Inject] private BalanceConfigProvider _balanceConfigProvider;
    [Inject] private TimeService _timeService;

    private ProjectileMover _projectileMover;
    private int _count;
    private float _lifeTime;

    [Inject] public EnemyConfig EnemyConfig { get; }
    [Inject] public bool IsPhased { get; }

    private void Start()
    {
      _projectileMover = new ProjectileMover(EnemyConfig.BulletSpeed);

      CheckInitialCollision();
    }

    private void Update()
    {
      LifeTime();

      if (_projectileMover.MoveProjectile(transform, _layerMask, out RaycastHit hit))
        return;

      if (hit.collider.TryGetComponent(out PlayerTargetTrigger player))
      {
        if (_count == 0)
        {
          _count++;
          player.TakeDamage(EnemyConfigBulletDamage());
        }
      }

      ImpactEffect(hit.point);

      transform.position = hit.point;

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
      if (!other.TryGetComponent(out PlayerTargetTrigger player))
        return;

      if (_count == 0)
      {
        player.TakeDamage(EnemyConfig.BulletDamage);
        _count++;
      }

      ImpactEffect(transform.position);
      Destroy(gameObject);
    }

    private void LifeTime()
    {
      if (_lifeTime >= DevConfig.ProjectileLifeTime | _timeService.IsPaused)
        Destroy(gameObject);
      else
        _lifeTime += Time.deltaTime;
    }

    private void ImpactEffect(Vector3 position)
    {
      VisualEffectId id = _artConfigs.Impact(EnemyConfig.Id);
      _visualEffectFactory.CreateAndDestroy(id, position, transform.rotation);
    }

    private float EnemyConfigBulletDamage()
    {
      if (IsPhased)
      {
        return EnemyConfig.BulletDamage * EnemyConfig.DamageMultiplier;
      }
      
      return EnemyConfig.BulletDamage;
    }
  }
}