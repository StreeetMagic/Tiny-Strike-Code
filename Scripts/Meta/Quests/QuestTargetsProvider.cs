using Core.Characters.Enemies;
using Core.Spawners.Enemies;
using LevelDesign.Maps;
using UnityEngine;

namespace Meta
{
  public class QuestTargetsProvider
  {
    protected MapProvider MapProvider;
    protected EnemySpawnerProvider EnemySpawnerProvider;
    protected readonly Transform[] Targets = new Transform[256];

    protected QuestTargetsProvider(MapProvider mapProvider, EnemySpawnerProvider enemySpawnerProvider)
    {
      MapProvider = mapProvider;
      EnemySpawnerProvider = enemySpawnerProvider;
    }

    protected void Clear()
    {
      for (int i = 0; i < Targets.Length; i++)
      {
        if (!Targets[i])
          break;
        
        Targets[i] = null;
      }
    }

    protected void SetEnemyTargetsOrNull(EnemyId id)
    {
      int targetIndex = 0;

      for (var i = 0; i < EnemySpawnerProvider.Spawners.Count; i++)
      {
        EnemySpawner spawner = EnemySpawnerProvider.Spawners[i];

        for (var j = 0; j < spawner.Enemies.Count; j++)
        {
          Enemy enemy = spawner.Enemies[j];

          if (enemy.Installer.Config.Id != id)
            continue;

          if (enemy.Installer.Health.Current.Value <= 0)
            continue;

          Targets[targetIndex++] = enemy.transform;
        }
      }
    }
  }
}