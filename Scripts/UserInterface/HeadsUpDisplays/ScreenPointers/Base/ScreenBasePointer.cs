using Meta.BackpackStorages;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.ScreenPointers.Base
{
  public class ScreenBasePointer : ScreenPointer
  {
    [Inject] private BackpackStorage _backpackStorage;

    private void Update()
    {
      if (TutorialProvider.Instance == null)
      {
        return;
      }

      if (!_backpackStorage.IsFull())
      {
        Hide();
        return;
      }

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

      UpdatePointer(MainCamera.WorldToScreenPoint(BaseTrigger.position));
    }

    private Transform BaseTrigger
      => MapProvider.Map.GetClosestBaseTrigger().transform;
  }
}