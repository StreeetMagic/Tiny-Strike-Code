using Windows;
using Core.Characters.Players;
using Core.Spawners.Enemies;
using Core.Weapons;
using Meta.Currencies;
using Meta.Expirience;
using Meta.Stats;
using SaveLoadServices;
using Scenes.Hubs;
using UnityEngine.UI;
using Zenject;

namespace Cheats
{
  public class CheatWindow : Window
  {
    public Button AddHealthButton;
    public Button AddMoveSpeedButton;
    public Button ImmortalButton;
    public Button AddDamageButton;
    public Button KnifeWeaponButton;
    public Button DeagleWeaponButton;
    public Button Ak47WeaponButton;

    // ReSharper disable once InconsistentNaming
    public Button XMWeaponButton;

    public Button AddMoneyButton;
    public Button AddKeysButton;
    public Button AddExpirienceButton;

    public Button RestartButton;
    public Button ClearSavesButton;

    [Inject]
    private PlayerStatsProvider _playerStatsProvider;

    [Inject]
    private PlayerProvider _playerProvider;

    [Inject]
    private WeaponStorage _playerWeaponStorage;

    [Inject]
    private CurrencyStorage _currencyStorage;

    [Inject]
    private ExpierienceStorage _expierienceStorage;

    [Inject]
    private HubInitializer _hubInitializer;

    [Inject]
    private EnemySpawnerProvider _enemySpawnerProvider;

    [Inject]
    private ISaveLoadService _saveLoadService;

    public override void Initialize()
    {
      ClearSavesButton.onClick.AddListener(() =>
      {
        _saveLoadService.DeleteSaves();
        _hubInitializer.Restart();
      });

      AddHealthButton.onClick.AddListener(() =>
      {
        _playerStatsProvider.AddCheatAdditiveStat(StatId.Health, 100);
        _playerProvider.Instance.Health.Current.Value += 100;
      });

      AddMoveSpeedButton.onClick.AddListener(() => { _playerStatsProvider.AddCheatAdditiveStat(StatId.MoveSpeed, 1); });

      ImmortalButton.onClick.AddListener(() =>
      {
        if (_playerProvider.Instance.Health.IsImmortal)
          _playerProvider.Instance.Health.IsImmortal = false;
        else
          _playerProvider.Instance.Health.IsImmortal = true;
      });

      AddDamageButton.onClick.AddListener(() => { _playerStatsProvider.AddCheatAdditiveStat(StatId.AdditionalDamage, 5); });

      KnifeWeaponButton.onClick.AddListener(() =>
      {
        _playerWeaponStorage.Add(WeaponId.Knife);
        _playerProvider.Instance.WeaponIdProvider.CurrentId.Value = WeaponId.Knife;
      });

      DeagleWeaponButton.onClick.AddListener(() =>
      {
        _playerWeaponStorage.Add(WeaponId.DesertEagle);
        _playerProvider.Instance.WeaponIdProvider.CurrentId.Value = WeaponId.DesertEagle;
      });

      Ak47WeaponButton.onClick.AddListener(() =>
      {
        _playerWeaponStorage.Add(WeaponId.Ak47);
        _playerProvider.Instance.WeaponIdProvider.CurrentId.Value = WeaponId.Ak47;
      });

      XMWeaponButton.onClick.AddListener(() =>
      {
        _playerWeaponStorage.Add(WeaponId.Xm1014);
        _playerProvider.Instance.WeaponIdProvider.CurrentId.Value = WeaponId.Xm1014;
      });

      AddMoneyButton.onClick.AddListener(() => { _currencyStorage.Get(CurrencyId.Money).Value += 50000; });
      AddKeysButton.onClick.AddListener(() => { _currencyStorage.Get(CurrencyId.Key).Value += 1; });
      //AddChestButton.onClick.AddListener(() => { _currencyStorage.Get(CurrencyId.Chest).Value += 1; });
      AddExpirienceButton.onClick.AddListener(() => { _expierienceStorage.AllPoints.Value += 100; });

      RestartButton.onClick.AddListener(() => { _hubInitializer.Restart(); });
    }

    protected override void SubscribeUpdates()
    {
    }

    protected override void Cleanup()
    {
    }
  }
}