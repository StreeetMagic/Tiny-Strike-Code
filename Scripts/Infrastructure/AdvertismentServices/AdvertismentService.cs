using System;
using AudioServices;
using CAS;
using PersistentProgresses;
using Projects;
using SaveLoadServices;
using TimeServices;
using UnityEngine;
using Zenject;

public partial class AdvertismentService : ITickable, IProgressWriter
{
  private const float RewardCooldown = 45f;
  private const float InterstitialCooldown = 90f;
  private const float InterstitialAfterRewardCooldown = 30f;
  private const float AskTimerCooldown = 1f;

  private readonly AudioService _audioService;
  private readonly TimeService _timeService;
  private readonly ProjectData _projectData;

  private IMediationManager _cas;
  private Action _onVideoFinishedReward;
  private bool _userRewardEarned;
  private float _askTimerLeft;

  private bool _isLogging;

  public AdvertismentService(AudioService audioService, TimeService timeService, ProjectData projectData)
  {
    _audioService = audioService;
    _timeService = timeService;
    _projectData = projectData;
  }

  public event Action<Action> InterstitialAdRequested;
  public event Action RewardNotAvaliableDueToInternetDisconnection;
  public event Action AdNotReady;

  public bool FirstInterstitialShown { get; private set; }
  public float RewardTimeLeft { get; private set; }
  public float InterstitialTimeLeft { get; private set; }

  private bool IsRewardedVideoLoaded { get; set; }

  private bool IsInterstitialVideoLoaded { get; set; }

  public void InitCas()
  {
    _cas = MobileAds.BuildManager().Build();

    InterstitialTimeLeft = InterstitialCooldown;

    _cas.OnRewardedAdClosed += OnRewardedAdClosed;
    _cas.OnRewardedAdShown += OnRewardedAdShown;
    _cas.OnRewardedAdCompleted += OnRewardedAdCompleted;
    _cas.OnRewardedAdLoaded += OnRewardedAdLoaded;
    _cas.OnRewardedAdClicked += OnRewardedAdClicked;
    _cas.OnRewardedAdFailedToLoad += OnRewardedAdFailedToLoad;
    _cas.OnRewardedAdFailedToShow += OnRewardedAdFailedToShow;

    _cas.OnRewardedAdImpression += OnRewardedAdImpression;

    _cas.OnInterstitialAdClosed += OnInterstitialAdClosed;
    _cas.OnInterstitialAdShown += OnInterstitialAdShown;
    _cas.OnInterstitialAdLoaded += OnInterstitialAdLoaded;
    _cas.OnInterstitialAdClicked += OnInterstitialAdClicked;
    _cas.OnInterstitialAdFailedToLoad += OnInterstitialAdFailedToLoad;
    _cas.OnInterstitialAdFailedToShow += OnInterstitialAdFailedToShow;

    _cas.OnInterstitialAdImpression += OnInterstitialAdImpression;
  }

  public void ReadProgress(ProjectProgress projectProgress)
  {
    FirstInterstitialShown = projectProgress.FirstInterstitialShown;
  }

  public void WriteProgress(ProjectProgress projectProgress)
  {
    projectProgress.FirstInterstitialShown = FirstInterstitialShown;
  }

  public void Tick()
  {
    if (_timeService.IsPaused)
      return;

    if (RewardTimeLeft > 0)
    {
      RewardTimeLeft -= Time.deltaTime;

      if (RewardTimeLeft <= 0)
        RewardTimeLeft = 0;
    }

    if (InterstitialTimeLeft > 0)
    {
      InterstitialTimeLeft -= Time.deltaTime;

      if (InterstitialTimeLeft <= 0)
        InterstitialTimeLeft = 0;
    }

    if (_askTimerLeft > 0)
    {
      _askTimerLeft -= Time.deltaTime;

      if (_askTimerLeft <= 0)
        _askTimerLeft = 0;
    }

    if (InterstitialTimeLeft == 0)
    {
      if (_askTimerLeft == 0)
      {
        if (_isLogging)
          Debug.Log("CAS IsReadyAd(AdType.Interstitial)");

        if (_projectData.IsUnityEditor)
        {
          if (_cas.IsReadyAd(AdType.Interstitial))
            InvokeInterstital();
        }
        else
        {
          if (_projectData.HasInternetConnection)
            if (_cas.IsReadyAd(AdType.Interstitial))
              InvokeInterstital();
        }

        _askTimerLeft = AskTimerCooldown;
      }
    }
  }

  public void ShowFirstInterstitial()
  {
    if (FirstInterstitialShown)
      return;

    if (!_projectData.HasInternetConnection)
      return;

    if (!_cas.IsReadyAd(AdType.Interstitial))
    {
      Debug.LogWarning("_cas.IsReadyAd(AdType.Interstitial) false");
      return;
    }

    if (!IsInterstitialVideoLoaded)
    {
      Debug.LogWarning("IsInterstitialVideoLoaded false");
      return;
    }

    FirstInterstitialShown = true;

    InterstitialTimeLeft = InterstitialAfterRewardCooldown + 1;

    InterstitialAdRequested?.Invoke(() => _cas.ShowAd(AdType.Interstitial));
  }

  public void ShowInterstitialAfterDeath()
  {
    if (!_projectData.HasInternetConnection)
      return;

    if (!_cas.IsReadyAd(AdType.Interstitial))
    {
      Debug.LogWarning("_cas.IsReadyAd(AdType.Interstitial) false");
      return;
    }

    if (!IsInterstitialVideoLoaded)
    {
      Debug.LogWarning("IsInterstitialVideoLoaded false");
      return;
    }

    InterstitialTimeLeft = InterstitialAfterRewardCooldown + 1;

    InterstitialAdRequested?.Invoke(() => _cas.ShowAd(AdType.Interstitial));
  }

