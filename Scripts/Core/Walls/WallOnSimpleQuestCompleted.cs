using Meta;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Core.Walls
{
  public class WallOnSimpleQuestCompleted : MonoBehaviour
  {
    [Inject] private SimpleQuestStorage _simpleQuestStorage;

    private NavMeshObstacle _navMeshObstacle;

    public SimpleQuestId SimpleQuestId;

    private void Start()
    {
      if (SimpleQuestId == SimpleQuestId.Unknown)
        return;

      _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    private void Update()
    {
      if (_simpleQuestStorage.Get(SimpleQuestId).State.Value == QuestState.RewardTaken)
      {
        _navMeshObstacle.enabled = false;
        gameObject.SetActive(false);
      }
    }
  }
}