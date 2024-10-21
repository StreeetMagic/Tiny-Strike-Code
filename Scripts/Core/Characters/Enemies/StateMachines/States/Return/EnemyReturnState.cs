using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Movers;
using Core.Spawners.Enemies;

namespace Core.Characters.Enemies.States.Return
{
  public class EnemyReturnState : State
  {
    private readonly IMover _mover;
    private readonly EnemySpawner _spawner;
    private readonly EnemyConfig _config;
    private readonly EnemyAnimatorProvider _animatorProvider;
    private readonly EnemyHealer _enemyHealer;

    public EnemyReturnState(List<Transition> transitions, IMover mover, EnemySpawner spawner,
      EnemyAnimatorProvider animatorProvider, EnemyConfig config, EnemyHealer enemyHealer) : base(transitions)
    {
      _mover = mover;
      _spawner = spawner;
      _animatorProvider = animatorProvider;
      _config = config;
      _enemyHealer = enemyHealer;
    }

    public override void Enter()
    {
      _animatorProvider.Instance.PlayRun();
    }

    protected override void OnTick()
    {
      _mover.SetDestination(_spawner.Transform.position, _config.RunSpeed);
      _enemyHealer.Heal();
    }

    public override void Exit()
    {
      _mover.Stop();
      _animatorProvider.Instance.StopRun();
    }
  }
}