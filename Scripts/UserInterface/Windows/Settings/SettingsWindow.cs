using Windows;
using AudioServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsWindow : Window
{
  private const float SnapThreshold = 0.02f;
    
  public Slider MusicSlider;
  public Slider SoundEffectsSlider;

  private float _minSliderValue;
  private float _maxSliderValue;

  [Inject] private AudioService _audioService;

  public override void Initialize()
  {
    _minSliderValue = AudioService.SliderMutedLoudness;
    _maxSliderValue = 0;

    float currentMusicLoudness = _audioService.MusicLoudness;
    float currentSoundEffectsLoudness = _audioService.SoundEffectsLoudness;

    MusicSlider.value = ConvertToSliderValue(currentMusicLoudness);
    SoundEffectsSlider.value = ConvertToSliderValue(currentSoundEffectsLoudness);
  }

  protected override void SubscribeUpdates()
  {
    MusicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
    SoundEffectsSlider.onValueChanged.AddListener(OnSoundEffectsSliderChanged);
  }

  protected override void Cleanup()
  {
    MusicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
    SoundEffectsSlider.onValueChanged.RemoveListener(OnSoundEffectsSliderChanged);
  }

  private void OnMusicSliderChanged(float value)
  {
    value = ApplySnapEffect(value);
    MusicSlider.value = value; 
    float loudness = ConvertToLoudnessValue(value);
    _audioService.SetMusicLoudness(loudness);
  }

  private void OnSoundEffectsSliderChanged(float value)
  {
    value = ApplySnapEffect(value);
    SoundEffectsSlider.value = value;
    float loudness = ConvertToLoudnessValue(value);
    _audioService.SetSoundEffectsLoudness(loudness);
  }

  private float ApplySnapEffect(float value)
  {
    if (value < SnapThreshold)
    {
      value = 0;
    }
    else if (value > 1 - SnapThreshold)
    {
      value = 1;
    }

    return value;
  }

  private float ConvertToLoudnessValue(float sliderValue)
  {
    return Mathf.Lerp(_minSliderValue, _maxSliderValue, sliderValue);
  }

  private float ConvertToSliderValue(float loudness)
  {
    return Mathf.InverseLerp(_minSliderValue, _maxSliderValue, loudness);
  }
}