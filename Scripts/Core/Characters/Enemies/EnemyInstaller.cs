using System.Collections.Generic;
using Core.Characters.Enemies.Phases;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Movers;
using Core.Spawners.Enemies;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject.Source.Install;
using ZenjectFactories.GameobjectContext;

namespace Core.Characters.Enemies
{
  public class EnemyInstaller : MonoInstaller
  {
    [Inject] private EnemyConfig _enemyConfig;
    [Inject] private List<Transform> _spawnPoints;
    [Inject] private EnemySpawner _spawner;
    [Inject] private bool _randomPatroling;
  
    public EnemyConfig Config => _enemyConfig;
    public bool RandomPatroling => _randomPatroling;
    public IHealth Health { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public EnemyMeshModel MeshModel { get; set; }
    public FiniteStateMachine FiniteStateMachine { get; private set; }

    public override void InstallBindings()
    {
      GetComponent<Enemy>().Installer = this;

      Container.Bind<EnemyInstaller>().FromInstance(this).AsSingle();
      Container.Bind<Enemy>().FromInstance(GetComponent<Enemy>()).AsSingle();
      Container.Bind<Transform>().FromInstance(transform).AsSingle();
      Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsSingle();
      Container.Bind<List<Transform>>().FromInstance(_spawnPoints).AsSingle();
      Container.Bind<EnemySpawner>().FromInstance(_spawner).AsSingle();
      Container.Bind<EnemyId>().FromInstance(_enemyConfig.Id).AsSingle();
      Container.Bind<NavMeshAgent>().FromInstance(GetComponent<NavMeshAgent>()).AsSingle();

      Container.BindInterfacesAndSelfTo<EnemyShooter>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyReturnToSpawnStatus>().AsSingle();
      Container.BindInterfacesAndSelfTo<HitStatus>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyMeleeAttacker>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyAnimatorProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyToPlayerRotator>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyShootingPointProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyMeshModelSpawner>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<EnemyToSpawnerDistance>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyGrenadeThrower>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyColliderDisabler>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyHealer>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyRoutePointsManager>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyAssistCall>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyMeshMaterialChanger>().AsSingle();
      Container.BindInterfacesAndSelfTo<FiniteStateMachine>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyIdleTimer>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyAlertTimer>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyGrenadeStorage>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyGrenadeThrowTimer>().AsSingle();
      Container.BindInterfacesAndSelfTo<WeaponRaiseTimer>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyWeaponLowerer>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyWeaponMagazine>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyWeaponMagazineReloaderTimer>().AsSingle();
      Container.BindInterfacesAndSelfTo<ExpirienceCollector>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyGrenadeThrowerStatus>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyAlertPointProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyToPlayerAggro>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyPhase>().AsSingle().NonLazy();

      Container.Bind<IMover>().To<NavMeshMover>().AsSingle();
      Container.Bind<IGameObjectZenjectFactory>().To<EnemyZenjectFactory>().AsSingle();
      Container.Bind<IStateMachineFactory>().To<EnemyStateMachineFactory>().AsSingle();
      Container.Bind<IHealth>().To<EnemyHealth>().FromInstance(GetComponent<EnemyHealth>()).AsSingle();

      Health = Container.Resolve<IHealth>();
      NavMeshAgent = Container.Resolve<NavMeshAgent>();
      FiniteStateMachine = Container.Resolve<FiniteStateMachine>();
    }
  }
}