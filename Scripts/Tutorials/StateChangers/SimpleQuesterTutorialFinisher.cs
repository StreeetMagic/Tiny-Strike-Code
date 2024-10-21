using Core.Characters.Questers;
using Meta;
using UnityEngine;
using Zenject;

namespace Tutorials.StateChangers
{
  [RequireComponent(typeof(SimpleQuester))]
  public class SimpleQuesterTutorialFinisher : MonoBehaviour
  {
    public TutorialState State = TutorialState.BombDefused;

    [Inject] private TutorialProvider _tutorialProvider;
    [Inject] private SimpleQuestStorage _simpleQuestStorage;

    private SimpleQuester _quester;
    private bool _hasProcessed;

    private void Awake()
    {
      _quester = GetComponent<SimpleQuester>();

      if (State == TutorialState.Uknown)
        Debug.LogError("EnemySpawnerTutorialFinisher: Unknown state");
    }

    private void Update()
    {
      if (!_quester)
        return;

      if (_hasProcessed)
        return;

      SimpleQuestId questId = _quester.SimpleQuestId;
      QuestState state = _simpleQuestStorage.Get(questId).State.Value;

      if (state == QuestState.RewardTaken)
      {
        if (_tutorialProvider.Instance.State.Value != State)
        {
          _tutorialProvider.Instance.State.Value = State;
          _hasProcessed = true;
        }
      }
    }
  }
}