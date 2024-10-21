using Core.Characters.Enemies.States.LowWeapon;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;
using UnityEngine;

namespace Core.Characters.Enemies.States.Shoot
{
  public class EnemyShootToLowWeaponTransition : Transition
  {
    private readonly PlayerProvider _playerProvider;
    private readonly Transform _transform;
    private readonly EnemyConfig _config;
    private readonly EnemyWeaponMagazine _magazine;
    private readonly EnemyGrenadeThrowerStatus _grenadeThrowerStatus;

    public EnemyShootToLowWeaponTransition(PlayerProvider playerProvider,
      Transform transform, EnemyConfig config, EnemyWeaponMagazine magazine, 
      EnemyGrenadeThrowerStatus grenadeThrowerStatus)
    {
      _playerProvider = playerProvider;
      _transform = transform;
      _config = config;
      _magazine = magazine;
      _grenadeThrowerStatus = grenadeThrowerStatus;
    }

    public override void Tick()
    {
      if (!_playerProvider.Instance)
      {
        Enter<EnemyLowWeaponState>();
        return;
      }
      
      if (_playerProvider.Instance.Health.IsDead)
      {
        Enter<EnemyLowWeaponState>();
        return;
      }
      
      if (_magazine.IsEmpty)
      {
        Enter<EnemyLowWeaponState>();
        return;
      }

      if (Vector3.Distance(_transform.position, _playerProvider.Instance.transform.position) < _config.MeleeRange)
      {
        Enter<EnemyLowWeaponState>();
        return;
      }

      if (Vector3.Distance(_transform.position, _playerProvider.Instance.transform.position) > _config.ShootRange)
      {
        Enter<EnemyLowWeaponState>();
        return;
      }

      if (_grenadeThrowerStatus.IsReady())
      {
        Enter<EnemyLowWeaponState>();
      }
    }
  }
}