using Core.Characters.Enemies.States.Die;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.AnyStatesTransitions
{
  public class EnemyAnyStateToDie : Transition
  {
    private readonly IHealth _health;

    public EnemyAnyStateToDie(IHealth health)
    {
      _health = health;
    }

    public override void Tick()
    {
      if (_health.IsDead)
      {
        Enter<EnemyDieState>();
      }
    }
  }
}