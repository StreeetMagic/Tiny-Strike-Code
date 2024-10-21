using UnityEngine;
using UnityEngine.UI;
using CAS;
// ReSharper disable InconsistentNaming

public class CSharpSampleController : MonoBehaviour
{
  public Text versionText;
  public Text interstitialStatus;
  public Text rewardedStatus;

  public Text appReturnButtonText;

  private bool isAppReturnEnable;
  private bool userRewardEarned;

  private IMediationManager manager;
  private IAdView bannerView;

  public void Start()
  {
    versionText.text = MobileAds.wrapperVersion;

    // -- Create manager:
    manager = MobileAds.BuildManager().Build();

    // -- Subscribe to CAS events:
    manager.OnInterstitialAdLoaded += OnInterstitialAdLoaded;
    manager.OnInterstitialAdFailedToLoad += OnInterstitialAdFailedToLoad;

    manager.OnRewardedAdLoaded += OnRewardedAdLoaded;
    manager.OnRewardedAdFailedToLoad += OnRewardedAdFailedToLoad;
    manager.OnRewardedAdCompleted += OnRewardedAdCompleted;
    manager.OnRewardedAdClosed += OnRewardedAdClosed;
    // Any other events in IMediationManager
  }

  private void OnDestroy()
  {
    // -- Unsubscribe from CAS events:
    manager.OnInterstitialAdLoaded -= OnInterstitialAdLoaded;
    manager.OnInterstitialAdFailedToLoad -= OnInterstitialAdFailedToLoad;

    manager.OnRewardedAdLoaded -= OnRewardedAdLoaded;
    manager.OnRewardedAdFailedToLoad -= OnRewardedAdFailedToLoad;
    manager.OnRewardedAdCompleted -= OnRewardedAdCompleted;
    manager.OnRewardedAdClosed += OnRewardedAdClosed;
  }

  public void ShowInterstitial()
  {
    manager.ShowAd(AdType.Interstitial);
  }

  public void ShowRewarded()
  {
    userRewardEarned = false;
    manager.ShowAd(AdType.Rewarded);
  }

  public void ChangeAppReturnState()
  {
    isAppReturnEnable = !isAppReturnEnable;
    appReturnButtonText.text = isAppReturnEnable ? "ENABLED" : "DISABLED";
    manager.SetAppReturnAdsEnabled(isAppReturnEnable);
  }

  #region Events

  private void OnRewardedAdCompleted()
  {
    userRewardEarned = true;
    rewardedStatus.text = "User reward earned";
  }

  private void OnRewardedAdClosed()
  {
    if (!userRewardEarned)
      rewardedStatus.text = "User are not rewarded";
  }

  private void OnRewardedAdLoaded()
  {
    rewardedStatus.text = "Ready";
  }

  private void OnRewardedAdFailedToLoad(AdError error)
  {
    rewardedStatus.text = error.GetMessage();
  }

  private void OnInterstitialAdFailedToLoad(AdError error)
  {
    interstitialStatus.text = error.GetMessage();
  }

  private void OnInterstitialAdLoaded()
  {
    interstitialStatus.text = "Ready";
  }

  #endregion
}