using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Idle
{
  public class EnemyIdleState : State
  {
    private readonly EnemyIdleTimer _idleTimer;
    private readonly EnemyToPlayerRotator _rotator;
    private readonly EnemyConfig _enemyConfig;
    private readonly EnemyAnimatorProvider _enemyAnimatorProvider;
    
    public EnemyIdleState(List<Transition> transitions, EnemyIdleTimer idleTimer, 
      EnemyConfig enemyConfig, EnemyToPlayerRotator rotator, EnemyAnimatorProvider enemyAnimatorProvider) : base(transitions)
    {
      _idleTimer = idleTimer;
      _enemyConfig = enemyConfig;
      _rotator = rotator;
      _enemyAnimatorProvider = enemyAnimatorProvider;
    }

    public override void Enter()
    {
       _enemyAnimatorProvider.Instance.PlayIdle();
       _idleTimer.Set(_enemyConfig.IdleDuration);
    }

    protected override void OnTick()
    {
      _idleTimer.Tick();
      
      if (_enemyConfig.LookAtPlayer)
        _rotator.Rotate();
    }

    public override void Exit()
    {
    }
  }
}