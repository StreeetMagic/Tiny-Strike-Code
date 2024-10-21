using System.Collections.Generic;
using AssetProviders;
using ConfigProviders;
using Core.LootSlots;
using Meta.BackpackStorages;
using Meta.Currencies;
using Meta.Loots;
using Prefabs;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays
{
  public class LootSlotsUpdater : MonoBehaviour
  {
    public List<LootSlot> LootSlots = new();

    [Inject] private LootSlotFactory _lootSlotFactory;
    [Inject] private BackpackStorage _backpackStorage;
    [Inject] private IAssetProvider _assetProvider;
    [Inject] private ArtConfigProvider _artConfigProvider;
    [Inject] private DevConfigProvider _devConfigProvider;

    private LootSlot Prefab =>
      _devConfigProvider.GetPrefabForComponent<LootSlot>(PrefabId.LootSlot);
    
    private void OnEnable()
    {
      _backpackStorage.LootDrops.Changed += OnLootDropsChanged;
    }

    private void OnDisable()
    {
      _backpackStorage.LootDrops.Changed -= OnLootDropsChanged;
    }

    private void OnLootDropsChanged(List<LootDrop> list)
    {
      foreach (LootSlot loot in LootSlots)
        Destroy(loot.gameObject);

      LootSlots.Clear();

      Dictionary<CurrencyId, int> info = _backpackStorage.ReadLoot();

      foreach (KeyValuePair<CurrencyId, int> loot in info)
        _lootSlotFactory.Create(loot.Key, Prefab, transform, loot.Value);
    }
  }
}