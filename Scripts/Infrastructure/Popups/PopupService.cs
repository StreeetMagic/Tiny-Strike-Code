using System;
using ConfigProviders;
using HeadsUpDisplays;
using HeadsUpDisplays.InterstitialAdvertisments;
using UnityEngine;
using Zenject;
using ZenjectFactories.ProjectContext;

namespace Popups
{
  public class PopupService : ITickable
  {
    private const float InternetPopupCooldown = 2f;

    private readonly ProjectZenjectFactory _factory;
    private readonly HeadsUpDisplayProvider _headsUpDisplayProvider;
    private readonly ArtConfigProvider _artConfigProvider;
    private readonly AdvertismentService _advertismentService;

    private float _timeLeft;

    public PopupService(ProjectZenjectFactory factory, HeadsUpDisplayProvider headsUpDisplayProvider,
      ArtConfigProvider artConfigProvider, AdvertismentService advertismentService)
    {
      _factory = factory;
      _headsUpDisplayProvider = headsUpDisplayProvider;
      _artConfigProvider = artConfigProvider;
      _advertismentService = advertismentService;

      _advertismentService.InterstitialAdRequested += OnInterstitialAdRequested;
      _advertismentService.RewardNotAvaliableDueToInternetDisconnection += OnRewardNotAvaliableDueToInternetDisconnection;
      _advertismentService.AdNotReady += OnAdNotReady;
    }

    private void OnAdNotReady()
    {
      Open(PopupId.AdvertismentNotReady); 
    }

    private void OnRewardNotAvaliableDueToInternetDisconnection()
    {
      Open(PopupId.NoInternetConnection);
    }

    private void OnInterstitialAdRequested(Action onComplete)
    {
      Open(PopupId.CoffeeBreak, onComplete);
    }

    public Popup Open(PopupId popupId, Action onComplete = null)
    {
      if (_timeLeft > 0)
        return null;

      _timeLeft = InternetPopupCooldown;
      
      Popup prefab = _artConfigProvider.Popups[popupId].Prefab;

      Popup popup = _factory.InstantiatePrefabForComponent(prefab);

      if (popupId == PopupId.CoffeeBreak)
      {
        var component = popup.GetComponent<AdvertismentsCoffeeBrakeTimerAnimation>();
        component.StartCountdown(onComplete);
      }

      popup.transform.SetParent(HudTransform(), false);
      popup.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

      return popup;
    }

    public void Tick()
    {
      if (_timeLeft > 0)
        _timeLeft -= Time.deltaTime;
    }

    private Transform HudTransform() =>
      _headsUpDisplayProvider.HeadsUpDisplay.GetComponentInChildren<Canvas>().transform;
  }
}