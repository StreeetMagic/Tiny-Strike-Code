using System.Collections.Generic;
using System.Linq;
using Core;
using Core.AimObstacles;
using Core.Characters.Players;
using Core.Characters.Questers;
using LevelDesign.EnemySpawnMarkers;
using LevelDesign.PlayerSpawnMarkers;
using Meta;
using Meta.Chests;
using UnityEngine;
using Zenject;

namespace LevelDesign.Maps
{
  public class Map : MonoBehaviour
  {
    public int ActiveChests = 5;

    public PlayerSpawnMarker PlayerSpawnMarker;
    public List<EnemySpawnerMarker> EnemySpawnMarkers;
    public List<BaseTrigger> BaseTrigger;
    public List<CompositeQuester> CompositeQuesters;
    public List<SimpleQuester> SimpleQuesters;
    public List<AimObstacle> AimObstacles;
    public List<HostageSpawnMarker> HostageSpawnMarkers;
    public List<BombSpawnMarker> BombSpawnMarkers;
    public List<Chest> Chests;
    
    [Inject] private PlayerProvider _playerProvider;
    [Inject] private SimpleQuestStorage _simpleQuestStorage;

    public void Setup()
    {
      PlayerSpawnMarker = GetComponentInChildren<PlayerSpawnMarker>();
      EnemySpawnMarkers = GetComponentsInChildren<EnemySpawnerMarker>().ToList();
      BaseTrigger = GetComponentsInChildren<BaseTrigger>().ToList();
      CompositeQuesters = GetComponentsInChildren<CompositeQuester>().ToList();
      SimpleQuesters = GetComponentsInChildren<SimpleQuester>().ToList();
      AimObstacles = GetComponentsInChildren<AimObstacle>().ToList();
      HostageSpawnMarkers = GetComponentsInChildren<HostageSpawnMarker>().ToList();
      BombSpawnMarkers = GetComponentsInChildren<BombSpawnMarker>().ToList();
      Chests = GetComponentsInChildren<Chest>().ToList();

      Validate();
    }

    public BaseTrigger GetClosestBaseTrigger()
    {
      BaseTrigger closestTrigger = null;
      float closestDistance = float.MaxValue;
      Vector3 playerPosition = _playerProvider.Instance.transform.position;

      for (int i = 0; i < BaseTrigger.Count; i++)
      {
        float distance = Vector3.Distance(BaseTrigger[i].transform.position, playerPosition);

        if (distance < closestDistance)
        {
          closestDistance = distance;
          closestTrigger = BaseTrigger[i];
        }
      }

      return closestTrigger;
    }

    public SimpleQuester GetSimpleQuesterOrNull(SimpleQuestId id)
    {
      foreach (SimpleQuester x in SimpleQuesters)
      {
        if (x.SimpleQuestId == id)
          return x;
      }

      return null;
    }

    private void Validate()
    {
      ValidateHostages();
      ValidateBombs();
    }

    private void ValidateHostages()
    {
      SimpleQuestId simpleQuestId = SimpleQuestId.HostagesRescue;

      int hostageQuestCount = _simpleQuestStorage.Get(simpleQuestId).Config.Quantity;

      if (HostageSpawnMarkers.Count != hostageQuestCount)
        Debug.LogError($"Hostage spawn marker count ({HostageSpawnMarkers.Count}) doesn't match with quest quantity ({hostageQuestCount})");
    }

    private void ValidateBombs()
    {
      SimpleQuestId simpleQuestId = SimpleQuestId.BombDefuse;

      int bombQuestCount = _simpleQuestStorage.Get(simpleQuestId).Config.Quantity;

      if (BombSpawnMarkers.Count != bombQuestCount)
        Debug.LogError($"Bomb spawn marker count ({BombSpawnMarkers.Count}) doesn't match with quest quantity ({bombQuestCount})");
    }
  }
}