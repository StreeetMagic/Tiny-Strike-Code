using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Movers;
using Meta.Stats;
using UnityEngine;

namespace Core.Characters.Players.States
{
  public class PlayerMoveState : State
  {
    private readonly PlayerInputHandler _inputHandler;
    private readonly PlayerRotator _rotator;
    private readonly IMover _mover;
    private readonly PlayerAnimatorController _animator;
    private readonly PlayerWeaponMagazineReloader _playerWeaponMagazineReloader;
    private readonly PlayerWeaponAmmo _playerWeaponAmmo;
    private readonly PlayerStatsProvider _playerStatsProvider;
    private readonly Transform _transform;

    public PlayerMoveState(List<Transition> transitions, PlayerInputHandler inputHandler,
      PlayerRotator rotator, PlayerAnimatorController animator, PlayerWeaponMagazineReloader playerWeaponMagazineReloader,
      PlayerWeaponAmmo playerWeaponAmmo, IMover mover, PlayerStatsProvider playerStatsProvider, Transform transform) : base(transitions)
    {
      _inputHandler = inputHandler;
      _rotator = rotator;
      _animator = animator;
      _playerWeaponMagazineReloader = playerWeaponMagazineReloader;
      _playerWeaponAmmo = playerWeaponAmmo;
      _mover = mover;
      _playerStatsProvider = playerStatsProvider;
      _transform = transform;
    }

    public override void Enter()
    {
      _animator.PlayRunAnimation();
    }

    protected override void OnTick()
    {
      Vector3 direction = _inputHandler.GetDirection();
      float magnitude = Mathf.Clamp(direction.magnitude, 0.5f, 1.0f);
      direction = direction.normalized * magnitude;

      float moveSpeed = _playerStatsProvider.GetStat(StatId.MoveSpeed);
      Vector3 newPosition = _transform.position + direction;

      _mover.SetDestination(newPosition, moveSpeed);

      _rotator.RotateTowardsDirection(direction);

      if (_playerWeaponMagazineReloader.IsActive)
      {
        _playerWeaponMagazineReloader.Tick();
      }
      else if (_playerWeaponAmmo.IsAboveHalf() == false)
      {
        _playerWeaponMagazineReloader.Activate();
        _playerWeaponMagazineReloader.Tick();
      }
    }

    public override void Exit()
    {
      _animator.Stop();
    }
  }
}