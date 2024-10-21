using LevelDesign;
using LevelDesign.Maps;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerHostageLocator : ITickable
  {
    [Inject] private MapProvider _mapProvider;
    [Inject] private PlayerProvider _playerProvider;

    public Hostage ClosestHostage { get; private set; }

    public void Tick()
    {
      ClosestHostage = null;
      float closestDistance = float.MaxValue;

      foreach (HostageSpawnMarker marker in _mapProvider.Map.HostageSpawnMarkers)
      {
        if (!marker.Spawned || !marker.Hostage || marker.Hostage.IsResqued())
          continue;

        float distance = Vector3.Distance(marker.Hostage.transform.position, _playerProvider.Instance.transform.position);

        if (distance < closestDistance)
        {
          closestDistance = distance;
          ClosestHostage = marker.Hostage;
        }
      }
    }
  }
}