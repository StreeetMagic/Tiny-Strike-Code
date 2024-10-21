using Core.Characters.Enemies.States.RaiseWeapon;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;
using UnityEngine;

namespace Core.Characters.Enemies.States.Chase
{
  public class EnemyChaseToRaiseWeaponTransition : Transition
  {
    private readonly EnemyConfig _config;
    private readonly Transform _transform;
    private readonly PlayerProvider _playerProvider;
    private readonly EnemyWeaponMagazine _magazine;
    private readonly EnemyGrenadeThrowerStatus _grenadeThrowerStatus;

    public EnemyChaseToRaiseWeaponTransition(EnemyConfig config, Transform transform,
      PlayerProvider playerProvider, EnemyWeaponMagazine magazine, EnemyGrenadeThrowerStatus grenadeThrowerStatus)
    {
      _config = config;
      _transform = transform;
      _playerProvider = playerProvider;
      _magazine = magazine;
      _grenadeThrowerStatus = grenadeThrowerStatus;
    }

    public override void Tick()
    {
      if (!_playerProvider.Instance)
        return;

      if (_playerProvider.Instance.Health.IsDead)
        return;

      if (_grenadeThrowerStatus.IsReady())
        return;

      if (_config.IsShooter == false)
        return;

      if (_magazine.IsEmpty)
        return;

      float distance = Vector3.Distance(_transform.position, _playerProvider.Instance.Transform.position);

      if (distance > _config.MeleeRange && distance < _config.ShootRange)
        Enter<EnemyRaiseWeaponState>();
    }
  }
}