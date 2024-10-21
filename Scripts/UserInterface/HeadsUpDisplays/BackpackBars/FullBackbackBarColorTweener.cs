using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming

namespace HeadsUpDisplays.BackpackBars
{
  public class FullBackbackBarColorTweener : MonoBehaviour
  {
    public Color startColor = Color.red;
    public Color endColor = Color.blue;
    public float duration = 2f;
    public Image image;

    private void Start()
    {
      // Начинаем сначала цвета
      TweenToColor(endColor);
    }

    private void TweenToColor(Color targetColor)
    {
      image
        .DOColor(targetColor, duration)
        .SetEase(Ease.Linear)
        .OnComplete(() => TweenToColor(targetColor == startColor ? endColor : startColor));
    }
  }
}