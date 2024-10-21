using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Characters.Enemies
{
  public class EnemyRoutePointsManager
  {
    private readonly List<Transform> _spawnPoints;

    private int _currentRouteIndex;

    private EnemyRoutePointsManager(List<Transform> initialSpawnPoints, EnemyInstaller enemyInstaller)
    {
      _spawnPoints = initialSpawnPoints;

      if (initialSpawnPoints.Count == 0)
        throw new System.InvalidOperationException("Spawn points list is empty");

      if (enemyInstaller.RandomPatroling)
        _spawnPoints = ShuffleRoutePoints(initialSpawnPoints);

      if (enemyInstaller.RandomPatroling)
        SetRandomRoute();
    }

    public Transform Current()
      => _spawnPoints[_currentRouteIndex];

    public void SetNextRoute()
    {
       if (_spawnPoints.Count == 0)
        return;
       
      _currentRouteIndex = (_currentRouteIndex + 1) % _spawnPoints.Count;
    }

    public void SetRandomRoute()
    {
      if (_spawnPoints.Count == 0)
        return;

      if (_spawnPoints.Count == 1)
      {
        _currentRouteIndex = 0;
        return;
      }

      int currentIndex = _currentRouteIndex;

      do
      {
        _currentRouteIndex = Random.Range(0, _spawnPoints.Count);
      } while (_currentRouteIndex == currentIndex);
    }

    private List<Transform> ShuffleRoutePoints(List<Transform> points)
    {
      var list = new List<Transform>(points);

      for (int i = 0; i < points.Count; i++)
      {
        int randIndex = Random.Range(i, list.Count);
        (list[randIndex], list[i]) = (list[i], list[randIndex]);
      }

      return list;
    }
  }
}