using UnityEngine;
using UnityEngine.UI;

namespace Cheats
{
  public class HeadsUpDisplayDisabler : MonoBehaviour
  {
    public Button DisableHeadsUpDisplayButton;
    public GameObject[] Disalables;
    public GameObject PlayableJoystick;
    public GameObject CreativeJoystick;

    private void OnEnable() =>
      DisableHeadsUpDisplayButton.onClick.AddListener(OnDisableHeadsUpDisplayButtonClick);

    private void OnDestroy() =>
      DisableHeadsUpDisplayButton.onClick.RemoveListener(OnDisableHeadsUpDisplayButtonClick);

    private void OnDisableHeadsUpDisplayButtonClick()
    {
      PlayableJoystick.SetActive(!PlayableJoystick.gameObject.activeSelf);
      CreativeJoystick.SetActive(!CreativeJoystick.gameObject.activeSelf);
      foreach (GameObject disalable in Disalables)
      {
        if (disalable != null)
          disalable.SetActive(!disalable.gameObject.activeSelf);
      }
    }
  }
}