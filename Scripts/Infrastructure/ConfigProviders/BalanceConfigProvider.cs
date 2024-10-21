using System;
using System.Collections.Generic;
using System.Linq;
using AssetProviders;
using Core.Characters.Companions.Configs;
using Core.Characters.Enemies;
using Core.Characters.Players;
using Core.Grenades;
using Core.Weapons;
using Loggers;
using Meta;
using Meta.Configs;
using Meta.Currencies;
using Meta.Expirience;
using Meta.Loots;
using Meta.Stats;
using Meta.Upgrades;
using PersistentProgresses;
using Projects;
using UnityEngine;

namespace ConfigProviders
{
  public class BalanceConfigProvider
  {
    private readonly ProjectData _projectData;
    private readonly IAssetProvider _assetProvider;

    public BalanceConfigProvider(ProjectData projectData, IAssetProvider assetProvider)
    {
      _projectData = projectData;
      _assetProvider = assetProvider;
    }
    
    public bool IsLoaded { get; private set; }

    public Dictionary<EnemyId, EnemyConfig> Enemies { get; private set; }
    public Dictionary<GrenadeTypeId, GrenadeConfig> Grenades { get; private set; }
    public Dictionary<CurrencyId, LootConfig> Loots { get; private set; }
    public Dictionary<CompositeQuestId, CompositeQuestConfig> CompositeQuests { get; private set; }
    public Dictionary<SimpleQuestId, SimpleQuestConfig> SimpleQuests { get; private set; }
    public Dictionary<StatId, UpgradeConfig> Upgrades { get; private set; }
    public Dictionary<WeaponId, WeaponConfig> Weapons { get; private set; }
    public Dictionary<CompanionId, CompanionConfig> Companions { get; private set; } 

    public ProjectConfig Project { get; private set; }
    public ExpirienceConfig Expirience { get; private set; }
    public PlayerConfig Player { get; private set; }

    public void LoadConfigs()
    {
      Enemies = LoadAll<EnemyId, EnemyConfig>(enemyConfig => enemyConfig.Id, true);
      Grenades = LoadAll<GrenadeTypeId, GrenadeConfig>(grenadeConfig => grenadeConfig.Id, true);
      Loots = LoadAll<CurrencyId, LootConfig>(lootConfig => lootConfig.Id, true);
      CompositeQuests = LoadAll<CompositeQuestId, CompositeQuestConfig>(questConfig => questConfig.Id, true);
      SimpleQuests = LoadAll<SimpleQuestId, SimpleQuestConfig>(simpleQuestConfig => simpleQuestConfig.Id, true);
      Upgrades = LoadAll<StatId, UpgradeConfig>(upgradeConfig => upgradeConfig.Id, false);
      Weapons = LoadAll<WeaponId, WeaponConfig>(weaponConfig => weaponConfig.WeaponTypeId, true);
      Companions = LoadAll<CompanionId, CompanionConfig>(companionConfig => companionConfig.Id, true);

      Project = Load<ProjectConfig>();
      Expirience = Load<ExpirienceConfig>();
      Player = Load<PlayerConfig>();

      Validate();
      
      IsLoaded = true;
    }

    private void Validate()
    {
      ValidatePlayerStats();
    }

    private void ValidatePlayerStats()
    {
      List<StatSetup> stats = Player.BaseAdditiveStats;

      List<StatId> statlist = new List<StatId>();
      
      foreach (StatSetup stat in stats)
      {
        if (statlist.Contains(stat.StatId))
          new DebugLogger().LogError($"Duplicate stat id: {stat.StatId}");
        else
          statlist.Add(stat.StatId);
        
        if (stat.StatId == StatId.Unknown)
          new DebugLogger().LogError($"Unknown stat id: {stat.StatId}");
      }
      
      var allStats = Enum.GetValues(typeof(StatId)).Cast<StatId>().ToList();
      allStats.Remove(StatId.Unknown);
      
      foreach (StatId stat in allStats)
      {
        if (!statlist.Contains(stat))
           new DebugLogger().LogError($"В конфиг игрока в Stats надо добавить : {stat}");
      }
    }

    private T Load<T>() where T : ScriptableObject
    {
      string name = typeof(T).Name;
      name = name.Replace("Config", "");
      string path = _projectData.ConfigId + "/" + name;

      T config = _assetProvider.GetScriptable<T>(path);
      return config;
    }

    private Dictionary<TEnum, TConfig> LoadAll<TEnum, TConfig>(Func<TConfig, TEnum> predicate, bool check) where TEnum : Enum where TConfig : ScriptableObject
    {
      string name = typeof(TConfig).Name;

      name = name.Replace("Config", "");

      string path = _projectData.ConfigId + "/" + name;

      Dictionary<TEnum, TConfig> configMap = _assetProvider.GetScriptables<TConfig>(path).ToDictionary(predicate, x => x);

      IEnumerable<TEnum> lostConfigs = GetEnums(configMap.Keys.ToList());

      IEnumerable<TEnum> enumerable = lostConfigs as TEnum[] ?? lostConfigs.ToArray();

      if (enumerable.Any() && check)
        new DebugLogger().LogWarning(path + " Не хватает конфигов: " + string.Join(", ", enumerable));

      return configMap;
    }

    private IEnumerable<T> GetEnums<T>(List<T> keys) where T : Enum
    {
      T[] allEnums = Enum.GetValues(typeof(T)).Cast<T>().Where(x => Convert.ToInt32(x) != 0).ToArray();

      T[] existEnums =
        allEnums
          .Where(keys.Contains)
          .ToArray();

      return allEnums.Except(existEnums).ToArray();
    }
  }
}