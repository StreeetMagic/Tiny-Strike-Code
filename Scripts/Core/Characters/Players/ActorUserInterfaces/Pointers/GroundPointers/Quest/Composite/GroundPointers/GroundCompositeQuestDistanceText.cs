using LevelDesign.Maps;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.GroundPointers
{
  public class GroundCompositeQuestDistanceText : MonoBehaviour
  {
    public TextMeshProUGUI Text;
    // ReSharper disable once InconsistentNaming
    public GroundCompositeQuestPointerToTargetRotator _rotator;

    [Inject] private PlayerProvider _playerProvider;
    [Inject] private MapProvider _mapProvider;

    private void Update()
    {
      if (!_mapProvider.Map)
        return;

      if (!_rotator)
        return;

      if (!_rotator.CurrentTarget)
        return;

      if (!_playerProvider.Instance)
        return;

      Transform player = _playerProvider.Instance.Transform;

      Transform target = _rotator.CurrentTarget;

      float distance = Vector3.Distance(target.position, player.transform.position);

      int distanceInt = (int)distance;

      Text.text = distanceInt + " m";
    }
  }
}