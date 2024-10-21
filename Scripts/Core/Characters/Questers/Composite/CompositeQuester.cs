using AudioServices.Sounds;
using Composite;
using Meta;
using Tutorials;
using UnityEngine;
using Zenject;

namespace Core.Characters.Questers
{
  [SelectionBase]
  public class CompositeQuester : Quester
  {
    public CompositeQuestId CompositeQuestId;
    public CompositeQuestId ActivateOnCompleted = CompositeQuestId.Unknown;

    [Inject] private CompositeQuestStorage _storage;
    [Inject] private TutorialProvider _tutorialProvider;

    protected override void ToggleMarks()
    {
      if (_storage.Get(CompositeQuestId).State.Value is QuestState.UnActivated)
      {
        ExclamationMark.SetActive(true);
        QuestionMark.SetActive(false);
      }
      else if (_storage.Get(CompositeQuestId).State.Value == QuestState.RewardReady)
      {
        ExclamationMark.SetActive(false);
        QuestionMark.SetActive(true);
      }
    }

    protected override void OpenWindow()
    {
      if (!PlayerProvider.Instance)
        return;

      if (!IsActive)
        return;

      if (Vector3.Distance(transform.position, PlayerProvider.Instance.transform.position) > DistanceToPlayer)
      {
        if (IsPlaying)
          IsPlaying = false;

        if (WindowService.ActiveWindow is CompositeQuestWindow window)
        {
          if (window.CompositeQuest.Config.Id == CompositeQuestId)
            WindowService.CloseCurrentWindow();
        }

        TimeLeft = TimeToOpenWindow;
        Opened = false;
        return;
      }

      if (Opened)
        return;

      if (TimeLeft <= 0)
      {
        WindowService.Open(CompositeQuestId);
        Opened = true;
        TimeLeft = TimeToOpenWindow;

        if (IsPlaying)
          return;

        IsPlaying = true;
        AudioService.Play(SoundId.Voice);

        return;
      }

      TimeLeft -= Time.deltaTime;
    }

    protected override void TryDestroy()
    {
      if (_storage.Get(CompositeQuestId).State.Value == QuestState.RewardTaken)
        Destroy(gameObject);
    }

    protected override void ToggleCounter() =>
      Counter.SetActive(_storage.Get(CompositeQuestId)
        .State.Value 
        is QuestState.UnActivated 
        or QuestState.RewardReady);

    protected override void Activate()
    {
      if (IsActive)
        return;

      if (_tutorialProvider.Instance == null)
        return;
      
      if (_tutorialProvider.Instance.State.Value != TutorialState.BombDefused)
        return;

      if (ActivateOnCompleted == CompositeQuestId.Unknown)
      {
        IsActive = true;
        return;
      }

      if (_storage.Get(ActivateOnCompleted).State.Value == QuestState.RewardTaken)
      {
        IsActive = true;
        return;
      }

      IsActive = false;
    }
  }
}