using DG.Tweening;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace HeadsUpDisplays.BackpackBars
{
  public class BackpackBarBounceEffect : MonoBehaviour
  {
    public RectTransform slider;
    public float bounceStrength = 0.3f;
    public float bounceSpeed = 0.1f;

    private bool _isAnimating;

    public void ApplyBounceEffect()
    {
      if (_isAnimating)
      {
        return;
      }

      _isAnimating = true;

      slider.transform
        .DOPunchScale(Vector3.one * bounceStrength, bounceSpeed, vibrato: 1, elasticity: 0)
        .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
          slider.transform.localScale = Vector3.one;
          _isAnimating = false;
        });
    }
  }
}