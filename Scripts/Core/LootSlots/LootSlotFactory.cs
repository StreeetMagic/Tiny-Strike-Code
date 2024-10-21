using System.Collections.Generic;
using ConfigProviders;
using HeadsUpDisplays;
using Meta.Currencies;
using Meta.Loots;
using Prefabs;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace Core.LootSlots
{
  public class LootSlotFactory
  {
    private readonly HubZenjectFactory _factory;
    private readonly ArtConfigProvider _artConfigProvider;
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly HeadsUpDisplayProvider _headsUpDisplayProvider;

    public LootSlotFactory(HubZenjectFactory factory, ArtConfigProvider artConfigProvider, 
      BalanceConfigProvider balanceConfigProvider, HeadsUpDisplayProvider headsUpDisplayProvider)
    {
      _factory = factory;
      _balanceConfigProvider = balanceConfigProvider;
      _headsUpDisplayProvider = headsUpDisplayProvider;
      _artConfigProvider = artConfigProvider;
    }

    public void Create(Transform parent, List<LootDrop> list)
    {
      Dictionary<CurrencyId, int> lootData = new();

      foreach (LootDrop item in list)
      {
        List<Loot> loots = _balanceConfigProvider.Loots[item.Id].Loots;
        int itemLevel = item.Level - 1;

        int count = loots[itemLevel].Value;
        lootData.Add(item.Id, count);
      }

      foreach (KeyValuePair<CurrencyId, int> item in lootData)
      {
     //   EnemyLootSlot slot = _factory.InstantiateGameObject(PrefabId.EnemyLootSlot, parent).GetComponent<EnemyLootSlot>();
        LootSlot slot = _factory.InstantiatePrefabForComponent<LootSlot>(PrefabId.EnemyLootSlot, parent);
        Sprite sprite = _artConfigProvider.Currencies[item.Key].Sprite;

        slot.Init(sprite, item.Value);
      }
    }
    
    public void Create(CurrencyId id, LootSlot prefab, Transform parent, int lootValue)
    {
      var slot = _factory.InstantiatePrefabForComponent(prefab, parent);
      _headsUpDisplayProvider.LootSlotsUpdater.LootSlots.Add(slot);

      Sprite icon = _artConfigProvider.Currencies[id].Sprite;

      slot.Init(icon, lootValue);
    }
  }
}