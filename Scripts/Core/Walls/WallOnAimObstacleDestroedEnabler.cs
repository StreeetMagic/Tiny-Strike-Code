using Core.AimObstacles;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Walls
{
  public class WallOnAimObstacleDestroedEnabler : MonoBehaviour
  {
    public AimObstacle AimObstacle;

    private NavMeshObstacle _navMeshObstacle;

    private void Start()
    {
      if (!AimObstacle)
        return;

      _navMeshObstacle = GetComponent<NavMeshObstacle>();

      AimObstacle.Installer.Health.Died += OnAimObstacleDied;
    }

    private void OnAimObstacleDied(IHealth arg1, int arg2, float arg3)
    {
      _navMeshObstacle.enabled = false;
      gameObject.SetActive(false);
    }
  }
}