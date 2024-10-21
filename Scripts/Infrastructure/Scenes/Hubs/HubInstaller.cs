using System;
using System.Collections.Generic;
using Windows;
using ConfigProviders;
using Core;
using Core.Cameras;
using Core.Characters.Enemies;
using Core.Characters.Hens;
using Core.Characters.Players;
using Core.CorpseRemovers;
using Core.PickUpTreasures;
using Core.Projectiles.Scripts;
using Core.Spawners.Companions;
using Core.Spawners.Enemies;
using HeadsUpDisplays;
using LevelDesign.Maps;
using Meta;
using Meta.BackpackStorages;
using Meta.Chests;
using Popups;
using Prefabs;
using Scripts.Debugs;
using Tutorials;
using UnityEngine;
using VisualEffects;
using VisualEffects.ParticleImages;
using Zenject;
using Zenject.Source.Install;
using ZenjectFactories.SceneContext;
using LootSlotFactory = Core.LootSlots.LootSlotFactory;

namespace Scenes.Hubs
{
  public class HubInstaller : MonoInstaller
  {
    [Inject] private ArtConfigProvider _artConfigProvider;
    [Inject] private DevConfigProvider _devConfigProvider;
    [Inject] private BalanceConfigProvider _balanceConfigProvider;

    public override void InstallBindings()
    {
      Container.Bind<HubInitializer>().FromInstance(GetComponent<HubInitializer>()).AsSingle().NonLazy();
      Container.Bind<HubInstaller>().FromInstance(this).AsSingle();

      Container.BindInterfacesAndSelfTo<HubZenjectFactory>().AsSingle();

      Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<PlayerProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<PlayerRespawnPosition>().AsSingle();

      Container.BindInterfacesAndSelfTo<CameraFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();

      Container.BindInterfacesAndSelfTo<EnemySpawnerFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemySpawnerProvider>().AsSingle();

      Container.BindInterfacesAndSelfTo<HenFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<HenSpawner>().AsSingle();

      Container.BindInterfacesAndSelfTo<MapFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<CorpseRemover>().AsSingle();
      Container.BindInterfacesAndSelfTo<ProjectileStorage>().AsSingle();

      Container.BindInterfacesAndSelfTo<PickUpTreasureSpawner>().AsSingle();
      Container.BindInterfacesAndSelfTo<PickUpTreasureCollector>().AsSingle();

      Container.BindInterfacesAndSelfTo<HeadsUpDisplayFactory>().AsSingle();

      Container.BindInterfacesAndSelfTo<UpgradeCellFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<LootSlotFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<ProjectileFactory>().AsSingle();

      Container.BindInterfacesAndSelfTo<VisualEffectFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<ParticleImageFactory>().AsSingle();

      Container.BindInterfacesAndSelfTo<MapProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<WindowService>().AsSingle();
      Container.BindInterfacesAndSelfTo<PopupService>().AsSingle().NonLazy();

      Container.BindInterfacesAndSelfTo<CompositeQuestTargetsProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<QuestCompleter>().AsSingle();

      Container.BindInterfacesAndSelfTo<SimpleQuestTargetsProvider>().AsSingle();

      Container.BindInterfacesAndSelfTo<TutorialFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<TutorialProvider>().AsSingle();

      Container.BindInterfacesAndSelfTo<BackpackStorage>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<CompanionSpawner>().AsSingle();
      
      Container.BindInterfacesAndSelfTo<ChestManager>().AsSingle();

      Container.BindInterfacesAndSelfTo<KeyboardDebugger>().AsSingle().NonLazy();

      BindMap();
      BindFactories();
    }

    private void BindFactories()
    {
      Container
        .BindFactory<EnemyConfig, List<Transform>, EnemySpawner, bool, Enemy, Enemy.Factory>()
        .FromSubContainerResolve()
        .ByNewContextPrefab<EnemyInstaller>(_devConfigProvider.GetPrefabForComponent<EnemyInstaller>(PrefabId.Enemy))
        .AsSingle();
    }

    private void BindMap()
    {
      Map map;

      Map[] maps = FindObjectsOfType<Map>();

      switch (maps.Length)
      {
        case 0:
          throw new Exception("На сцене нет объекта со скриптом Map");

        case 1:
          map = maps[0];
          break;

        default:
          throw new Exception("На сцене несколько объектов со скриптом Map");
      }

      Container.Resolve<MapProvider>().Map = map;
    }
  }
}