using System.Collections.Generic;
using Core.Characters.Enemies.States.Patrol;
using Core.Characters.FiniteStateMachines;
using UnityEngine;

namespace Core.Characters.Enemies.States.Idle
{
  public class EnemyIdleToPatrolTransition : Transition
  {
    private readonly EnemyIdleTimer _idleTimer;
    private readonly List<Transform> _routes;

    public EnemyIdleToPatrolTransition(EnemyIdleTimer idleTimer, List<Transform> routes)
    {
      _idleTimer = idleTimer;
      _routes = routes;
    }

    public override void Tick()
    {
      if (_routes.Count == 1)
        return;

      if (_idleTimer.IsCompleted)
      {
        _idleTimer.Set(1f);
        Enter<EnemyPatrolState>();
      }
      else
      {
        _idleTimer.Tick();
      }
    }
  }
}