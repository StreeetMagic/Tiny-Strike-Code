using Core.PickUpTreasures;
using UnityEngine;
using Zenject;

namespace LevelDesign
{
  public class DestroyOnPickUpTreasureIdCollected : MonoBehaviour
  {
    public PickUpTreasureId Id;

    [Inject] private PickUpTreasureCollector _service;

    private void OnEnable()
    {
      if (Id == PickUpTreasureId.Unknown)
        return;

      _service.PickUpTreasureCollected += OnPickUpTreasureCollected;
    }

    private void OnDisable()
    {
      if (Id == PickUpTreasureId.Unknown)
        return;

      _service.PickUpTreasureCollected -= OnPickUpTreasureCollected;
    }

    private void OnPickUpTreasureCollected(PickUpTreasureId id)
    {
      if (Id == id)
        Destroy(gameObject);
    }
  }
}