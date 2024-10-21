using Core.Characters.Enemies.States.Idle;
using Core.Characters.FiniteStateMachines;
using DevConfigs;
using UnityEngine;

namespace Core.Characters.Enemies.States.Patrol
{
  public class EnemyPatrolToIdleTransition : Transition
  {
    private readonly EnemyRoutePointsManager _points;
    private readonly Transform _transform;
    private float _timeSinceLastTick;

    private const float TickInterval = 1f; // Интервал в секундах

    public EnemyPatrolToIdleTransition(EnemyRoutePointsManager points, Transform transform)
    {
      _points = points;
      _transform = transform;
      _timeSinceLastTick = 0f;
    }

    public override void Tick()
    {
      _timeSinceLastTick += Time.deltaTime;

      if (_timeSinceLastTick >= TickInterval)
      {
        _timeSinceLastTick = 0f;

        Vector3 pointPosition = _points.Current().position;
        Vector3 transformPosition = _transform.position;

        Vector2 point2D = new Vector2(pointPosition.x, pointPosition.z);
        Vector2 transform2D = new Vector2(transformPosition.x, transformPosition.z);

        float distanceSquared = (point2D - transform2D).sqrMagnitude;
        float requiredDistanceSquared = DevConfig.DistanceToRoutePoint * DevConfig.DistanceToRoutePoint;

        if (distanceSquared < requiredDistanceSquared)
        {
          Enter<EnemyIdleState>();
        }
      }
    }
  }
}