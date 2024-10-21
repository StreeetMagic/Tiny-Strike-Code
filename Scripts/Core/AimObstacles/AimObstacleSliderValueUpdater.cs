using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.AimObstacles
{
  public class AimObstacleSliderValueUpdater : MonoBehaviour
  {
    public Slider Slider;
    public Slider WhiteSlider;
    public float SliderUpdateSpeed;
    public float WhiteSliderUpdateSpeed;

    [Inject] private IHealth _health;

    private void Update()
    {
      float value = _health.Current.Value / _health.Initial;

      if (Math.Abs(Slider.value - value) > 0.01f)
        Slider.value = value;

      if (Math.Abs(WhiteSlider.value - value) > 0.01f)
        WhiteSlider.value = Mathf.MoveTowards(WhiteSlider.value, value, Time.deltaTime * WhiteSliderUpdateSpeed);
    }
  }
}