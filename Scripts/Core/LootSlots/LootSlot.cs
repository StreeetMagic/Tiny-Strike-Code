using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.LootSlots
{
  public class LootSlot : MonoBehaviour
  {
    public Image Image;
    public TextMeshProUGUI Text;

    public void Init(Sprite sprite, int itemValue)
    {
      Image.sprite = sprite;
      Text.text = itemValue.ToString();
    }
  }
}