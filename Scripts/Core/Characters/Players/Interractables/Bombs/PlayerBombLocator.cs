using Core.Bombs;
using LevelDesign;
using LevelDesign.Maps;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerBombLocator : ITickable
  {
    [Inject] private MapProvider _mapProvider;
    [Inject] private PlayerProvider _playerProvider;
    
    public Bomb ClosestBomb { get; private set; }
    
    public void Tick()
    {
      ClosestBomb = null;
      float closestDistance = float.MaxValue;

      foreach (BombSpawnMarker marker in _mapProvider.Map.BombSpawnMarkers)
      {
        if (!marker.Spawned || !marker.Bomb || marker.Bomb.IsDefused())
          continue;

        float distance = Vector3.Distance(marker.Bomb.transform.position, _playerProvider.Instance.transform.position);

        if (distance < closestDistance)
        {
          closestDistance = distance;
          ClosestBomb = marker.Bomb;
        }
      }
    }
  }
}