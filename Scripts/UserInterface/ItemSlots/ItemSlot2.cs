using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ItemSlots
{
  public class ItemSlot2 : MonoBehaviour
  {
    public Image Image;
    public TextMeshProUGUI Text;

    public void Init(string text, Sprite sprite)
    {
      Text.text = text;
      Image.sprite = sprite;
    }
  }
}