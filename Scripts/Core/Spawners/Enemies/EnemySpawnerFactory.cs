using System.Collections.Generic;
using System.Linq;
using ConfigProviders;
using Core.Characters.Enemies;
using LevelDesign.EnemySpawnMarkers;
using LevelDesign.Maps;
using Prefabs;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace Core.Spawners.Enemies
{
  public class EnemySpawnerFactory
  {
    private readonly HubZenjectFactory _zenjectFactory;
    private readonly MapProvider _mapProvider;
    private readonly EnemySpawnerProvider _provider;
    private readonly DevConfigProvider _devConfigProvider;

    public EnemySpawnerFactory(HubZenjectFactory zenjectFactory, MapProvider mapProvider,
      EnemySpawnerProvider provider, DevConfigProvider devConfigProvider)
    {
      _zenjectFactory = zenjectFactory;
      _mapProvider = mapProvider;
      _provider = provider;
      _devConfigProvider = devConfigProvider;
    }

    private Map Map => _mapProvider.Map;

    public void Create()
    {
      List<EnemySpawnerMarker> enemySpawnerMarkers = Map.EnemySpawnMarkers;

      foreach (EnemySpawnerMarker enemySpawnMarker in enemySpawnerMarkers)
      {
        List<Transform> spawnPoints = CreateSpawnPoints(enemySpawnMarker);

        enemySpawnMarker.Spawner = _zenjectFactory.Instantiate<EnemySpawner>(enemySpawnMarker, spawnPoints);
        enemySpawnMarker.Spawner.Activate(enemySpawnMarker.Count);

        _provider.Spawners.Add(enemySpawnMarker.Spawner);
      }
    }

    public void CreateSingle(EnemyId enemyId, Transform spawnPoint, int count)
    {
      var prefab = _devConfigProvider.GetPrefab(PrefabId.EnemySpawnMarker).GetComponent<EnemySpawnerMarker>();

      var marker = Object.Instantiate(prefab, spawnPoint);
      marker.EnemyId = enemyId;
      marker.Count = count;

      marker.transform.parent = null;

      List<Transform> spawnPoints = CreateSpawnPoints(marker);
      marker.Spawner = _zenjectFactory.Instantiate<EnemySpawner>(marker, spawnPoints);

      marker.Spawner.Activate(marker.Count);
    }

    private List<Transform> CreateSpawnPoints(EnemySpawnerMarker marker)
    {
      List<Transform> spawnPoints = new List<Transform>();

      List<EnemySpawnPointMarker> markers = marker.GetComponentsInChildren<EnemySpawnPointMarker>().ToList();

      foreach (EnemySpawnPointMarker enemySpawnPointMarker in markers)
      {
        Transform spawnPoint = enemySpawnPointMarker.transform;
        spawnPoints.Add(spawnPoint);
      }

      return spawnPoints;
    }

    public void Destroy()
    {
      foreach (EnemySpawner spawner in _provider.Spawners)
        spawner.DeSpawnAll();

      _provider.Spawners.Clear();
    }
  }
}