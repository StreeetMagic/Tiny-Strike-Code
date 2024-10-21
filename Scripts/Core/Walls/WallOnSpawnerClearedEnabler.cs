using LevelDesign.EnemySpawnMarkers;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Walls
{
  public class WallOnSpawnerClearedEnabler : MonoBehaviour
  {
    public EnemySpawnerMarker SpawnerMarker;

    private NavMeshObstacle _navMeshObstacle;

    private bool _enabled;

    private void Update()
    {
      if (!SpawnerMarker)
        return;
      
      if (_enabled)
        return;
      
      if (SpawnerMarker.Spawner == null)
        return;
      
      if (SpawnerMarker.Spawner.Enemies.Count == 0)
      {
        _enabled = true;
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _navMeshObstacle.enabled = false;
        gameObject.SetActive(false);
      }
    }
  }
}