using Windows;
using Core.Characters.Players;
using FullRewardChestOpen;
using Meta.ChestRewards;
using Meta.Currencies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Meta.Chests
{
  public class Chest : MonoBehaviour
  {
    private const float DistanceToPlayer = 3f;

    public int ExpierenceReward;
    public int MoneyReward;
    public float TimeToOpen = 2f;
    public Image SliderImage1;
    public Image SliderImage2;

    public GameObject OpenByKeyContainer;
    public GameObject OpenByAdvContainer;

    private AdvertismentService _advertismentService;
    private CurrencyStorage _currencyStorage;
    private PlayerProvider _playerProvider;
    private WindowService _windowService;

    [Inject]
    private void Construct(CurrencyStorage currencyStorage, PlayerProvider playerProvider, WindowService windowService, AdvertismentService advertismentService)
    {
      _currencyStorage = currencyStorage;
      _playerProvider = playerProvider;
      _windowService = windowService;
      _advertismentService = advertismentService;
    }

    public bool IsOpened { get; private set; }
    public bool IsActive { get; private set; }
    private float _timeLeft;

    private void Update()
    {
      if (IsOpened)
        return;

      if (!_playerProvider.Instance)
        return;

      Toggle();

      bool isNear = Vector3.Distance(transform.position, _playerProvider.Instance.transform.position) < DistanceToPlayer;

      if (isNear)
      {
        if (HasKey())
        {
          _timeLeft -= Time.deltaTime;
          UpdateSlider();

          if (_timeLeft <= 0)
          {
            if (!IsOpened)
            {
              _currencyStorage.Spend(CurrencyId.Key, 1);
              IsOpened = true;
              OpenWindow();
            }
          }
        }
        else
        {
          if (!IsOpened)
          {
            _timeLeft -= Time.deltaTime;
            UpdateSlider();

            if (_timeLeft <= 0)
            {
              var shown = _advertismentService.ShowRewardedVideo(() => { OpenWindow(); });
              
              IsOpened = true;
            }
          }
          else
          {
            _timeLeft = TimeToOpen;
            UpdateSlider();
          }
        }
      }
      else
      {
        _timeLeft = TimeToOpen;
        UpdateSlider();
      }
    }

    public void Enable()
    {
      IsOpened = false;
      IsActive = true;
      _timeLeft = TimeToOpen;
      gameObject.SetActive(true);
    }

    public void Disable()
    {
      IsActive = false;
      gameObject.SetActive(false);
    }

    private void UpdateSlider()
    {
      SliderImage1.fillAmount = (TimeToOpen - _timeLeft) / TimeToOpen;
      SliderImage2.fillAmount = (TimeToOpen - _timeLeft) / TimeToOpen;
    }

    private void OpenWindow()
    {
      Window window = _windowService.Open(WindowId.FullRewardChestOpenWindow);

      FullRewardChestOpenWindow chestWindow = (FullRewardChestOpenWindow)window;

      var chestRewards = new ChestReward[]
      {
        new(ChestRewardId.Money, MoneyReward),
        new(ChestRewardId.Exprience, ExpierenceReward)
      };

      chestWindow.CreateItemSlots(chestRewards);
    }

    private void Toggle()
    {
      if (HasKey())
      {
        if (OpenByKeyContainer.activeSelf == false)
          OpenByKeyContainer.SetActive(true);

        if (OpenByAdvContainer.activeSelf)
          OpenByAdvContainer.SetActive(false);
      }
      else
      {
        if (OpenByKeyContainer.activeSelf)
          OpenByKeyContainer.SetActive(false);

        if (OpenByAdvContainer.activeSelf == false)
          OpenByAdvContainer.SetActive(true);
      }
    }

    private bool HasKey()
    {
      return _currencyStorage.Get(CurrencyId.Key).Value > 0;
    }
  }
}