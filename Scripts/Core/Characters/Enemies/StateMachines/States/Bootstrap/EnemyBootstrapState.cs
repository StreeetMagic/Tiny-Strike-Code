using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Bootstrap
{
  public class EnemyBootstrapState : State
  {
    public EnemyBootstrapState(List<Transition> transitions) : base(transitions)
    {
    }

    public override void Enter()
    {
    }

    protected override void OnTick()
    {
    }

    public override void Exit()
    {
    }
  }
}