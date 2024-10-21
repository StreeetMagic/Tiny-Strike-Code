using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;
using UnityEngine;

namespace Core.Characters.Companions.States.Shoot
{
  public class CompanionShootState : State
  {
    private readonly Companion _companion;
    private readonly CompanionToEnemyRotator _toEnemyRotator;
    private readonly PlayerProvider _playerProvider;
    private readonly CompanionWeaponMagazine _weaponMagazine;
    private readonly CompanionShooter _companionShooter;

    private float _shootTimeLeft;

    public CompanionShootState(List<Transition> transitions, Companion companion, PlayerProvider playerProvider,
      CompanionToEnemyRotator toEnemyRotator, CompanionWeaponMagazine weaponMagazine, CompanionShooter companionShooter) : base(transitions)
    {
      _companion = companion;
      _playerProvider = playerProvider;
      _toEnemyRotator = toEnemyRotator;
      _weaponMagazine = weaponMagazine;
      _companionShooter = companionShooter;
    }

    public override void Enter()
    {
      _shootTimeLeft = 1 / (float)_companion.Installer.Config.FireRate;
      _weaponMagazine.Reload();
    }

    protected override void OnTick()
    {
      if (!_playerProvider.Instance)
        return;

      if (_playerProvider.Instance.TargetHolder.CurrentTarget == null)
        return;

      _toEnemyRotator.Rotate();

      Vector3 targerPosition = _playerProvider.Instance.TargetHolder.CurrentTarget.TargetPoint.position;
      Vector3 companionPosition = _companion.Installer.ShootingPoint.position;
      Vector3 direction = targerPosition - companionPosition;

      _shootTimeLeft -= Time.deltaTime;

      if (_shootTimeLeft > 0)
        return;

      if (_weaponMagazine.TryGetBullet() == false)
        return;

      Transform parentTransform = _companion.Installer.ShootingPoint;
      _companionShooter.Shoot(parentTransform, parentTransform.position, direction, _companion.Installer.Config);

      _shootTimeLeft = 1 / (float)_companion.Installer.Config.FireRate;
    }

    public override void Exit()
    {
    }
  }
}