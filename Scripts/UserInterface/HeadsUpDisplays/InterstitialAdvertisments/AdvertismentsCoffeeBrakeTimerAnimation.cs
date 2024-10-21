using System;
using DG.Tweening;
using TimeServices;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace HeadsUpDisplays.InterstitialAdvertisments
{
  public class AdvertismentsCoffeeBrakeTimerAnimation : MonoBehaviour
  {
    [FormerlySerializedAs("countdownText")]
    public TextMeshProUGUI CountdownText;

    [FormerlySerializedAs("countdownImage")]
    public Image CountdownImage;
    
    [Inject]
    private TimeService _timeService;

    private float _countdownTime = 3f;
    
    public void StartCountdown(Action onComplete)
    {
      _timeService.Pause();
      
      DOTween.To(() => _countdownTime, x => _countdownTime = x, 0f, 3f)
        .OnUpdate(UpdateCountdownText)
        .OnComplete(() =>
        {
          CountdownText.text = "";
          onComplete?.Invoke();
          
          _timeService.UnPause();
          Destroy(gameObject);
        })
        .SetEase(Ease.Linear);
    }

    private void UpdateCountdownText()
    {
      int seconds = Mathf.CeilToInt(_countdownTime);
      CountdownText.text = "0:0" + seconds;

      AnimateImage();
    }

    private void AnimateImage()
    {
      CountdownImage.transform
        .DOScale(1.2f, 0.2f)
        .OnComplete(() =>
        {
          CountdownImage.transform
            .DOScale(1f, 0.2f);
        });

      CountdownImage.transform
        .DORotate(new Vector3(0, 0, 10f), 0.1f)
        .OnComplete(() =>
        {
          CountdownImage.transform
            .DORotate(new Vector3(0, 0, -10f), 0.2f)
            .OnComplete(() =>
            {
              CountdownImage.transform
                .DORotate(Vector3.zero, 0.1f);
            });
        });
    }
  }
}