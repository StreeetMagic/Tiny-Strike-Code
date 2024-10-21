using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerAttackState : State
  {
    private readonly PlayerWeaponAttacker _playerWeaponAttacker;

    public PlayerAttackState(List<Transition> transitions, PlayerWeaponAttacker playerWeaponAttacker)
      : base(transitions)
    {
      _playerWeaponAttacker = playerWeaponAttacker;
    }

    public override void Enter()
    {
      _playerWeaponAttacker.IsAttacking = true;
      _playerWeaponAttacker.ResetValues();
    }

    protected override void OnTick()
    {
      _playerWeaponAttacker.Tick();
    }

    public override void Exit()
    {
      _playerWeaponAttacker.IsAttacking = false;
    }
  }
}