using System;
using System.Collections.Generic;
using System.Linq;
using Windows;
using Windows.Configs;
using AssetProviders;
using AudioServices.AudioMixers;
using AudioServices.Sounds;
using AudioServices.Sounds.Configs;
using Core.AimObstacles;
using Core.Characters.Companions.Configs;
using Core.Characters.Enemies;
using Core.PickUpTreasures;
using Core.PickUpTreasures.Configs;
using Core.Weapons;
using Core.Weapons.Configs;
using Loggers;
using Meta;
using Meta.ChestRewards;
using Meta.Configs;
using Meta.Currencies;
using Meta.LevelUp;
using Meta.Stats;
using Meta.Upgrades.Configs;
using Popups;
using Popups.Configs;
using VisualEffects;
using VisualEffects.Configs;
using VisualEffects.ParticleImages;

namespace ConfigProviders
{
  public class ArtConfigProvider
  {
    private readonly IAssetProvider _assetProvider;

    public ArtConfigProvider(IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }

    public Dictionary<AimObstacleId, AimObstacleArtSetup> AimObstacles { get; private set; }
    public Dictionary<AudioMixerGroupId, AudioMixerGroupArtSetup> AudioMixerGroups { get; private set; }
    public Dictionary<ChestRewardId, ChestRewardArtSetup> ChestRewards { get; private set; }
    public Dictionary<CompanionId, CompanionArtSetup> Companions { get; private set; }
    public Dictionary<CompositeQuestId, CompositeQuestArtSetup> CompositeQuests { get; private set; }
    public Dictionary<CurrencyId, CurrencyArtSetup> Currencies { get; private set; }
    public Dictionary<EnemyId, EnemyArtSetup> Enemies { get; private set; }
    public Dictionary<EnemyVisualId, EnemyVisualArtSetup> EnemyVisuals { get; private set; }
    public Dictionary<LevelUpRewardId, LevelUpRewardArtSetup> LevelUpRewards { get; private set; }
    public Dictionary<ParticleImageId, ParticleImageArtSetup> ParticleImages { get; private set; }
    public Dictionary<PickUpTreasureId, PickUpTreasureArtSetup> PickUpTreasures { get; private set; }
    public Dictionary<PickUpWindowId, PickUpWindowArtSetup> PickUpWindows { get; private set; }
    public Dictionary<PopupId, PopupArtSetup> Popups { get; private set; }
    public Dictionary<SimpleQuestId, SimpleQuestArtSetup> SimpleQuests { get; private set; }
    public Dictionary<SoundId, SoundArtSetup> Sounds { get; private set; }
    public Dictionary<StatId, UpgradeArtSetup> Upgrades { get; private set; }
    public Dictionary<VisualEffectId, VisualEffectArtSetup> VisualEffects { get; private set; }
    public Dictionary<WeaponId, WeaponArtSetup> Weapons { get; private set; }
    public Dictionary<WindowId, WindowArtSetup> Windows { get; private set; }

    public void LoadConfigs()
    {
      AimObstacles = Load<AimObstacleId, AimObstacleArtSetup, AimObstacleArtConfig>();
      AudioMixerGroups = Load<AudioMixerGroupId, AudioMixerGroupArtSetup, AudioMixerGroupArtConfig>();
      ChestRewards = Load<ChestRewardId, ChestRewardArtSetup, ChestRewardArtConfig>();
      Companions = Load<CompanionId, CompanionArtSetup, CompanionArtConfig>();
      CompositeQuests = Load<CompositeQuestId, CompositeQuestArtSetup, CompositeQuestArtConfig>();
      Currencies = Load<CurrencyId, CurrencyArtSetup, CurrencyArtConfig>();
      Enemies = Load<EnemyId, EnemyArtSetup, EnemyArtConfig>();
      EnemyVisuals = Load<EnemyVisualId, EnemyVisualArtSetup, EnemyVisualArtConfig>();
      LevelUpRewards = Load<LevelUpRewardId, LevelUpRewardArtSetup, LevelUpRewardArtConfig>();
      ParticleImages = Load<ParticleImageId, ParticleImageArtSetup, ParticleImageArtConfig>();
      PickUpTreasures = Load<PickUpTreasureId, PickUpTreasureArtSetup, PickUpTreasureArtConfig>();
      PickUpWindows = Load<PickUpWindowId, PickUpWindowArtSetup, PickUpWindowArtConfig>();
      Popups = Load<PopupId, PopupArtSetup, PopupArtConfig>();
      SimpleQuests = Load<SimpleQuestId, SimpleQuestArtSetup, SimpleQuestArtConfig>();
      Sounds = Load<SoundId, SoundArtSetup, SoundArtConfig>();
      Upgrades = Load<StatId, UpgradeArtSetup, UpgradeArtConfig>();
      VisualEffects = Load<VisualEffectId, VisualEffectArtSetup, VisualEffectArtConfig>();
      Weapons = Load<WeaponId, WeaponArtSetup, WeaponArtConfig>();
      Windows = Load<WindowId, WindowArtSetup, WindowArtConfig>();
    }

    private Dictionary<T, T1> Load<T, T1, T2>()
      where T2 : ArtConfig<T1>
      where T1 : ArtSetup<T>
      where T : Enum
    {
      Dictionary<T, T1> artSetups = _assetProvider.GetArtScriptable<T2>().Setups.ToDictionary(setup => setup.Id, setup => setup);

      ValidateUnknownKeys<T, T1, T2>(artSetups);

      IEnumerable<T> lostConfigs = GetEnums(artSetups.Keys.ToList());

      IEnumerable<T> enumerable = lostConfigs as T[] ?? lostConfigs.ToArray();

      if (enumerable.Any())
        new DebugLogger().LogWarning(typeof(T).Name + " Не хватает арт конфигов: " + string.Join(", ", enumerable));

      return artSetups;
    }

    private void ValidateUnknownKeys<T, T1, T2>(Dictionary<T, T1> artSetups)
      where T2 : ArtConfig<T1>
      where T1 : ArtSetup<T>
      where T : Enum
    {
      if (artSetups.Keys.All(id => Convert.ToInt32(id) != 0))
        return;

      new DebugLogger().LogError("В " + typeof(T2).Name + " присутствуют Unknown Keys: ");
    }

    private IEnumerable<T> GetEnums<T>(List<T> keys) where T : Enum
    {
      T[] allEnums = Enum.GetValues(typeof(T)).Cast<T>().Where(x => Convert.ToInt32(x) != 0).ToArray();

      T[] existEnums = allEnums
        .Where(keys.Contains)
        .ToArray();

      return allEnums.Except(existEnums).ToArray();
    }
  }
}