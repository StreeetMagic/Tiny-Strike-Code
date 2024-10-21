using System.Collections.Generic;
using System.Linq;
using ConfigProviders;
using Core.Characters.Players;
using Core.Weapons;
using HeadsUpDisplays;
using Meta;
using Meta.Currencies;
using Meta.Sub;
using Meta.Upgrades;
using Tutorials;
using UnityEngine;

namespace PersistentProgresses
{
  public class PersistentProgressService
  {
    private readonly BalanceConfigProvider _balanceConfigProvider;

    public PersistentProgressService(BalanceConfigProvider balanceConfigProvider)
    {
      _balanceConfigProvider = balanceConfigProvider;
    }

    public ProjectProgress ProjectProgress { get; private set; }

    public void LoadProgress(string getString) =>
      ProjectProgress = JsonUtility.FromJson<ProjectProgress>(getString);

    public void SetDefault()
    {
      ProjectConfig projectConfig = _balanceConfigProvider.Project;
      PlayerConfig playerConfig = _balanceConfigProvider.Player;

      WeaponId playerConfigStartWeapon = playerConfig.StartWeapons.Count != 0
        ? playerConfig.StartWeapons[0]
        : WeaponId.Unarmed;

      ProjectProgress = new ProjectProgress
      (
        Currencies(projectConfig),
        lootDropedFromBackpack: 0,
        projectConfig.Expierience,
        musicLoudness: 0,
        soundEffectsLoudness: 0,
        playerConfigStartWeapon,
        0, 
        0, 
        0,
        false,
        Upgrades(),
        Quests(),
        SimpleQuests(),
        playerConfig.StartWeapons,
        new HeadsUpDisplayProgress(false),
        new TutorialProgress(TutorialState.Start)
      );
    }

    private List<CurrencyProgress> Currencies(ProjectConfig projectConfig) =>
      projectConfig
        .Currencies
        .Select(currency => new CurrencyProgress(currency.Id, currency.Count))
        .ToList();

    private List<UpgradeProgress> Upgrades() =>
      _balanceConfigProvider
        .Upgrades
        .Select(upgrade => new UpgradeProgress(upgrade.Key, null))
        .ToList();

    private List<CompositeQuestProgress> Quests()
    {
      var questProgresses = new List<CompositeQuestProgress>();

      Dictionary<CompositeQuestId, CompositeQuestConfig> questConfigs = _balanceConfigProvider.CompositeQuests;

      foreach (KeyValuePair<CompositeQuestId, CompositeQuestConfig> questConfig in questConfigs)
      {
        List<SubQuestProgress> subQuests = new List<SubQuestProgress>();

        for (var i = 0; i < questConfig.Value.SubQuests.Count; i++)
        {
          SubQuestProgress subQuestProgress = new(questConfig.Value.Id, 0, QuestState.UnActivated);
          subQuests.Add(subQuestProgress);
        }

        questProgresses.Add(new CompositeQuestProgress(questConfig.Key, QuestState.UnActivated, subQuests, false));
      }

      return questProgresses;
    }

    private List<SimpleQuestProgress> SimpleQuests() =>
      _balanceConfigProvider
        .SimpleQuests
        .Select(keyValuePair => new SimpleQuestProgress(keyValuePair.Key, QuestState.UnActivated, 0))
        .ToList();
  }
}