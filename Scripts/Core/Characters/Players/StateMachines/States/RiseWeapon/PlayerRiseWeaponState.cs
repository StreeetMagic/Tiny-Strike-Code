using System.Collections.Generic;
using ConfigProviders;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerRiseWeaponState : State
  {
    private readonly WeaponRaiseTimer _weaponRaiser;
    private readonly PlayerAnimatorController _animator;
    private readonly PlayerRotator _rotator;
    private readonly PlayerTargetHolder _targetHolder;
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly PlayerProvider _playerProvider;

    public PlayerRiseWeaponState(List<Transition> transitions,
      WeaponRaiseTimer timer, PlayerAnimatorController playerAnimator, PlayerRotator rotator,
      PlayerTargetHolder targetHolder, BalanceConfigProvider balanceConfigProvider, PlayerProvider playerProvider)
      : base(transitions)
    {
      _weaponRaiser = timer;
      _animator = playerAnimator;
      _rotator = rotator;
      _targetHolder = targetHolder;
      _balanceConfigProvider = balanceConfigProvider;
      _playerProvider = playerProvider;
    }

    public override void Enter()
    {
      float time = _balanceConfigProvider.Weapons[(_playerProvider.Instance.WeaponIdProvider.CurrentId.Value)].RaiseTime;
      
      _animator.RaiseWeapon(time);
      _weaponRaiser.Set(time);
      _animator.OnStateShooting();
    }

    protected override void OnTick() 
    {
      if (_targetHolder.HasTarget)
        _rotator.RotateTowardsDirection(_targetHolder.LookDirectionToTarget());
    }

    public override void Exit()
    {
    }
  }
}