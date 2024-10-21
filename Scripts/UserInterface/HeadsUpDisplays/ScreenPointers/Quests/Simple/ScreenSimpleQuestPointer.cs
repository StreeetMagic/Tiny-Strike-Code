using Meta;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.ScreenPointers
{
  public class ScreenSimpleQuestPointer : ScreenQuestPointer
  {
    [Inject] public SimpleQuestId QuestId { get; private set; }
    [Inject] private SimpleQuestStorage _simpleQuestStorage;
    [Inject] private SimpleQuestTargetsProvider _targetsProvider;

    private void Update()
    {
      if (!MapProvider.Map)
      {
        Hide();
        return;
      }

      if (!MapProvider.Map.GetClosestBaseTrigger())
      {
        Hide();
        return;
      }

      Transform[] targetsOrNull = _targetsProvider.GetTargetsOrNull(QuestId);

      if (targetsOrNull.Length == 0)
      {
        Hide();
        return;
      }

      if (!targetsOrNull[0])
      {
        Hide();
        return;
      }

      SwitchMarks(_simpleQuestStorage.Get(QuestId).State.Value);
      UpdatePointer(MainCamera.WorldToScreenPoint(targetsOrNull[0].position));

      Clear();
      Targets = _targetsProvider.GetTargetsOrNull(QuestId);
      SetClosestTarget();
    }
  }
}