using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Companions.States.LowWeapon
{
  public class CompanionLowWeaponState : State
  {
    private readonly WeaponLowTimer _timer;
    private readonly Companion _companion;
    
    public CompanionLowWeaponState(List<Transition> transitions) : base(transitions)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    protected override void OnTick()
    {
    }
  }
}