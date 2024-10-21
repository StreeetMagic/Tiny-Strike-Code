using System;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Movers;
using SaveLoadServices;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject.Source.Install;
using ZenjectFactories.GameobjectContext;

namespace Core.Characters.Players
{
  public class PlayerInstaller : MonoInstaller, IInitializable, IDisposable
  {
    [Inject] private ISaveLoadService _saveLoadServices;

    public Transform Transform { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public PlayerTargetHolder TargetHolder { get; private set; }
    public PlayerHealth Health { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerStandsOnSamePosition StandsOnSamePosition { get; private set; }
    public PlayerHenContainer HenContainer { get; private set; }
    public PlayerMoveSpeed MoveSpeed { get; private set; }
    public PlayerTargetTrigger TargetTrigger { get; private set; }
    public IMover Mover { get; private set; }
    public PlayerDamage Damage { get; private set; }
    public PlayerWeaponShootingPoint WeaponShootingPointPoint { get; private set; }
    public PlayerWeaponIdProvider WeaponIdProvider { get; private set; }
    public PlayerWeaponAmmo WeaponAmmo { get; private set; }
    public PlayerWeaponMagazineReloader PlayerWeaponMagazineReloader { get; private set; }
    public PlayerWeaponAttacker WeaponAttacker { get; private set; }
    public PlayerCompanionContainer CompanionContainer { get; private set; }
    public PlayerHostageHolder HostageHolder { get; private set; }

    public override void InstallBindings()
    {
      TargetTrigger = GetComponentInChildren<PlayerTargetTrigger>();

      Container.BindInterfacesAndSelfTo<PlayerInstaller>().FromInstance(this).AsSingle().NonLazy();

      Container.Bind<PlayerAnimatorController>().FromInstance(GetComponentInChildren<PlayerAnimatorController>()).AsSingle();
      Container.Bind<PlayerHenContainer>().FromInstance(GetComponentInChildren<PlayerHenContainer>()).AsSingle();
      Container.Bind<CharacterController>().FromInstance(GetComponent<CharacterController>()).AsSingle();
      Container.Bind<Transform>().FromInstance(transform).AsSingle();
      Container.Bind<PlayerWeaponContainer>().FromInstance(GetComponentInChildren<PlayerWeaponContainer>()).AsSingle();
      Container.Bind<PlayerTargetTrigger>().FromInstance(TargetTrigger).AsSingle();
      Container.Bind<NavMeshAgent>().FromInstance(GetComponent<NavMeshAgent>()).AsSingle();
      Container.Bind<PlayerHostageHolderPoint>().FromInstance(GetComponentInChildren<PlayerHostageHolderPoint>()).AsSingle();
      Container.Bind<PlayerCompanionContainer>().FromInstance(GetComponentInChildren<PlayerCompanionContainer>()).AsSingle();

      Container.BindInterfacesAndSelfTo<PlayerRotator>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerStandsOnSamePosition>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerHenSpawner>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerBombLocator>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerMoveSpeed>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerHealth>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerHealthRegenerator>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerTargetHolder>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerTargetLocator>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<FiniteStateMachine>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerReloadAnimationController>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerDamage>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerHostageLocator>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerHostageHolder>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponIdProvider>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<WeaponRaiseTimer>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponLowTimer>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponAttacker>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponMeshSwitcher>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponShootingPoint>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponAmmo>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponMagazineReloader>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponMuzzleFlashEffector>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponAttackAnimationController>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<PlayerWeaponShooter>().AsSingle().NonLazy();

      Container.Bind<IMover>().To<NavMeshMover>().AsSingle().NonLazy();
      Container.Bind<IGameObjectZenjectFactory>().To<PlayerZenjectFactory>().AsSingle().NonLazy();
      Container.Bind<IStateMachineFactory>().To<PlayerStateMachineFactory>().AsSingle().NonLazy();

      Transform = Container.Resolve<Transform>();
      WeaponShootingPointPoint = Container.Resolve<PlayerWeaponShootingPoint>();
      TargetHolder = Container.Resolve<PlayerTargetHolder>();
      Health = Container.Resolve<PlayerHealth>();
      InputHandler = Container.Resolve<PlayerInputHandler>();
      StandsOnSamePosition = Container.Resolve<PlayerStandsOnSamePosition>();
      HenContainer = Container.Resolve<PlayerHenContainer>();
      WeaponIdProvider = Container.Resolve<PlayerWeaponIdProvider>();
      MoveSpeed = Container.Resolve<PlayerMoveSpeed>();
      WeaponAmmo = Container.Resolve<PlayerWeaponAmmo>();
      NavMeshAgent = Container.Resolve<NavMeshAgent>();
      Mover = Container.Resolve<IMover>();
      Damage = Container.Resolve<PlayerDamage>();
      PlayerWeaponMagazineReloader = Container.Resolve<PlayerWeaponMagazineReloader>();
      WeaponAttacker = Container.Resolve<PlayerWeaponAttacker>();
      CompanionContainer = Container.Resolve<PlayerCompanionContainer>();
      HostageHolder = Container.Resolve<PlayerHostageHolder>();
    }

    public void Initialize()
    {
      _saveLoadServices.ProgressReaders.Add(WeaponIdProvider);
    }

    public void Dispose()
    {
      _saveLoadServices.ProgressReaders.Remove(WeaponIdProvider);
    }
  }
}