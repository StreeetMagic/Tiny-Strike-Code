using System.Collections.Generic;
using DevConfigs;
using Meta;
using Prefabs;
using Tutorials;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.GroundPointers
{
  public class GroundCompositeQuestPointerSpawner : GroundQuestPointerSpawner
  {
    [Inject] private CompositeQuestStorage _storage;
    [Inject] private TutorialProvider _tutorialProvider;

    private GroundCompositeQuestPointer[] _pointers;

    protected override void CreateAll()
    {
      List<CompositeQuest> allQuests = _storage.GetAll();
      _pointers = new GroundCompositeQuestPointer[allQuests.Count];

      for (int i = 0; i < allQuests.Count; i++)
        CreatePointer(allQuests[i], i);
    }

    private void CreatePointer(CompositeQuest quest, int index)
    {
      GroundCompositeQuestPointer pointer = Factory
        .InstantiatePrefabForComponent(DevConfigProvider.GetPrefabForComponent<GroundCompositeQuestPointer>(PrefabId.GroundCompositeQuestPointer), new List<object> { quest.Config });

      pointer.transform.SetParent(transform);
      pointer.transform.localPosition = Vector3.zero;
      pointer.Hider.Hide();
      _pointers[index] = pointer;
    }

    protected override void ShowClosestAndHideOthers()
    {
      if (_tutorialProvider.Instance == null)
        return;

      if (_tutorialProvider.Instance.State.Value is TutorialState.Start or TutorialState.BombDefused)
        return;

      GroundCompositeQuestPointer closestPointer = GetClosestPointer();

      if (!closestPointer)
        return;

      TogglePointerVisibility(closestPointer);
    }

    private GroundCompositeQuestPointer GetClosestPointer()
    {
      GroundCompositeQuestPointer closestPointer = null;
      float minDistance = float.MaxValue;

      foreach (GroundCompositeQuestPointer pointer in _pointers)
      {
        if (!pointer.Rotator.CurrentTarget)
          continue;

        float distance = Vector3.Distance(transform.position, pointer.Rotator.CurrentTarget.transform.position);

        if (distance > minDistance)
          continue;

        minDistance = distance;
        closestPointer = pointer;
      }

      return closestPointer;
    }

    private void TogglePointerVisibility(GroundCompositeQuestPointer closestPointer)
    {
      if (Vector3.Distance(transform.position, closestPointer.Rotator.CurrentTarget.transform.position) >= DevConfig.MinPointerDistance)
        closestPointer.Hider.Show();
      else
        closestPointer.Hider.Hide();

      foreach (GroundCompositeQuestPointer pointer in _pointers)
        if (pointer != closestPointer)
          pointer.Hider.Hide();
    }
  }
}