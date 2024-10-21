using System;
using Windows;
using Buttons;
using Core.Characters.Players;
using Popups;
using TMPro;
using UnityEngine;
using Zenject;

public class DeadScreenAdRewardReviveButton : BaseButton
{
  public GameObject RewardAdButton;
  public GameObject RewardTimerButton;
  public TextMeshProUGUI TimerText;

  private PlayerProvider _playerProvider;
  private WindowService _windowService;
  private AdvertismentService _advertismentService;
  private PopupService _popupService;

  [Inject]
  private void Construct(PlayerProvider playerProvider,
    WindowService windowService, AdvertismentService advertismentService, PopupService popupService)
  {
    _playerProvider = playerProvider;
    _windowService = windowService;
    _advertismentService = advertismentService;
    _popupService = popupService;
  }

  private void Start()
  {
    Button.onClick.AddListener(OnClick);
  }

  private void Update()
  {
    if (_advertismentService.RewardTimeLeft > 0)
    {
      Button.interactable = false;

      TimerText.text = TimeSpan
        .FromSeconds(_advertismentService.RewardTimeLeft)
        .ToString(@"m\:ss");

      if (RewardTimerButton.activeSelf == false)
      {
        RewardTimerButton.SetActive(true);
      }

      if (RewardAdButton.activeSelf)
        RewardAdButton.SetActive(false);
    }
    else
    {
      Button.interactable = true;

      if (RewardTimerButton.activeSelf)
        RewardTimerButton.SetActive(false);

      if (RewardAdButton.activeSelf == false)
        RewardAdButton.SetActive(true);
    }
  }

  private void OnClick()
  {
    _advertismentService.ShowRewardedVideo(() =>
    {
      _playerProvider.Instance.Health.HealMax();
      _playerProvider.Instance.TargetTrigger.Collider.enabled = true;
      _windowService.CloseCurrentWindow();
    });
  }
}