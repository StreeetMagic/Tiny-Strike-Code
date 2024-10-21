using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerBootstrapState : State
  {
    public PlayerBootstrapState(List<Transition> transitions) : base(transitions)
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