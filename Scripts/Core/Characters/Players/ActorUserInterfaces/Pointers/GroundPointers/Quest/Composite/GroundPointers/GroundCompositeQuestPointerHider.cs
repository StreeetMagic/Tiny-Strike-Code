using ConfigProviders;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.GroundPointers
{
  public class GroundCompositeQuestPointerHider : MonoBehaviour
  {
    // ReSharper disable once InconsistentNaming
    public GroundCompositeQuestPointerToTargetRotator _rotator;
    public GameObject Pointer;

    [Inject] private BalanceConfigProvider _balanceConfigProvider;

    public float DistanceToTarget { get; private set; }

    private void LateUpdate()
    {
      if (!_rotator.CurrentTarget)
      {
        Hide();
        return;
      }

      MeasureDistance(_rotator.CurrentTarget);
    }

    private void MeasureDistance(Transform target)
    {
      DistanceToTarget = Vector3.Distance(transform.position, target.transform.position);
    }

    public void Show()
    {
      Pointer.SetActive(true);
    }

    public void Hide()
    {
      Pointer.SetActive(false);
    }
  }
}