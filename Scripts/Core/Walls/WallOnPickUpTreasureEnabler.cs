using Core.PickUpTreasures;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Core.Walls
{
  public class WallOnPickUpTreasureEnabler : MonoBehaviour
  {
    [Inject] private PickUpTreasureCollector _pickUpTreasureCollector;

    private NavMeshObstacle _navMeshObstacle;

    public PickUpTreasureId PickUpTreasureId;

    private void Start()
    {
      if (PickUpTreasureId == PickUpTreasureId.Unknown)
        return;

      _navMeshObstacle = GetComponent<NavMeshObstacle>();

      _pickUpTreasureCollector.PickUpTreasureCollected += OnPickUpTreasureCollected;
    }

    private void OnPickUpTreasureCollected(PickUpTreasureId id)
    {
      if (id == PickUpTreasureId)
      {
        _navMeshObstacle.enabled = false;
        gameObject.SetActive(false);
      }
    }
  }
}