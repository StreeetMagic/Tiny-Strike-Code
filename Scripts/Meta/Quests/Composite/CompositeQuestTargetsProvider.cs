using System;
using System.Collections.Generic;
using Core.AimObstacles;
using Core.Characters.Enemies;
using Core.Characters.Questers;
using Core.Spawners.Enemies;
using LevelDesign.Maps;
using UnityEngine;

namespace Meta
{
  public class CompositeQuestTargetsProvider : QuestTargetsProvider
  {
    private readonly CompositeQuestStorage _storage;

    private readonly Dictionary<CompositeQuestId, Action> _questActions;
    
    private CompositeQuester _compositeQuester;

    public CompositeQuestTargetsProvider(CompositeQuestStorage storage, MapProvider mapProvider,
      EnemySpawnerProvider enemySpawnerProvider) : base(mapProvider, enemySpawnerProvider)
    {
      _storage = storage;
      MapProvider = mapProvider;
      EnemySpawnerProvider = enemySpawnerProvider;

      _questActions = new Dictionary<CompositeQuestId, Action>
      {
        { CompositeQuestId.DestroyCrushBox, () => GetAimObstacleTargetsOrNull(AimObstacleId.CrushBox) },
        { CompositeQuestId.KillHens, () => SetEnemyTargetsOrNull(EnemyId.Hen) },
        { CompositeQuestId.KillZombieFast, () => SetEnemyTargetsOrNull(EnemyId.ZombieFast) },
        { CompositeQuestId.KillTerKnife, () => SetEnemyTargetsOrNull(EnemyId.TerKnife) },
        { CompositeQuestId.KillTerKnifeStrong, () => SetEnemyTargetsOrNull(EnemyId.TerKnifeStrong) },
        { CompositeQuestId.KillTerGun, () => SetEnemyTargetsOrNull(EnemyId.TerGun) },
        { CompositeQuestId.KillTerSniper, () => SetEnemyTargetsOrNull(EnemyId.TerSniper) },
        { CompositeQuestId.KillTerGrenade, () => SetEnemyTargetsOrNull(EnemyId.TerGrenade) },
        { CompositeQuestId.KillTerBatMelee, () => SetEnemyTargetsOrNull(EnemyId.TerBatMelee) },
        { CompositeQuestId.KillTerAk47, () => SetEnemyTargetsOrNull(EnemyId.TerAk47) },
        { CompositeQuestId.KillRoosters, () => SetEnemyTargetsOrNull(EnemyId.Rooster) },
        { CompositeQuestId.KillTerShotgun, () => SetEnemyTargetsOrNull(EnemyId.TerShotgun) },
        { CompositeQuestId.KillTerTutorialBoss, () => SetEnemyTargetsOrNull(EnemyId.TerTutorialBoss) },
        { CompositeQuestId.KillZombieTank, () => SetEnemyTargetsOrNull(EnemyId.ZombieTank) },
        { CompositeQuestId.KillZombiePolice, () => SetEnemyTargetsOrNull(EnemyId.ZombiePolice) }
      };
    }

    public Transform[] GetTargetsOrNull(CompositeQuestId compositeQuestId)
    {
      Clear();
      CompositeQuest compositeQuest = _storage.Get(compositeQuestId);

      switch (compositeQuest.State.Value)
      {
        case QuestState.UnActivated:
        case QuestState.RewardReady:
          SetQuesterOrNull(compositeQuestId);
          return Targets;

        case QuestState.Activated:
          if (!_questActions.TryGetValue(compositeQuestId, out Action action))
            throw new ArgumentOutOfRangeException(nameof(compositeQuestId), compositeQuestId, null);

          action.Invoke();

          return Targets;

        case QuestState.RewardTaken:
          return Array.Empty<Transform>();

        case QuestState.Unknown:
        default:
          throw new ArgumentOutOfRangeException(nameof(compositeQuestId), compositeQuestId, null);
      }
    }

    private void SetQuesterOrNull(CompositeQuestId compositeQuestId)
    {
      if (MapProvider.Map.CompositeQuesters.Count == 0)
        return;

      _compositeQuester = null;

      foreach (CompositeQuester quester in MapProvider.Map.CompositeQuesters)
      {
        if (quester.CompositeQuestId == compositeQuestId)
        {
          _compositeQuester = quester;
          break;
        }
      }

      if (_compositeQuester && _compositeQuester.IsActive)
        Targets[0] = _compositeQuester.transform;
    }

    private void GetAimObstacleTargetsOrNull(AimObstacleId id)
    {
      for (var i = 0; i < MapProvider.Map.AimObstacles.Count; i++)
      {
        AimObstacle aimObstacle = MapProvider.Map.AimObstacles[i];
        
        if (aimObstacle.Id != id)
          continue;

        if (!aimObstacle.IsQuestTarget)
          continue;

        if (aimObstacle.Installer.Health.Current.Value <= 0)
          continue;

        Targets[i] = aimObstacle.transform;
      }
    }
  }
}