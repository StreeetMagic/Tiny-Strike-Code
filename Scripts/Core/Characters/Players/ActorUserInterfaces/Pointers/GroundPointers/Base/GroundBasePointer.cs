using DevConfigs;
using LevelDesign.Maps;
using Meta.BackpackStorages;
using Tutorials;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.GroundPointers.Base
{
  public class GroundBasePointer : MonoBehaviour
  {
    public GameObject Pointer;

    [Inject] private MapProvider _mapProvider;
    [Inject] private BackpackStorage _backpackStorage;
    [Inject] private TutorialProvider _tutorialProvider;

    private void Update()
    {
      if (_tutorialProvider.Instance == null)
        return;

      if (_tutorialProvider.Instance.State.Value == TutorialState.Start || _tutorialProvider.Instance.State.Value == TutorialState.BombDefused)
      {
        Hide();
        return;
      }

      if (!_mapProvider.Map)
      {
        Hide();
        return;
      }

      if (!_mapProvider.Map.GetClosestBaseTrigger())
      {
        Hide();
        return;
      }

      RotateToBase();
      HideIfClose();
    }

    private void RotateToBase()
    {
      transform.rotation = Quaternion.LookRotation(transform.position - BaseTrigger.transform.position);
      Quaternion cachedRotation = transform.rotation;
      transform.rotation = new Quaternion(0, cachedRotation.y, 0, cachedRotation.w);
    }

    private void HideIfClose()
    {
      if (_backpackStorage.IsEmpty())
      {
        Pointer.gameObject.SetActive(false);
        return;
      }

      float distance = Vector3.Distance(transform.position, BaseTrigger.transform.position);

      Pointer.gameObject.SetActive(distance > DevConfig.MinPointerDistance);
    }

    private void Hide() =>
      Pointer.gameObject.SetActive(false);

    private BaseTrigger BaseTrigger
      => _mapProvider.Map.GetClosestBaseTrigger();
  }
}