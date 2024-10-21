using System.Collections.Generic;
using Core.Characters.Players;
using DevConfigs;
using Meta;
using Prefabs;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.ScreenPointers
{
  public class ScreenSimpleQuestPointerSpawner : ScreenQuestPointerSpawner
  {
    [Inject] private SimpleQuestStorage _storage;
    [Inject] private PlayerProvider _playerProvider;

    private ScreenSimpleQuestPointer[] _pointers;

    protected override void CreateAll()
    {
      List<SimpleQuest> allQuests = _storage.GetAll();
      _pointers = new ScreenSimpleQuestPointer[allQuests.Count];

      for (int i = 0; i < allQuests.Count; i++)
        CreatePointer(allQuests[i], i);
    }

    private void CreatePointer(SimpleQuest quest, int index)
    {
      ScreenSimpleQuestPointer pointer = Factory
        .InstantiatePrefabForComponent(DevConfigProvider.GetPrefabForComponent<ScreenSimpleQuestPointer>(PrefabId.ScreenSimpleQuestPointer), new List<object> { quest.Config.Id });

      pointer.ParentCanvasRectTransform = ParentCanvasRectTransform;
      pointer.transform.SetParent(transform, false);

      pointer.Hide();
      _pointers[index] = pointer;
    }

    protected override void ShowClosestAndHideOthers()
    {
      ScreenSimpleQuestPointer closestPointer = GetClosestPointer();

      if (!closestPointer)
        return;

      TogglePointerVisibility(closestPointer);
    }

    private ScreenSimpleQuestPointer GetClosestPointer()
    {
      ScreenSimpleQuestPointer closestPointer = null;
      float minDistance = float.MaxValue;

      foreach (ScreenSimpleQuestPointer pointer in _pointers)
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

    private void TogglePointerVisibility(ScreenSimpleQuestPointer closestPointer)
    {
      if (Vector3.Distance(_playerProvider.Instance.Transform.position, closestPointer.CurrentTarget.transform.position) >= DevConfig.MinPointerDistance)
      {
        closestPointer.IsClosest = true;
        closestPointer.Show();
      }
      else
      {
        closestPointer.IsClosest = false;
        closestPointer.Hide();
      }

      foreach (ScreenSimpleQuestPointer pointer in _pointers)
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