using UnityEngine;

namespace Core.Characters.Movers
{
  public interface IMover
  {
    void SetDestination(Vector3 target, float speed);
    void Stop();
    void Warp(Vector3 warpPositionPosition);
  }
}