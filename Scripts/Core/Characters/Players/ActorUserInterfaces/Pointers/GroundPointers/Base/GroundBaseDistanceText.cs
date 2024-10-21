using LevelDesign.Maps;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.GroundPointers.Base
{
  public class GroundBaseDistanceText : MonoBehaviour
  {
    public TextMeshProUGUI Text;

    private PlayerProvider _playerProvider;
    private MapProvider _mapProvider;

    [Inject]
    private void Construct(PlayerProvider playerProvider, MapProvider mapProvider)
    {
      _playerProvider = playerProvider;
      _mapProvider = mapProvider;
    }

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;

      if (!_mapProvider.Map)
        return;
      
      if (!_mapProvider.Map.GetClosestBaseTrigger())
        return;

      BaseTrigger baseTrigger = _mapProvider.Map.GetClosestBaseTrigger();
      PlayerInstaller player = _playerProvider.Instance;

      float distance = Vector3.Distance(baseTrigger.transform.position, player.transform.position);

      int distanceInt = (int)distance;

      Text.text = distanceInt + " m";
    }
  }
}