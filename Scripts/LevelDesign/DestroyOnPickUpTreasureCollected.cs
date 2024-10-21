using Core.PickUpTreasures;
using UnityEngine;

namespace LevelDesign
{
  public class DestroyOnPickUpTreasureCollected : MonoBehaviour
  {
    public PickUpTreasureView PickUpTreasureView;

    private void OnEnable()
    {
      if (!PickUpTreasureView)
        return;

      PickUpTreasureView.PickedUp += OnPickUpTreasureCollected;
    }

    private void OnDisable()
    {
      if (!PickUpTreasureView)
        return;

      PickUpTreasureView.PickedUp -= OnPickUpTreasureCollected;
    }

    private void OnPickUpTreasureCollected()
    {
      Destroy(gameObject);
    }
  }
}