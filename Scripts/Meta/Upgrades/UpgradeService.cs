using System;
using System.Collections.Generic;
using System.Linq;
using ConfigProviders;
using Meta.Currencies;
using Meta.Stats;
using PersistentProgresses;
using SaveLoadServices;

namespace Meta.Upgrades
{
  public class UpgradeService : IProgressWriter
  {
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly CurrencyStorage _currencyStorage;

    private Dictionary<StatId, UpgradeSetup> _upgrades;

    public UpgradeService(BalanceConfigProvider balanceConfigProvider, CurrencyStorage currencyStorage)
    {
      _balanceConfigProvider = balanceConfigProvider;
      _currencyStorage = currencyStorage;
    }

    public event Action Changed;

    public void BuyUpgrade(StatId statId)
    {
      _currencyStorage.Spend(_balanceConfigProvider.Upgrades[statId].CurrencyId, GetNextUpgradeCost(statId));

      if (_upgrades[statId] == null)
      {
        _upgrades[statId] = _balanceConfigProvider.Upgrades[statId].Setups[0];
      }
      else
      {
        int indexOf = _balanceConfigProvider.Upgrades[statId].Setups.IndexOf(_upgrades[statId]);
        _upgrades[statId] = _balanceConfigProvider.Upgrades[statId].Setups[indexOf + 1];
      }

      Changed?.Invoke();
    }    
    
    public void GainUpgradeByAd(StatId statId)
    {
      if (_upgrades[statId] == null)
      {
        _upgrades[statId] = _balanceConfigProvider.Upgrades[statId].Setups[0];
      }
      else
      {
        int indexOf = _balanceConfigProvider.Upgrades[statId].Setups.IndexOf(_upgrades[statId]);
        _upgrades[statId] = _balanceConfigProvider.Upgrades[statId].Setups[indexOf + 1];
      }

      Changed?.Invoke();
    }

    public UpgradeSetup GetUpgrade(StatId statId)
    {
      return _upgrades.GetValueOrDefault(statId);
    }

    public void ReadProgress(ProjectProgress projectProgress)
    {
      Dictionary<StatId, UpgradeConfig> upgrades = _balanceConfigProvider.Upgrades;
      _upgrades = new Dictionary<StatId, UpgradeSetup>();

      foreach (StatId upgradeId in upgrades.Keys)
      {
        UpgradeConfig config = upgrades[upgradeId];
        UpgradeProgress upgradeProgress = projectProgress.Upgrades.FirstOrDefault(x => x.Id == upgradeId);

        if (upgradeProgress == null)
          throw new Exception("Upgrade progress not found");

        _upgrades.Add(upgradeId, upgradeProgress.Setup == null
          ? null
          : config.Setups.FirstOrDefault(x => x.Cost == upgradeProgress.Setup.Cost));
      }
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      var upgrades = new List<UpgradeProgress>();

      for (int i = 0; i < _balanceConfigProvider.Upgrades.Count; i++)
      {
        StatId upgradeId = _balanceConfigProvider.Upgrades.Keys.ElementAt(i);
        UpgradeSetup setup = _upgrades[upgradeId];
        upgrades.Add(new UpgradeProgress(upgradeId, setup));
      }

      projectProgress.Upgrades = upgrades;
    }

    public bool IsMax(StatId configId)
    {
      UpgradeSetup current = _upgrades[configId];
      int indexOf = _balanceConfigProvider.Upgrades[configId].Setups.IndexOf(current);
      return indexOf == _balanceConfigProvider.Upgrades[configId].Setups.Count - 1;
    }

    public int NextLevelCost(StatId configId)
    {
      UpgradeSetup current = _upgrades[configId];
      int indexOf = _balanceConfigProvider.Upgrades[configId].Setups.IndexOf(current);
      return _balanceConfigProvider.Upgrades[configId].Setups[indexOf + 1].Cost;
    }

    public int CurrentLevel(StatId configId)
    {
      UpgradeSetup current = _upgrades[configId];

      if (current == null)
        return 0;

      int indexOf = _balanceConfigProvider.Upgrades[configId].Setups.IndexOf(current);
      return indexOf + 1;
    }

    public int MaxLevel(StatId configId)
    {
      return _balanceConfigProvider.Upgrades[configId].Setups.Count;
    }

    public int GetNextUpgradeCost(StatId id)
    {
      return GetNextUpgrade(id, _upgrades[id]).Cost;
    }

    public float NextValue(StatId id)
    {
      return GetNextUpgrade(id, _upgrades[id]).Value;
    }

    public bool HasAvailableUpgrades()
    {
      foreach (KeyValuePair<StatId, UpgradeSetup> upgrade in _upgrades)
      {
        UpgradeConfig config = _balanceConfigProvider.Upgrades[upgrade.Key];

        if (IsMax(upgrade.Key))
          continue;

        if (_currencyStorage.Get(config.CurrencyId).Value >= NextLevelCost(upgrade.Key))
          return true;
      }

      return false;
    }

    public bool CanBuyNextUpgrade(StatId id)
    {
      return _currencyStorage.Get(_balanceConfigProvider.Upgrades[id].CurrencyId).Value >= NextLevelCost(id);
    }

    private UpgradeSetup GetNextUpgrade(StatId statId, UpgradeSetup upgrade)
    {
      int indexOf = _balanceConfigProvider.Upgrades[statId].Setups.IndexOf(upgrade);
      return _balanceConfigProvider.Upgrades[statId].Setups[indexOf + 1];
    }
  }
}