  private void InvokeInterstital()
  {
    if (!_cas.IsReadyAd(AdType.Interstitial))
    {
      Debug.LogWarning("_cas.IsReadyAd(AdType.Interstitial) false");
      return;
    }

    if (!IsInterstitialVideoLoaded)
    {
      Debug.LogWarning("IsInterstitialVideoLoaded false");
      return;
    }

    InterstitialTimeLeft = InterstitialAfterRewardCooldown + 1;

    InterstitialAdRequested?.Invoke(() => _cas.ShowAd(AdType.Interstitial));
  }

  public bool ShowRewardedVideo(Action onVideoFinished)
  {
    if (!_projectData.HasInternetConnection)
    {
      RewardNotAvaliableDueToInternetDisconnection?.Invoke();
      return false;
    }

    if (!_cas.IsReadyAd(AdType.Rewarded))
    {
      AdNotReady?.Invoke();
      return false;
    }

    _onVideoFinishedReward = onVideoFinished;
    _cas.ShowAd(AdType.Rewarded);
    return true;
  }
}

public partial class AdvertismentService
{
  /// <summary>
  /// ЛЮБОЕ ЗАКРЫТИЕ РЕВАРД РЕКЛАМЫ
  /// </summary>
  private void OnRewardedAdClosed()
  {
    if (_isLogging)
      Debug.Log("CAS RewardedAdClosed");

    _userRewardEarned = false;
    _onVideoFinishedReward = null;

    if (InterstitialTimeLeft <= InterstitialAfterRewardCooldown)
      InterstitialTimeLeft = InterstitialAfterRewardCooldown;

    _audioService.UnmuteMasterMixer();
    _timeService.UnPause();
  }

  /// <summary>
  /// НАЖАЛИ НА ПОЛУЧЕНИЕ НАГРАДЫ
  /// </summary>
  private void OnRewardedAdClicked()
  {
    if (_isLogging)
      Debug.Log("CAS RewardedAdClicked");
  }

  /// <summary>
  ///  ОШИБКА ПОКАЗА РЕКЛАМЫ
  /// </summary>
  private void OnRewardedAdFailedToShow(string error)
  {
    if (_isLogging)
      Debug.Log("CAS RewardedAdFailedToShow " + error);

    // _headsUpDisplayProvider.HeadsUpDisplay.ShowAdvertismentIsNotAvailablePopup();

    _userRewardEarned = false;
    _onVideoFinishedReward = null;
  }

  /// <summary>
  ///  УДАЧНЫЙ ПОКАЗ РЕКЛАМЫ (ПОЛУЧАЕМ НАГРАДУ)
  /// </summary>
  private void OnRewardedAdCompleted()
  {
    if (_isLogging)
      Debug.Log("CAS RewardedAdCompleted");

    if (_onVideoFinishedReward == null)
      return;

    _userRewardEarned = true;

    RewardTimeLeft = RewardCooldown;

    _onVideoFinishedReward?.Invoke();
    _onVideoFinishedReward = null;
  }

  private void OnRewardedAdShown()
  {
    if (_isLogging)
      Debug.Log("CAS RewardedAdShown");

    _audioService.MuteMasterMixer();

    _timeService.Pause(" OnRewardedAdShown");

    RewardTimeLeft = RewardCooldown;
  }

  private void OnRewardedAdLoaded()
  {
    if (_isLogging)
      Debug.Log("CAS RewardedAdLoaded");

    IsRewardedVideoLoaded = true;
  }

  private void OnRewardedAdImpression(AdMetaData meta)
  {
    if (_isLogging)
      Debug.Log("CAS RewardedAdImpression");
  }

  private void OnRewardedAdFailedToLoad(AdError error)
  {
    if (_isLogging)
      Debug.Log("CAS RewardedAdFailedToLoad " + error);
  }
}

public partial class AdvertismentService
{
  private void OnInterstitialAdClosed()
  {
    if (_isLogging)
      Debug.Log("CAS InterstitialAdClosed");

    IsInterstitialVideoLoaded = false;

    _timeService.UnPause();
    _audioService.UnmuteMasterMixer();
  }

  private void OnInterstitialAdShown()
  {
    if (_isLogging)
      Debug.Log("CAS InterstitialAdShown");

    InterstitialTimeLeft = InterstitialCooldown;

    IsInterstitialVideoLoaded = false;

    _timeService.Pause(" OnInterstitialAdShown");
    _audioService.MuteMasterMixer();
  }

  private void OnInterstitialAdLoaded()
  {
    // if (_isLogging)
    Debug.Log("CAS InterstitialAdLoaded");

    IsInterstitialVideoLoaded = true;
  }

  private void OnInterstitialAdClicked()
  {
    if (_isLogging)
      Debug.Log("CAS InterstitialAdClicked");

    IsInterstitialVideoLoaded = false;
  }

  private void OnInterstitialAdImpression(AdMetaData meta)
  {
    if (_isLogging)
      Debug.Log("CAS InterstitialAdImpression");

    IsInterstitialVideoLoaded = false;
  }

  private void OnInterstitialAdFailedToLoad(AdError error)
  {
    if (_isLogging)
      Debug.Log("CAS InterstitialAdFailedToLoad " + error);
  }

  private void OnInterstitialAdFailedToShow(string error)
  {
    if (_isLogging)
      Debug.Log("CAS InterstitialAdFailedToShow " + error);
  }
}