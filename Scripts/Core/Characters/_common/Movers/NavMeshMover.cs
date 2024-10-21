using TimeServices;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Characters.Movers
{
  public class NavMeshMover : IMover
  {
    private readonly NavMeshAgent _navMeshAgent;
    private readonly TimeService _timeService;

    private Vector3 _cachedVelocity;

    public NavMeshMover(NavMeshAgent navMeshAgent, TimeService timeService)
    {
      _navMeshAgent = navMeshAgent;

      _timeService = timeService;

      _timeService.Paused += OnPaused;
      _timeService.UnPaused += OnUnpaused;
    }

    private void OnPaused()
    {
      if (_navMeshAgent.enabled)
      {
        if (_navMeshAgent.isOnNavMesh)
        {
          if (!_navMeshAgent.isStopped)
          {
            _navMeshAgent.isStopped = true;
          }
        }
      }
    }

    private void OnUnpaused()
    {
      if (_navMeshAgent.enabled)
      {
        if (_navMeshAgent.isOnNavMesh)
        {
          if (_navMeshAgent.isStopped)
          {
            _navMeshAgent.isStopped = false;
          }
        }
      }
    }

    public void SetDestination(Vector3 target, float speed)
    {
      _navMeshAgent.isStopped = false;
      _navMeshAgent.acceleration = float.MaxValue;
      _navMeshAgent.SetDestination(target);
      _navMeshAgent.speed = speed;
    }

    public void Stop()
    {
      _navMeshAgent.ResetPath();
      _navMeshAgent.isStopped = true;
      _navMeshAgent.speed = 0f;
    }

    public void Warp(Vector3 warpPositionPosition)
    {
      _navMeshAgent.Warp(warpPositionPosition);
    }
  }
}