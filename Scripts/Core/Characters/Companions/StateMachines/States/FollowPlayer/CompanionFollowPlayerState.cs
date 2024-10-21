using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Movers;

namespace Core.Characters.Companions.States
{
  public class CompanionFollowPlayerState : State
  {
    private readonly IMover _mover;
    private readonly Companion _companion;

    public CompanionFollowPlayerState(List<Transition> transitions, IMover mover, Companion companion) : base(transitions)
    {
      _mover = mover;
      _companion = companion;
    }

    public override void Enter()
    {
    }

    protected override void OnTick()
    {
      _mover.SetDestination(_companion.Installer.TransformContainer.Transform.position, _companion.Installer.Config.MoveSpeed);
    }

    public override void Exit()
    {
    }
  }
}