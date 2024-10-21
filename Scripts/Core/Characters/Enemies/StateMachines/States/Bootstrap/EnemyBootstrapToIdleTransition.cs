using Core.Characters.Enemies.States.Idle;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Bootstrap
{
  public class EnemyBootstrapToIdleTransition : Transition
  {
    public override void Tick()
    {
      Enter<EnemyIdleState>();
    }
  }
}