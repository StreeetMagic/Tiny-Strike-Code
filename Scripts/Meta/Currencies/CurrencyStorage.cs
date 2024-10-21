using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Rewards;
using PersistentProgresses;
using SaveLoadServices;
using Utilities;

namespace Meta.Currencies
{
  public class CurrencyStorage : IProgressWriter
  {
    private readonly Dictionary<CurrencyId, ReactiveProperty<int>> _currencies;

    public CurrencyStorage()
    {
      _currencies = Enum.GetValues(typeof(CurrencyId))
        .Cast<CurrencyId>()
        .Where(id => id != CurrencyId.Unknown)
        .ToDictionary(id => id, _ => new ReactiveProperty<int>(0));
    }

    public event Action<CurrencyId> OnChestOrKeyAdded;

    public ReactiveProperty<int> LootDropedFromBackpack { get; } = new();
    // public ReactiveProperty<bool> HasChestsOrKeys { get; } = new(false);

    public void ReadProgress(ProjectProgress projectProgress)
    {
      foreach (CurrencyProgress currency in projectProgress.Currencies)
        SetCurrencyValue(_currencies[currency.Id], currency.Count);

      SetCurrencyValue(LootDropedFromBackpack, projectProgress.LootDropedFromBackpack);
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      List<CurrencyProgress> currencies =
        _currencies
          .Select(keyValuePair => new CurrencyProgress(keyValuePair.Key, keyValuePair.Value.Value))
          .ToList();

      projectProgress.Currencies = currencies;

      projectProgress.LootDropedFromBackpack = LootDropedFromBackpack.Value;
    }

    public ReactiveProperty<int> Get(CurrencyId currencyId)
    {
      return _currencies[currencyId];
    }

    public void OnRewardGain(CurrencyReward reward)
    {
      AddCurrencyValue(Get(reward.Id), reward.Quantity);

      if (reward.Id is CurrencyId.Key)
      {
      }
    }

    public void OnRewardedVideoCompleted(CurrencyId currencyId, int amount)
    {
      AddCurrencyValue(Get(currencyId), amount);
    }

    public void ApplyBackpackLoot(Dictionary<CurrencyId, int> backPackLoot)
    {
      foreach (KeyValuePair<CurrencyId, int> loot in backPackLoot)
      {
        ApplyLoot(loot.Key, loot.Value);
      }
    }

    public void Spend(CurrencyId currencyId, int amount)
    {
      if (Get(currencyId).Value < amount)
        throw new Exception("Not enough currency in BaseTrigger class! Check BaseTrigger.cs");

      SetCurrencyValue(Get(currencyId), Get(currencyId).Value - amount);
    }

    public void AddPickUpTreasureCurrency(CurrencyId currencyId, int amount)
    {
      AddCurrencyValue(Get(currencyId), amount);

      if (currencyId is /*CurrencyId.Chest or*/ CurrencyId.Key)
      {
        //    HasChestsOrKeys.Value = true;
        OnChestOrKeyAdded?.Invoke(currencyId);
      }
    }

    private void SetCurrencyValue(ReactiveProperty<int> currency, int value)
    {
      currency.Value = value;
    }

    private void AddCurrencyValue(ReactiveProperty<int> currency, int amount)
    {
      currency.Value += amount;
    }

    private void ApplyLoot(CurrencyId currencyId, int amount)
    {
      AddCurrencyValue(Get(currencyId), amount);

      LootDropedFromBackpack.Value++;
    }
  }
}