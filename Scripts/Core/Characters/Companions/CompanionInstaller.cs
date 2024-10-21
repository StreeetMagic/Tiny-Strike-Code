using Core.Characters.Companions.Configs;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Movers;
using UnityEngine;
using UnityEngine.AI;
using Zenject.Source.Install;
using ZenjectFactories.GameobjectContext;

namespace Core.Characters.Companions
{
  public class CompanionInstaller : MonoInstaller
  {
    [field: SerializeField] public Transform ShootingPoint { get; private set; }
    
    public TransformContainer TransformContainer { get; set; }
    public CompanionConfig Config { get; set; }

    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<FiniteStateMachine>().AsSingle();
      Container.BindInterfacesAndSelfTo<WeaponRaiseTimer>().AsSingle();
      Container.BindInterfacesAndSelfTo<CompanionShooter>().AsSingle();
      Container.BindInterfacesAndSelfTo<CompanionToEnemyRotator>().AsSingle();
      Container.BindInterfacesAndSelfTo<CompanionWeaponMagazine>().AsSingle();

      Container.Bind<IStateMachineFactory>().To<CompanionStateMachineFactory>().AsSingle();
      Container.Bind<IMover>().To<NavMeshMover>().AsSingle();
      Container.Bind<IGameObjectZenjectFactory>().To<EnemyZenjectFactory>().AsSingle();

      Container.Bind<Companion>().FromInstance(GetComponent<Companion>()).AsSingle();
      Container.Bind<NavMeshAgent>().FromInstance(GetComponent<NavMeshAgent>()).AsSingle();
      Container.Bind<Transform>().FromInstance(transform).AsSingle();
    }
  }
} 