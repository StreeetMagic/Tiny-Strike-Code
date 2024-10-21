using Core.Characters;
using Meta.BackpackStorages;
using UnityEngine;
using Zenject;

namespace Core.AimObstacles
{
  public class AimObstacleTargetOutline : MonoBehaviour
  {
    private TargetOutline _outline;

    private ITargetTrigger _targetTrigger;
    [Inject] private BackpackStorage _backpackStorage;

    private void Start()
    {
      _targetTrigger = GetComponentInChildren<ITargetTrigger>();
      _outline = GetComponentInChildren<TargetOutline>();
    }

    private void Update()
    {
      bool isTargeted = _targetTrigger.IsTargeted;

      if (_backpackStorage.IsFull())
        isTargeted = false;

      _outline.gameObject.SetActive(isTargeted);
    }
  }
}