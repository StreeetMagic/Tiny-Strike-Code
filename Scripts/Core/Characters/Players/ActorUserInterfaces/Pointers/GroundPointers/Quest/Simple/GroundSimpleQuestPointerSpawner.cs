using System.Collections.Generic;
using DevConfigs;
using Meta;
using Prefabs;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.GroundPointers
{
  public class GroundSimpleQuestPointerSpawner : GroundQuestPointerSpawner
  {
    [Inject] private SimpleQuestStorage _storage;

    private GroundSimpleQuestPointer[] _pointers;

    protected override void CreateAll()
    {
      List<SimpleQuest> allQuests = _storage.GetAll();
      _pointers = new GroundSimpleQuestPointer[allQuests.Count];

      for (int i = 0; i < allQuests.Count; i++)
        CreatePointer(allQuests[i], i);
    }

    private void CreatePointer(SimpleQuest quest, int index)
    {
      GroundSimpleQuestPointer pointer = Factory
        .InstantiatePrefabForComponent(DevConfigProvider.GetPrefabForComponent<GroundSimpleQuestPointer>(PrefabId.GroundSimpleQuestPointer), new List<object> { quest.Config });

      pointer.transform.SetParent(transform);
      pointer.transform.localPosition = Vector3.zero;
      pointer.Hider.Hide();
      _pointers[index] = pointer;
    }

    protected override void ShowClosestAndHideOthers()
    {
      GroundSimpleQuestPointer closestPointer = GetClosestPointer();

      if (!closestPointer)
        return;

      TogglePointerVisibility(closestPointer);
    }

    private GroundSimpleQuestPointer GetClosestPointer()
    {
      GroundSimpleQuestPointer closestPointer = null;
      float minDistance = float.MaxValue;

      foreach (GroundSimpleQuestPointer pointer in _pointers)
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

    private void TogglePointerVisibility(GroundSimpleQuestPointer closestPointer)
    {
      if (Vector3.Distance(transform.position, closestPointer.Rotator.CurrentTarget.transform.position) >= DevConfig.MinPointerDistance)
        closestPointer.Hider.Show();
      else
        closestPointer.Hider.Hide();

      foreach (GroundSimpleQuestPointer pointer in _pointers)
        if (pointer != closestPointer)
          pointer.Hider.Hide();
    }
  }
}