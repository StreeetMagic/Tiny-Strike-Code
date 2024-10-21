using System.Collections.Generic;
using Core.AimObstacles;
using Core.Characters.Enemies;
using Meta.Sub;

namespace Meta
{
  public class QuestCompleter
  {
    private readonly CompositeQuestStorage _compositeQuestStorage;
    private readonly SimpleQuestStorage _simpleQuestStorage;

    private Dictionary<CompositeQuestId, EnemyId> _compositeQuestEnemyMap;
    private Dictionary<SimpleQuestId, EnemyId> _simpleQuestEnemyMap;
    private Dictionary<CompositeQuestId, AimObstacleId> _compositeQuestObstacleMap;
    private Dictionary<SimpleQuestId, AimObstacleId> _simpleQuestObstacleMap;

    public QuestCompleter(CompositeQuestStorage compositeQuestStorage, SimpleQuestStorage simpleQuestStorage)
    {
      _compositeQuestStorage = compositeQuestStorage;
      _simpleQuestStorage = simpleQuestStorage;

      InitializeCompositeQuestEnemyMap();
      InitializeSimpleQuestEnemyMap();
      InitializeCompositeQuestObstacleMap();
      InitializeSimpleQuestObstacleMap();
    }

    public void OnEnemyKilled(EnemyId enemyId)
    {
      foreach (CompositeQuest quest in _compositeQuestStorage.GetAll())
      {
        foreach (SubQuest subQuest in quest.SubQuests)
        {
          if (subQuest.State.Value == QuestState.Activated && _compositeQuestEnemyMap.TryGetValue(subQuest.Id, out EnemyId mappedEnemyId) && mappedEnemyId == enemyId)
          {
            subQuest.CompletedQuantity.Value++;
          }
        }
      }

      foreach (SimpleQuest simpleQuest in _simpleQuestStorage.GetAll())
      {
        if (simpleQuest.State.Value == QuestState.Activated && _simpleQuestEnemyMap.TryGetValue(simpleQuest.Config.Id, out EnemyId mappedEnemyId) && mappedEnemyId == enemyId)
        {
          simpleQuest.CompletedQuantity.Value++;
        }
      }
    }

    public void OnAimObstacleDestroyed(AimObstacleId id)
    {
      foreach (CompositeQuest quest in _compositeQuestStorage.GetAll())
      {
        foreach (SubQuest subQuest in quest.SubQuests)
        {
          if (subQuest.State.Value == QuestState.Activated && _compositeQuestObstacleMap.TryGetValue(subQuest.Id, out AimObstacleId mappedObstacleId) && mappedObstacleId == id)
          {
            subQuest.CompletedQuantity.Value++;
          }
        }
      }

      foreach (SimpleQuest simpleQuest in _simpleQuestStorage.GetAll())
      {
        if (simpleQuest.State.Value == QuestState.Activated && _simpleQuestObstacleMap.TryGetValue(simpleQuest.Config.Id, out AimObstacleId mappedObstacleId) && mappedObstacleId == id)
        {
          simpleQuest.CompletedQuantity.Value++;
        }
      }
    }

    private void InitializeCompositeQuestEnemyMap()
    {
      _compositeQuestEnemyMap = new Dictionary<CompositeQuestId, EnemyId>
      {
        { CompositeQuestId.KillHens, EnemyId.Hen },
        { CompositeQuestId.KillZombieFast, EnemyId.ZombieFast },
        { CompositeQuestId.KillTerKnife, EnemyId.TerKnife },
        { CompositeQuestId.KillTerKnifeStrong, EnemyId.TerKnifeStrong },
        { CompositeQuestId.KillTerGun, EnemyId.TerGun },
        { CompositeQuestId.KillTerSniper, EnemyId.TerSniper },
        { CompositeQuestId.KillTerGrenade, EnemyId.TerGrenade },
        { CompositeQuestId.KillTerBatMelee, EnemyId.TerBatMelee },
        { CompositeQuestId.KillTerAk47, EnemyId.TerAk47 },
        { CompositeQuestId.KillRoosters, EnemyId.Rooster },
        { CompositeQuestId.KillTerShotgun, EnemyId.TerShotgun },
        { CompositeQuestId.KillTerTutorialBoss, EnemyId.TerTutorialBoss },
        { CompositeQuestId.KillZombieTank, EnemyId.ZombieTank },
        { CompositeQuestId.KillZombiePolice, EnemyId.ZombiePolice }
      };
    }

    private void InitializeSimpleQuestEnemyMap()
    {
      _simpleQuestEnemyMap = new Dictionary<SimpleQuestId, EnemyId>
      {
        { SimpleQuestId.KillTutorialBoss, EnemyId.TerTutorialBoss }
      };
    }

    private void InitializeCompositeQuestObstacleMap()
    {
      _compositeQuestObstacleMap = new Dictionary<CompositeQuestId, AimObstacleId>
      {
        { CompositeQuestId.DestroyCrushBox, AimObstacleId.CrushBox }
      };
    }

    private void InitializeSimpleQuestObstacleMap()
    {
      _simpleQuestObstacleMap = new Dictionary<SimpleQuestId, AimObstacleId>
      {
        { SimpleQuestId.DestroyCrushBox, AimObstacleId.CrushBox }
      };
    }
  }
}