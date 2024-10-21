using LevelDesign.Maps;
using PersistentProgresses;
using SaveLoadServices;
using UnityEngine;

namespace Core
{
  /// <summary>
  /// После смерти
  /// </summary>
  public class PlayerRespawnPosition : IProgressWriter
  {
    private readonly MapProvider _mapProvider;

    private Vector3 _position;

    public PlayerRespawnPosition(MapProvider mapProvider)
    {
      _mapProvider = mapProvider;
    }

    public Vector3 Position()
    {
      return _position == Vector3.zero
        ? _mapProvider.Map.PlayerSpawnMarker.transform.position
        : _position;
    }

    public void SetPosition(Vector3 position)
    {
      _position = position;
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      projectProgress.SpawnPositionX = _position.x;
      projectProgress.SpawnPositionY = _position.y;
      projectProgress.SpawnPositionZ = _position.z;
    }

    public void ReadProgress(ProjectProgress projectProgress)
    {
      _position = new Vector3(projectProgress.SpawnPositionX, projectProgress.SpawnPositionY, projectProgress.SpawnPositionZ);
    }
  }
}