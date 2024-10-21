using Meta.Currencies;
using Projects;
using TMPro;
using UnityEngine;
using Zenject;

public class AdDebugText : MonoBehaviour
{
  public TextMeshProUGUI Text;

  private AdvertismentService _advertismentService;
  private ProjectData _projectData;
  private CurrencyStorage _currencyStorage;

  [Inject]
  private void Construct(AdvertismentService advertismentService, ProjectData projectData, CurrencyStorage currencyStorage)
  {
    _advertismentService = advertismentService;
    _projectData = projectData;
    _currencyStorage = currencyStorage;
  }

  private void Update()
  {
    float rewardSecondsLeft = _advertismentService.RewardTimeLeft;
    float interstitialSecondsLeft = _advertismentService.InterstitialTimeLeft;
    bool isUnityEditor = _projectData.IsUnityEditor;
    PlatformId platformId = _projectData.PlatformId;
    bool hasInternetConnection = _projectData.HasInternetConnection;
    int keyCount = _currencyStorage.Get(CurrencyId.Key).Value;

    Text.text = $"Reward: {rewardSecondsLeft}" +
                $"\nInterstitial: {interstitialSecondsLeft}" +
                $"\nIsUnityEditor: {isUnityEditor}" +
                $"\nPlatformId: {platformId}" +
                $"\nHasInternetConnection: {hasInternetConnection}" +
                $"\nKeyCount: {keyCount}" + 
                $"\nFirstInterShown : {_advertismentService.FirstInterstitialShown}"
      ;
  }
}