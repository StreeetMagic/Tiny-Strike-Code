using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Die
{
  public class EnemyDieState : State
  {
    private readonly EnemyAnimatorProvider _animatorProvider;
    private readonly EnemyConfig _config;

    public EnemyDieState(List<Transition> transitions, EnemyAnimatorProvider animatorProvider, EnemyConfig config) : base(transitions)
    {
      _animatorProvider = animatorProvider;
      _config = config;
    }

    public override void Enter()
    {
      if (_config.HasDeathAnimation == false)
        return;

      _animatorProvider.Instance.PlayDeathAnimation();
    }

    protected override void OnTick()
    {
    }

    public override void Exit()
    {
    }
  }
}