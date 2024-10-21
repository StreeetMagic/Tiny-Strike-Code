using Meta;
using PersistentProgresses;
using SaveLoadServices;
using Utilities;
using Zenject;

namespace Tutorials
{
  public class Tutorial : IProgressWriter, ITickable
  {
    [Inject] private SimpleQuestStorage _simpleQuestStorage;

    private bool _hasProcessed;

    public ReactiveProperty<TutorialState> State { get; } = new();

    public void ReadProgress(ProjectProgress projectProgress)
    {
      State.Value = projectProgress.TutorialProgress.State;
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      projectProgress.TutorialProgress.State = State.Value;
    }

    public void Tick()
    {
      if (_hasProcessed)
        return;

      if (_simpleQuestStorage.Get(SimpleQuestId.BombDefuse).State.Value == QuestState.RewardTaken)
      {
        State.Value = TutorialState.BombDefused;
        _hasProcessed = true;
      }
    }
  }
}