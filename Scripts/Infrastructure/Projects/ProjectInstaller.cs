using AssetProviders;
using AudioServices;
using ConfigProviders;
using Core.Weapons;
using CoroutineRunners;
using DevConfigs;
using HeadsUpDisplays;
using Inputs;
using LoadingCurtains;
using Loggers;
using Meta;
using Meta.Currencies;
using Meta.Expirience;
using Meta.Stats;
using Meta.Upgrades;
using PersistentProgresses;
using RandomServices;
using SaveLoadServices;
using SceneLoaders;
using TimeServices;
using UnityEngine.Audio;
using VisualEffects;
using VisualEffects.ParticleImages;
using Zenject.Source.Install;
using ZenjectFactories.ProjectContext;

namespace Projects
{
  public class ProjectInstaller : MonoInstaller
  {
    public AudioMixer AudioMixer;

    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<ProjectInitializer>().FromInstance(GetComponent<ProjectInitializer>()).AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<ProjectZenjectFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromComponentInNewPrefabResource(DevConfig.LoadingCurtain).AsSingle();

      Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromComponentInNewPrefabResource(DevConfig.CoroutineRunner).AsSingle();
      Container.Bind<ISaveLoadService>().To<PlayerPrefsSaveLoad>().AsSingle();
      Container.Bind<IAssetProvider>().To<ResourceFolderAssetProvider>().AsSingle();

      Container.BindInterfacesAndSelfTo<RandomService>().AsSingle();

      Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
      Container.BindInterfacesAndSelfTo<PersistentProgressService>().AsSingle();
      Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
      Container.BindInterfacesAndSelfTo<AudioService>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<DebugLogger>().AsSingle();

      Container.BindInterfacesAndSelfTo<AdvertismentService>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<InternetConnectionChecker>().AsSingle().NonLazy();

      Container.BindInterfacesAndSelfTo<BalanceConfigProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<DevConfigProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<ArtConfigProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<VisualEffectProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<ParticleImageProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyVisualsProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<HeadsUpDisplayProvider>().AsSingle();

      Container.BindInterfacesAndSelfTo<UpgradeService>().AsSingle();

      Container.BindInterfacesAndSelfTo<ProjectData>().AsSingle();
      Container.BindInterfacesAndSelfTo<CurrencyStorage>().AsSingle();
      Container.BindInterfacesAndSelfTo<ExpierienceStorage>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<CompositeQuestStorage>().AsSingle();
      Container.BindInterfacesAndSelfTo<SimpleQuestStorage>().AsSingle();

      Container.BindInterfacesAndSelfTo<WeaponShop>().AsSingle();
      Container.BindInterfacesAndSelfTo<WeaponStorage>().AsSingle();

      Container.BindInterfacesAndSelfTo<PlayerStatsProvider>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();

      Container.Bind<AudioMixer>().FromInstance(AudioMixer).AsSingle();
    }
  }
}