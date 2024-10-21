using System;
using System.Collections.Generic;
using Core.Characters.Enemies.AnyStatesTransitions;
using Core.Characters.Enemies.States.Alert;
using Core.Characters.Enemies.States.Bootstrap;
using Core.Characters.Enemies.States.Chase;
using Core.Characters.Enemies.States.Die;
using Core.Characters.Enemies.States.Idle;
using Core.Characters.Enemies.States.LowWeapon;
using Core.Characters.Enemies.States.MeleeAttack;
using Core.Characters.Enemies.States.Patrol;
using Core.Characters.Enemies.States.RaiseWeapon;
using Core.Characters.Enemies.States.Reload;
using Core.Characters.Enemies.States.Return;
using Core.Characters.Enemies.States.Shoot;
using Core.Characters.Enemies.States.ThrowGrenade;
using Core.Characters.FiniteStateMachines;
using ZenjectFactories.GameobjectContext;

namespace Core.Characters.Enemies
{
  public class EnemyStateMachineFactory : IStateMachineFactory
  {
    private readonly IGameObjectZenjectFactory _factory;

    public EnemyStateMachineFactory(IGameObjectZenjectFactory factory)
    {
      _factory = factory;
    }

    public Dictionary<Type, State> GetStates()
    {
      return new Dictionary<Type, State>
      {
        {
          typeof(EnemyBootstrapState),
          _factory.Instantiate<EnemyBootstrapState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyBootstrapToIdleTransition>()
            }
          )
        },

        {
          typeof(EnemyIdleState),
          _factory.Instantiate<EnemyIdleState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyIdleToPatrolTransition>(),
              _factory.Instantiate<EnemyIdleToAlertTransition>()
            }
          )
        },

        {
          typeof(EnemyAlertState),
          _factory.Instantiate<EnemyAlertState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyAlertToChaseTransition>()
            }
          )
        },

        {
          typeof(EnemyChaseState),
          _factory.Instantiate<EnemyChaseState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyChaseToReturnTransition>(),
              _factory.Instantiate<EnemyChaseToThrowGrenadeTransition>(),
              _factory.Instantiate<EnemyChaseToReloadTransition>(),
              _factory.Instantiate<EnemyChaseToMeleeAttackTransition>(),
              _factory.Instantiate<EnemyChaseToRaiseWeaponTransition>(),
            }
          )
        },

        {
          typeof(EnemyPatrolState),
          _factory.Instantiate<EnemyPatrolState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyPatrolToIdleTransition>(),
              _factory.Instantiate<EnemyPatrolToAlertTransition>(),
            }
          )
        },

        {
          typeof(EnemyMeleeAttackState),
          _factory.Instantiate<EnemyMeleeAttackState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyMeleeAttackToChaseTransition>(),
            }
          )
        },

        {
          typeof(EnemyThrowGrenadeState),
          _factory.Instantiate<EnemyThrowGrenadeState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyThrowGrenadeToChaseTransition>(),
            }
          )
        },

        {
          typeof(EnemyReloadState),
          _factory.Instantiate<EnemyReloadState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyReloadToChaseTransition>(),
            }
          )
        },

        {
          typeof(EnemyRaiseWeaponState),
          _factory.Instantiate<EnemyRaiseWeaponState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyRaiseWeaponToShootTransition>(),
            }
          )
        },

        {
          typeof(EnemyShootState),
          _factory.Instantiate<EnemyShootState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyShootToLowWeaponTransition>(),
            }
          )
        },

        {
          typeof(EnemyLowWeaponState),
          _factory.Instantiate<EnemyLowWeaponState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyLowWeaponToChaseTransition>(),
            }
          )
        },

        {
          typeof(EnemyReturnState),
          _factory.Instantiate<EnemyReturnState>
          (
            new List<Transition>
            {
              _factory.Instantiate<EnemyReturnToIdleTransition>(),
            }
          )
        },

        {
          typeof(EnemyDieState),
          _factory.Instantiate<EnemyDieState>
          (
            new List<Transition>() 
          )
        },
      };
    }

    public Dictionary<Type, Transition> GetAnyStateTransitions()
    {
      return new Dictionary<Type, Transition>
      {
        {
          typeof(EnemyAnyStateToDie),
          _factory.Instantiate<EnemyAnyStateToDie>()
        },
      };
    }
  }
}