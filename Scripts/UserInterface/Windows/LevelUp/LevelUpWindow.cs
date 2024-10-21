using System;
using System.Collections.Generic;
using Windows;
using ConfigProviders;
using Core.Characters.Players;
using Core.Weapons;
using ItemSlots;
using Meta.Currencies;
using Meta.Expirience;
using Meta.LevelUp;
using Meta.Rewards;
using SaveLoadServices;
using TimeServices;
using TMPro;
using UnityEngine;
using Zenject;
using ZenjectFactories.SceneContext;

public class LevelUpWindow : Window
{
  public TextMeshProUGUI LevelText;
  public Transform ItemSlotsContainer;

  private ExpierienceStorage _expierienceStorage;
  private TimeService _timeService;
  private ArtConfigProvider _artConfigProvider;
  private BalanceConfigProvider _balanceConfigProvider;
  private HubZenjectFactory _hubZenjectFactory;
  private WeaponStorage _weaponStorage;
  private CurrencyStorage _currencyStorage;
  private PlayerProvider _playerProvider;
  private AdvertismentService _advertismentService;
  private ISaveLoadService _saveLoadService;

  [Inject]
  private void Construct(ExpierienceStorage expierienceStorage, TimeService timeService, ArtConfigProvider artConfigProvider,
    BalanceConfigProvider balanceConfigProvider, HubZenjectFactory hubZenjectFactory, WeaponStorage weaponStorage, CurrencyStorage currencyStorage,
    PlayerProvider playerProvider, AdvertismentService advertismentService, ISaveLoadService saveLoadService)
  {
    _expierienceStorage = expierienceStorage;
    _timeService = timeService;
    _artConfigProvider = artConfigProvider;
    _balanceConfigProvider = balanceConfigProvider;
    _hubZenjectFactory = hubZenjectFactory;
    _weaponStorage = weaponStorage;
    _currencyStorage = currencyStorage;
    _playerProvider = playerProvider;
    _advertismentService = advertismentService;
    _saveLoadService = saveLoadService;
  }

  private readonly List<ItemSlot2> _itemSlots = new();

  public override void Initialize()
  {
    CloseButton.gameObject.SetActive(true);
    SetCurrentLevel();
    CreateItemSlots();
  }

  protected override void SubscribeUpdates()
  {
    _timeService.Pause("SubscribeUpdates");
  }

  protected override void Cleanup()
  {
    CleanItemSlots();
    _timeService.UnPause();

    if (_expierienceStorage.CurrentLevel() == 2)
    {
      _advertismentService.ShowFirstInterstitial();
      _saveLoadService.SaveProgress(nameof(LevelUpWindow));
    }
  }

  private void CleanItemSlots()
  {
    for (int i = 0; i < _itemSlots.Count; i++)
      Destroy(_itemSlots[i].gameObject);

    _itemSlots.Clear();
  }

  private void SetCurrentLevel()
  {
    LevelText.text = _expierienceStorage.CurrentLevel().ToString();
  }

  private void CreateItemSlots()
  {
    LevelUpReward[] rewards = _balanceConfigProvider.Expirience.Levels[_expierienceStorage.CurrentLevel() - 1].Rewards;

    for (int i = 0; i < rewards.Length; i++)
    {
      LevelUpReward reward = rewards[i];
      LevelUpRewardArtSetup artSetup = _artConfigProvider.LevelUpRewards[reward.Id];
      ItemSlot2 prefab = artSetup.ItemSlot;

      ItemSlot2 itemSlot = _hubZenjectFactory.InstantiatePrefabForComponent(prefab, ItemSlotsContainer);
      _itemSlots.Add(itemSlot);

      InitVisuals(reward, itemSlot, artSetup);

      GainReward(artSetup, reward);
    }
  }

  private void GainReward(LevelUpRewardArtSetup artSetup, LevelUpReward reward)
  {
    if (artSetup.IsWeapon)
      GainWeaponReward(reward);
    else
      GainCurrency(reward);
  }

  private void GainCurrency(LevelUpReward reward)
  {
    switch (reward.Id)
    {
      case LevelUpRewardId.Money:
        _currencyStorage.OnRewardGain(new CurrencyReward(CurrencyId.Money, reward.Count));
        break;

      case LevelUpRewardId.Key:
        _currencyStorage.OnRewardGain(new CurrencyReward(CurrencyId.Key, reward.Count));
        break;

      default:
      case LevelUpRewardId.Unknown:
      case LevelUpRewardId.Xm1014:
      case LevelUpRewardId.Ak47:
      case LevelUpRewardId.DesertEagle:
        throw new ArgumentOutOfRangeException();
    }
  }

  private void GainWeaponReward(LevelUpReward reward)
  {
    WeaponId weaponId;

    switch (reward.Id)
    {
      case LevelUpRewardId.DesertEagle:
        weaponId = WeaponId.DesertEagle;
        break;

      case LevelUpRewardId.Ak47:
        weaponId = WeaponId.Ak47;
        break;

      case LevelUpRewardId.Xm1014:
        weaponId = WeaponId.Xm1014;
        break;

      default:
      case LevelUpRewardId.Unknown:
      case LevelUpRewardId.Money:
      case LevelUpRewardId.Key:
        throw new ArgumentOutOfRangeException();
    }

    _weaponStorage.Add(weaponId);
    _playerProvider.Instance.WeaponIdProvider.CurrentId.Value = weaponId;
  }

  private void InitVisuals(LevelUpReward reward, ItemSlot2 itemSlot, LevelUpRewardArtSetup artSetup)
  {
    string text = artSetup.IsWeapon
      ? artSetup.WeaponName
      : reward.Count.ToString();

    itemSlot.Init(text, artSetup.Icon);
  }
}