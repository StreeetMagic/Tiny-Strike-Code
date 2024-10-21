using System.Collections.Generic;
using ConfigProviders;
using Meta.Currencies;
using Meta.Loots;
using Meta.Stats;
using Utilities;
using Zenject;

namespace Meta.BackpackStorages
{
  public class BackpackStorage : IInitializable
  {
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly PlayerStatsProvider _playerStatsProvider;

    public BackpackStorage(BalanceConfigProvider balanceConfigProvider,
      PlayerStatsProvider playerStatsProvider)
    {
      _balanceConfigProvider = balanceConfigProvider;
      _playerStatsProvider = playerStatsProvider;
    }

    public ReactiveList<LootDrop> LootDrops { get; } = new();

    public void Initialize()
    {
      LootDrops.Clear();
    }

    public void AddLoot(List<LootDrop> lootDrops)
    {
      foreach (LootDrop lootDrop in lootDrops)
        LootDrops.Add(lootDrop);
    }

    public Dictionary<CurrencyId, int> ReadLoot()
    {
      Dictionary<CurrencyId, int> loot = new();

      foreach (LootDrop lootDrop in LootDrops.Value)
      {
        LootConfig lootConfig = _balanceConfigProvider.Loots[lootDrop.Id];
        int value = lootConfig.Loots[lootDrop.Level - 1].Value;

        if (!loot.TryAdd(lootDrop.Id, value))
          loot[lootDrop.Id] += value;
      }

      return loot;
    }

    public bool IsFull() =>
      Volume() >= _playerStatsProvider.GetStat(StatId.BackpackCapacity);

    public bool IsEmpty() =>
      Volume() == 0;

    public int Volume()
    {
      int totalVolume = 0;

      for (var i = 0; i < LootDrops.Value.Count; i++)
      {
        LootDrop lootDrop = LootDrops.Value[i];
        var lootConfig = _balanceConfigProvider.Loots[lootDrop.Id];
        totalVolume += lootConfig.Loots[lootDrop.Level - 1].Volume;
      }

      return totalVolume;
    }
  }
}