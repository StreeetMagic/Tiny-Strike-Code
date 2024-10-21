using AudioServices.Sounds;
using Meta;
using Tutorials;
using UnityEngine;
using Zenject;

namespace Core.Characters.Questers
{
  [SelectionBase]
  public class SimpleQuester : Quester
  {
    public SimpleQuestId SimpleQuestId = SimpleQuestId.Unknown;
    public SimpleQuestId ActivateOnCompleted = SimpleQuestId.Unknown;

    [Inject] private SimpleQuestStorage _storage;
    [Inject] private TutorialProvider _tutorialProvider;

    private float _timeLeft;
    private bool _opened;
    private bool _isPlaying;

    protected override void ToggleMarks()
    {
      if (_storage.Get(SimpleQuestId).State.Value is QuestState.UnActivated)
      {
        ExclamationMark.SetActive(true);
        QuestionMark.SetActive(false);
      }
      else if (_storage.Get(SimpleQuestId).State.Value == QuestState.RewardReady)
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
        if (_isPlaying)
          _isPlaying = false;

        if (WindowService.ActiveWindow is SimpleQuestWindow window)
        {
          if (window.SimpleQuest.Config.Id == SimpleQuestId)
            WindowService.CloseCurrentWindow();
        }

        _timeLeft = TimeToOpenWindow;
        _opened = false;
        return;
      }

      if (_opened)
        return;

      if (_timeLeft <= 0)
      {
        WindowService.Open(SimpleQuestId);
        _opened = true;
        _timeLeft = TimeToOpenWindow;

        if (!_isPlaying)
        {
          _isPlaying = true;
          AudioService.Play(SoundId.Voice);
        }

        return;
      }

      _timeLeft -= Time.deltaTime;
    }

    protected override void TryDestroy()
    {
      if (_storage.Get(SimpleQuestId).State.Value == QuestState.RewardTaken)
        Destroy(gameObject);
    }

    protected override void ToggleCounter()
    {
      Counter.SetActive(_storage.Get(SimpleQuestId).State.Value is QuestState.UnActivated or QuestState.RewardReady);
    }

    protected override void Activate()
    {
      if (ActivateOnCompleted == SimpleQuestId.Unknown)
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