using System.Collections.Generic;
using Windows;
using ConfigProviders;
using ItemSlots;
using Meta.ChestRewards;
using Meta.Currencies;
using Meta.Expirience;
using Meta.Rewards;
using TimeServices;
using UnityEngine;
using Zenject;
using ZenjectFactories.SceneContext;

namespace FullRewardChestOpen
{
  public class FullRewardChestOpenWindow : Window
  {
    public Transform Container;

    private ExpierienceStorage _expierienceStorage;
    private TimeService _timeService;
    private ArtConfigProvider _artConfigProvider;
    private HubZenjectFactory _hubZenjectFactory;
    private CurrencyStorage _currencyStorage;

    [Inject]
    private void Construct(ExpierienceStorage expierienceStorage, TimeService timeService,
      ArtConfigProvider artConfigProvider,
      HubZenjectFactory hubZenjectFactory, CurrencyStorage currencyStorage)
    {
      _expierienceStorage = expierienceStorage;
      _timeService = timeService;
      _artConfigProvider = artConfigProvider;
      _hubZenjectFactory = hubZenjectFactory;
      _currencyStorage = currencyStorage;
    }

    private readonly List<ItemSlot2> _itemSlots = new();

    public override void Initialize()
    {
      CloseButton.gameObject.SetActive(true);
    }

    protected override void SubscribeUpdates()
    {
      _timeService.Pause(" FullRewardChestOpenWindow SubscribeUpdates");
    }

    public void CreateItemSlots(ChestReward[] rewards)
    {
      for (int i = 0; i < rewards.Length; i++)
      {
        ChestReward reward = rewards[i];
        ChestRewardArtSetup artSetup = _artConfigProvider.ChestRewards[reward.Id];
        ItemSlot2 prefab = artSetup.ItemSlot;

        ItemSlot2 itemSlot = _hubZenjectFactory.InstantiatePrefabForComponent(prefab, Container);
        InitVisuals(reward, itemSlot, artSetup);
        _itemSlots.Add(itemSlot);

        GainReward(reward);
      }
    }

    protected override void Cleanup()
    {
      for (var i = 0; i < _itemSlots.Count; i++)
      {
        ItemSlot2 itemSlot = _itemSlots[i];

        if (itemSlot)
          Destroy(itemSlot.gameObject);
      }

      _itemSlots.Clear();
      _timeService.UnPause();
    }

    private void InitVisuals(ChestReward reward, ItemSlot2 itemSlot, ChestRewardArtSetup artSetup)
    {
      itemSlot.Init(reward.Count.ToString(), artSetup.Icon);
    }

    private void GainReward(ChestReward reward)
    {
      if (reward.Id == ChestRewardId.Money)
        GainMoney(reward);

      if (reward.Id == ChestRewardId.Exprience)
        GainExp(reward);
    }

    private void GainExp(ChestReward reward)
    {
      _expierienceStorage.AllPoints.Value += reward.Count;
    }

    private void GainMoney(ChestReward reward)
    {
      _currencyStorage.OnRewardGain(new CurrencyReward(CurrencyId.Money, reward.Count));
    }
  }
}