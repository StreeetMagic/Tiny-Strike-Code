using System;
using System.Collections.Generic;
using System.Linq;
using ConfigProviders;
using Meta.Upgrades;
using Utilities;

namespace Meta.Stats
{
  public class PlayerStatsProvider
  {
    private readonly UpgradeService _upgradeService;
    private readonly BalanceConfigProvider _balanceConfigProvider;

    private readonly Dictionary<StatId, ReactiveProperty<float>> _baseAdditives = new();
    private readonly Dictionary<StatId, ReactiveProperty<float>> _upgradeAdditives = new();
    private readonly Dictionary<StatId, ReactiveProperty<float>> _cheatAdditives = new();

    private readonly Dictionary<StatId, ReactiveProperty<float>> _statusMultipliers = new();

    public PlayerStatsProvider(UpgradeService upgradeService, BalanceConfigProvider balanceConfigProvider)
    {
      _upgradeService = upgradeService;
      _balanceConfigProvider = balanceConfigProvider;
    }

    public void Create()
    {
      CreateBaseAdditives();
      OnUpgradesChanged();

      _upgradeService.Changed += OnUpgradesChanged;
    }

    public void Stop()
    {
      _upgradeService.Changed -= OnUpgradesChanged;
      _baseAdditives.Clear();
    }

    public float GetStat(StatId id)
    {
      float additives = 0;

      if (_baseAdditives.TryGetValue(id, out ReactiveProperty<float> baseAdditive))
        additives += baseAdditive.Value;

      if (_upgradeAdditives.TryGetValue(id, out ReactiveProperty<float> upgradeAdditive))
        additives += upgradeAdditive.Value;

      if (_cheatAdditives.TryGetValue(id, out ReactiveProperty<float> cheatAdditive))
        additives += cheatAdditive.Value;

      float multipliers = 1;

      if (_statusMultipliers.TryGetValue(id, out ReactiveProperty<float> statusMultiplier))
        multipliers += statusMultiplier.Value;

      return additives * multipliers;
    }

    public void SetStatusMultiplier(StatId id, float value)
    {
      if (_statusMultipliers.TryGetValue(id, out ReactiveProperty<float> stat))
        stat.Value += value;
      else
        _statusMultipliers.Add(id, new ReactiveProperty<float>(value));
    }

    public void ClearStatusMultiplier(StatId id)
    {
      if (_statusMultipliers.TryGetValue(id, out ReactiveProperty<float> stat))
        stat.Value = 0;
      else
        _statusMultipliers.Add(id, new ReactiveProperty<float>(0));
    }

    public void AddCheatAdditiveStat(StatId id, float value)
    {
      if (_cheatAdditives.TryGetValue(id, out ReactiveProperty<float> stat))
        stat.Value += value;
      else
        _cheatAdditives.Add(id, new ReactiveProperty<float>(value));
    }

    public void ClearAllCheats()
    {
      _cheatAdditives.Clear();
    }

    private void OnUpgradesChanged()
    {
      _upgradeAdditives.Clear();

      foreach (StatId statId in Enum.GetValues(typeof(StatId)))
      {
        if (statId == StatId.Unknown)
          continue;

        var key = new ReactiveProperty<float>();
        _upgradeAdditives.Add(statId, key);

        if (_upgradeService.GetUpgrade(statId) != null)
          key.Value = _upgradeService.GetUpgrade(statId).Value;
      }
    }

    private void CreateBaseAdditives()
    {
      Dictionary<StatId, float> stats =
        _balanceConfigProvider
          .Player
          .BaseAdditiveStats
          .ToDictionary(x => x.StatId, x => x.Value);

      foreach (StatId statId in Enum.GetValues(typeof(StatId)))
      {
        if (statId == StatId.Unknown)
          continue;

        var key = new ReactiveProperty<float>();
        _baseAdditives.Add(statId, key);

        if (stats.TryGetValue(statId, out float value))
          key.Value = value;
      }
    }
  }
}