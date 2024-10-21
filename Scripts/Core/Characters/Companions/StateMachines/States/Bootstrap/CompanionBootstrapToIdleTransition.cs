using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Companions.States
{
  public class CompanionBootstrapToIdleTransition : Transition
  {
    public override void Tick()
    {
      Enter<CompanionIdleState>();
    }
  }
}