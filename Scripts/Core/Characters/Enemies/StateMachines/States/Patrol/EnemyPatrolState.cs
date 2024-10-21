using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Movers;

namespace Core.Characters.Enemies.States.Patrol
{
  public class EnemyPatrolState : State
  {
    private readonly IMover _mover;
    private readonly EnemyRoutePointsManager _points;
    private readonly EnemyAnimatorProvider _animatorProvider;
    private readonly EnemyConfig _config;
    private readonly EnemyHealer _healer;
    private readonly EnemyInstaller _enemyInstaller;

    public EnemyPatrolState(List<Transition> transitions, IMover mover,
      EnemyAnimatorProvider animatorProvider, EnemyConfig config,
      EnemyRoutePointsManager points, EnemyHealer healer, EnemyInstaller enemyInstaller)
      : base(transitions)
    {
      _mover = mover;
      _animatorProvider = animatorProvider;
      _config = config;
      _points = points;
      _healer = healer;
      _enemyInstaller = enemyInstaller;
    }

    public override void Enter()
    {
      _animatorProvider.Instance.PlayWalk();

      if (_enemyInstaller.RandomPatroling)
        _points.SetRandomRoute();
      else 
        _points.SetNextRoute();

      _mover.SetDestination(_points.Current().position, _config.MoveSpeed);
    }

    protected override void OnTick()
    {
      _healer.Heal();
    }

    public override void Exit()
    {
      _mover.Stop();
      _animatorProvider.Instance.StopWalk();
      _healer.Heal();
    }
  }
}