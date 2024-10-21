using UnityEngine;
using UnityEngine.UI;

namespace ItemSlots
{
  public class ItemSlot : MonoBehaviour
  {
    public Image Image;

    public void Init(Sprite sprite)
    {
      Image.sprite = sprite;
    }
  }
}