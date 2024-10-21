using UnityEngine;
using UnityEngine.UI;

namespace UpgradeCells
{
  public class Icon : MonoBehaviour
  {
    public Image Image;

    public void SetIcon(Sprite icon)
    {
      Image.sprite = icon;
    }
  }
}