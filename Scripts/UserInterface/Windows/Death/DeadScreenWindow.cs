using Windows;
using Core.Characters.Players;
using Meta.BackpackStorages;
using Zenject;

public class DeadScreenWindow : Window
{
  [Inject]
  private PlayerProvider _playerProvider;

  [Inject]
  private BackpackStorage _playerBackpackStorage;

  [Inject]
  private WindowService _windowService;

  [Inject]
  private AdvertismentService _advertismentService;

  public override void Initialize()
  {
    CloseButton.onClick.AddListener(() =>
    {
      _playerBackpackStorage.LootDrops.Clear();
      _playerProvider.Instance.Health.HealMax();
      _playerProvider.Instance.TargetTrigger.Collider.enabled = true;
      _windowService.CloseCurrentWindow();
    });
  }

  protected override void SubscribeUpdates()
  {
    CloseButton.onClick.AddListener(() =>
    {
      _advertismentService.ShowInterstitialAfterDeath();
    });
  }

  protected override void Cleanup()
  {
    CloseButton.onClick.RemoveAllListeners();
  }
}