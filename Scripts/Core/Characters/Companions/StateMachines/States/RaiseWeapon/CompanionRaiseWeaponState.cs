using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Companions.States.RaiseWeapon
{
  public class CompanionRaiseWeaponState : State
  {
     private readonly WeaponRaiseTimer _timer;
     private readonly Companion _companion;
    
    public CompanionRaiseWeaponState(List<Transition> transitions, WeaponRaiseTimer timer, Companion companion) : base(transitions)
    {
      _timer = timer;
      _companion = companion;
    }

    public override void Enter()
    {
      _timer.Set(_companion.Installer.Config.RaiseWeaponDuration);
    }

    protected override void OnTick()
    {
      _timer.Tick();
    }

    public override void Exit()
    {
    }
  }
}