using System.Collections.Generic;
using Core.Characters.Players;
using DevConfigs;
using Meta;
using Prefabs;
using Tutorials;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.ScreenPointers
{
  public class ScreenCompositeQuestPointerSpawner : ScreenQuestPointerSpawner
  {
    [Inject] private CompositeQuestStorage _storage;
    [Inject] private PlayerProvider _playerProvider;
    [Inject] private TutorialProvider _tutorialProvider;

    private ScreenCompositeQuestPointer[] _pointers;

    protected override void CreateAll()
    {
      List<CompositeQuest> allQuests = _storage.GetAll();
      _pointers = new ScreenCompositeQuestPointer[allQuests.Count];

      for (int i = 0; i < allQuests.Count; i++)
        CreatePointer(allQuests[i], i);
    }

    private void CreatePointer(CompositeQuest quest, int index)
    {
      ScreenCompositeQuestPointer pointer = Factory
        .InstantiatePrefabForComponent(DevConfigProvider.GetPrefabForComponent<ScreenCompositeQuestPointer>(PrefabId.ScreenCompositeQuestPointer), new List<object> { quest.Config.Id });

      pointer.ParentCanvasRectTransform = ParentCanvasRectTransform;
      pointer.transform.SetParent(transform, false);

      pointer.Hide();
      _pointers[index] = pointer;
    }

    protected override void ShowClosestAndHideOthers()
    {
      ScreenCompositeQuestPointer closestPointer = GetClosestPointer();

      if (!closestPointer)
        return;

      TogglePointerVisibility(closestPointer);
    }

    private ScreenCompositeQuestPointer GetClosestPointer()
    {
      ScreenCompositeQuestPointer closestPointer = null;
      float minDistance = float.MaxValue;

      foreach (ScreenCompositeQuestPointer pointer in _pointers)
      {
        if (!pointer.CurrentTarget)
          continue;

        float distance = Vector3.Distance(_playerProvider.Instance.Transform.position, pointer.CurrentTarget.transform.position);

        if (distance > minDistance)
          continue;

        minDistance = distance;
        closestPointer = pointer;
      }

      return closestPointer;
    }

    private void TogglePointerVisibility(ScreenCompositeQuestPointer closestPointer)
    {
      if (Vector3.Distance(_playerProvider.Instance.Transform.position, closestPointer.CurrentTarget.transform.position) >= DevConfig.MinPointerDistance)
      {
        if (_tutorialProvider.Instance.State.Value != TutorialState.BombDefused)
        {
          closestPointer.IsClosest = false;
          closestPointer.Hide();
          return;
        }

        closestPointer.IsClosest = true;
        closestPointer.Show();
      }
      else
      {
        closestPointer.IsClosest = false;
        closestPointer.Hide();
      }

      foreach (ScreenCompositeQuestPointer pointer in _pointers)
      {
        if (pointer != closestPointer)
        {
          pointer.IsClosest = false;
          pointer.Hide();
        }
      }
    }
  }
}