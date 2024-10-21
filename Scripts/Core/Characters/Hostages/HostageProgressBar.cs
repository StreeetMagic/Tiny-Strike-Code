using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Characters
{
  public class HostageProgressBar : MonoBehaviour
  {
    public Hostage Hostage;
    public Slider Slider;
    public TextMeshProUGUI Text;

    private void Update()
    {
      Slider.value = Hostage.ResqueProgress;
      Text.text = Mathf.RoundToInt(Hostage.ResqueProgress * 100).ToString();

      if (Hostage.IsResqued())
      {
        Slider.gameObject.SetActive(false);
        Text.gameObject.SetActive(false);
      }
      else
      {
        Slider.gameObject.SetActive(true);
        Text.gameObject.SetActive(true);
      }
    }
  }
}