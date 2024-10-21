using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Companions.States
{
  public class CompanionIdleState : State
  {
    public CompanionIdleState(List<Transition> transitions) : base(transitions)
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