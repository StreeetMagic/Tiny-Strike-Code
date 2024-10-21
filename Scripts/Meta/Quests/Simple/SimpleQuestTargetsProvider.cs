using System;
using System.Linq;
using Core.AimObstacles;
using Core.Characters.Enemies;
using Core.Characters.Players;
using Core.Characters.Questers;
using Core.Spawners.Enemies;
using LevelDesign;
using LevelDesign.Maps;
using UnityEngine;

namespace Meta
{
  public class SimpleQuestTargetsProvider : QuestTargetsProvider
  {
    private readonly SimpleQuestStorage _storage;
    private readonly PlayerProvider _playerProvider;

    public SimpleQuestTargetsProvider(SimpleQuestStorage storage,
      MapProvider mapProvider, EnemySpawnerProvider enemySpawnerProvider,
      PlayerProvider playerProvider) : base(mapProvider, enemySpawnerProvider)
    {
      _storage = storage;
      _playerProvider = playerProvider;
      MapProvider = mapProvider;
      EnemySpawnerProvider = enemySpawnerProvider;
    }

    public Transform[] GetTargetsOrNull(SimpleQuestId simpleQuestId)
    {
      Clear();
      SimpleQuest simpleQuest = _storage.Get(simpleQuestId);

      switch (simpleQuest.State.Value)
      {
        case QuestState.UnActivated:
        case QuestState.RewardReady:
          SetQuesterOrNull(simpleQuestId);
          return Targets;

        case QuestState.Activated:
          SetActivatedSubQuestTargetsOrNull(simpleQuestId);
          return Targets;

        case QuestState.RewardTaken:
          return Array.Empty<Transform>();

        case QuestState.Unknown:
        default:
          throw new ArgumentOutOfRangeException(nameof(simpleQuestId), simpleQuestId, null);
      }
    }

    private void SetActivatedSubQuestTargetsOrNull(SimpleQuestId simpleQuestId)
    {
      switch (simpleQuestId)
      {
        case SimpleQuestId.DestroyCrushBox:
          SetAimObstacleTargetsOrNull(AimObstacleId.CrushBox);
          break;

        case SimpleQuestId.KillTutorialBoss:
          SetEnemyTargetsOrNull(EnemyId.TerTutorialBoss);
          break;

        case SimpleQuestId.BombDefuse:
          SetBombTargetsOrNull(simpleQuestId);
          break;

        case SimpleQuestId.HostagesRescue:
          SetHostageTargetsOrNull(simpleQuestId);
          break;

        case SimpleQuestId.Unknown:
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void SetQuesterOrNull(SimpleQuestId simpleQuestId)
    {
      if (MapProvider.Map.SimpleQuesters.Count == 0)
        return;

      SimpleQuester simpleQuester =
        MapProvider
          .Map
          .SimpleQuesters
          .FirstOrDefault(quester => quester.SimpleQuestId == simpleQuestId);

      if (simpleQuester && simpleQuester.IsActive)
        Targets[0] = simpleQuester.transform;
    }

    private void SetAimObstacleTargetsOrNull(AimObstacleId id)
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

    private void SetHostageTargetsOrNull(SimpleQuestId simpleQuestId)
    {
      if (_playerProvider.Instance.HostageHolder.HasHostage)
      {
        SetQuesterOrNull(simpleQuestId);
        return;
      }

      for (var i = 0; i < MapProvider.Map.HostageSpawnMarkers.Count; i++)
      {
        HostageSpawnMarker hostageSpawnMarker = MapProvider.Map.HostageSpawnMarkers[i];

        if (hostageSpawnMarker.QuestId != simpleQuestId)
          continue;

        if (!hostageSpawnMarker.Spawned)
          continue;

        if (!hostageSpawnMarker.Hostage)
          continue;

        if (hostageSpawnMarker.Hostage.IsResqued())
          continue;

        Targets[i] = hostageSpawnMarker.transform;
      }
    }

    private void SetBombTargetsOrNull(SimpleQuestId simpleQuestId)
    {
      for (var i = 0; i < MapProvider.Map.BombSpawnMarkers.Count; i++)
      {
        BombSpawnMarker bombSpawnMarker = MapProvider.Map.BombSpawnMarkers[i];

        if (bombSpawnMarker.QuestId != simpleQuestId)
          continue;

        if (!bombSpawnMarker.Spawned)
          continue;

        if (!bombSpawnMarker.Bomb)
          continue;

        if (bombSpawnMarker.Bomb.IsDefused())
          continue;

        Targets[i] = bombSpawnMarker.transform;
      }
    }
  }
}