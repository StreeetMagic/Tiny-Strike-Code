using System;
using System.Collections.Generic;
using Core.Weapons;
using HeadsUpDisplays;
using Meta;
using Meta.Currencies;
using Meta.Upgrades;
using Tutorials;

namespace PersistentProgresses
{
  [Serializable]
  public class ProjectProgress
  {
    public List<CurrencyProgress> Currencies;
    public int LootDropedFromBackpack;

    public int Expierience;
    public float MusicLoudness;
    public float SoundEffectsLoudness;
    public WeaponId CurrentPlayerWeaponId;
    public float SpawnPositionX;
    public float SpawnPositionY;
    public float SpawnPositionZ;
    public bool FirstInterstitialShown;

    public List<UpgradeProgress> Upgrades;
    public List<CompositeQuestProgress> Quests;
    public List<SimpleQuestProgress> SimpleQuests;
    public List<WeaponId> PlayerWeapons;

    public HeadsUpDisplayProgress HeadsUpDisplayProgress;
    public TutorialProgress TutorialProgress; 

    public ProjectProgress
    (
      List<CurrencyProgress> currencies,
      int lootDropedFromBackpack,
      int expierience,
      float musicLoudness,
      float soundEffectsLoudness,
      WeaponId currentPlayerWeaponId,
      float spawnPositionX,
      float spawnPositionY,
      float spawnPositionZ,
      bool firstInterstitialShown,
      List<UpgradeProgress> upgrades,
      List<CompositeQuestProgress> quests,
      List<SimpleQuestProgress> simpleQuests,
      List<WeaponId> playerWeapons,
      HeadsUpDisplayProgress headsUpDisplayProgress,
      TutorialProgress tutorialProgress
    )
    {
      Currencies = currencies;
      LootDropedFromBackpack = lootDropedFromBackpack;
      Expierience = expierience;
      MusicLoudness = musicLoudness;
      SoundEffectsLoudness = soundEffectsLoudness;
      CurrentPlayerWeaponId = currentPlayerWeaponId;
      SpawnPositionX = spawnPositionX;
      SpawnPositionY = spawnPositionY;
      SpawnPositionZ = spawnPositionZ;
      FirstInterstitialShown = firstInterstitialShown;
      Upgrades = upgrades;
      Quests = quests;
      SimpleQuests = simpleQuests;
      PlayerWeapons = playerWeapons;
      HeadsUpDisplayProgress = headsUpDisplayProgress;
      TutorialProgress = tutorialProgress;
    }
  }
}