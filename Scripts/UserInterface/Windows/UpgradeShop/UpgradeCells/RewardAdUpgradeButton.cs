using System;
using Buttons;
using Meta.Stats;
using Meta.Upgrades;
using Popups;
using TMPro;
using UnityEngine;
using Zenject;

namespace UpgradeCells
{
  public class RewardAdUpgradeButton : BaseButton
  {
    public GameObject RewardAdButton;
    public GameObject RewardTimerButton;
    public TextMeshProUGUI TimerText;

    public UpgradeCell UpgradeCell;

    private UpgradeService _upgradeService;
    private AdvertismentService _advertismentService;
    private PopupService _popupService;

    [Inject]
    public void Construct(UpgradeService upgradeService,
      AdvertismentService advertismentService, PopupService popupService)
    {
      _upgradeService = upgradeService;
      _advertismentService = advertismentService;
      _popupService = popupService;
    }

    private StatId Id => UpgradeCell.UpgradeConfig.Id;

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
      _advertismentService.ShowRewardedVideo(() => { _upgradeService.GainUpgradeByAd(Id); });
    }
  }
}