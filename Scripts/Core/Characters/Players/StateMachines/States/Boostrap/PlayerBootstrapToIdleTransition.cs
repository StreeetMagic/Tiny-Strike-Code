using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerBootstrapToIdleTransition : Transition
  {
    public override void Tick()
    {
      Enter<PlayerIdleState>();
    }
  }
}