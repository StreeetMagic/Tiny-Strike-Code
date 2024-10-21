using Core.PickUpTreasures;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Doors
{
  [RequireComponent(typeof(DoorAnimatorController))]
  public class DoorOpener : MonoBehaviour
  {
    public bool OpenByPickUpTreasure;
    // ReSharper disable once InconsistentNaming
    [FormerlySerializedAs("PickUpTresuareView")] public PickUpTreasureView pickUpTreasureView;

    private DoorAnimatorController _doorAnimatorController;

    private void Awake()
    {
      _doorAnimatorController = GetComponent<DoorAnimatorController>();
    }

    private void OnEnable()
    {
      if (OpenByPickUpTreasure)
      {
        pickUpTreasureView.PickedUp += OnPickedUp;
      }
    }
    
    private void OnDisable()
    {
      if (OpenByPickUpTreasure)
      {
        pickUpTreasureView.PickedUp -= OnPickedUp;
      }
    }

    private void OnPickedUp()
    {
      _doorAnimatorController.PlayOpenDoor();
    }
  }
}