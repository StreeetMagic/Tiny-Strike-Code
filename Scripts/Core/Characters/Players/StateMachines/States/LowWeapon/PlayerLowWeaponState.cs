using System.Collections.Generic;
using ConfigProviders;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerLowWeaponState : State
  {
    private readonly PlayerAnimatorController _playerAnimator;
    private readonly PlayerWeaponLowTimer _playerWeaponLowTimer;
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly PlayerWeaponIdProvider _playerWeaponIdProvider;

    public PlayerLowWeaponState(List<Transition> transitions, PlayerAnimatorController playerAnimator,
      PlayerWeaponLowTimer playerWeaponLowTimer, BalanceConfigProvider balanceConfigProvider, PlayerWeaponIdProvider playerWeaponIdProvider) : base(transitions)
    {
      _playerAnimator = playerAnimator;
      _playerWeaponLowTimer = playerWeaponLowTimer;
      _balanceConfigProvider = balanceConfigProvider;
      _playerWeaponIdProvider = playerWeaponIdProvider;
    }

    public override void Enter()
    {
      _playerWeaponLowTimer.Set(_balanceConfigProvider.Weapons[_playerWeaponIdProvider.CurrentId.Value].RaiseTime);
      _playerAnimator.OffStateShooting();
    }

    protected override void OnTick()
    {
    }

    public override void Exit()
    {
    }
  }
}