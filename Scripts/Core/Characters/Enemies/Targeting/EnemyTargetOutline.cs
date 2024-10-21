using Meta.BackpackStorages;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class EnemyTargetOutline : MonoBehaviour
  {
    private TargetOutline _outline;
    private EnemyTargetTrigger _targetTrigger;
    [Inject] private BackpackStorage _backpackStorage;

    private void Start()
    {
      _targetTrigger = GetComponentInChildren<EnemyTargetTrigger>();
      _outline = GetComponentInChildren<TargetOutline>();
    }

    private void Update()
    {
      UpdateOutline();
    }

    private void UpdateOutline()
    {
      bool isTargeted = _targetTrigger.IsTargeted;

      if (isTargeted && !_outline.gameObject.activeSelf)
        _outline.gameObject.SetActive(true);
      else if (!isTargeted && _outline.gameObject.activeSelf)
        _outline.gameObject.SetActive(false);
    }
  }
}