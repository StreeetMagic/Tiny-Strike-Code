using Core.Characters.Enemies.States.Idle;
using Core.Characters.FiniteStateMachines;
using Core.Spawners.Enemies;
using DevConfigs;
using UnityEngine;

namespace Core.Characters.Enemies.States.Return
{
  public class EnemyReturnToIdleTransition : Transition
  {
    private readonly Transform _transform;
    private readonly EnemySpawner _spawner;

    public EnemyReturnToIdleTransition(Transform transform, EnemySpawner spawner)
    {
      _transform = transform;
      _spawner = spawner;
    }

    public override void Tick()
    {
      if (Vector3.Distance(_spawner.Transform.position, _transform.position) < DevConfig.DistanceToRoutePoint)
        Enter<EnemyIdleState>();
    }
  }
}