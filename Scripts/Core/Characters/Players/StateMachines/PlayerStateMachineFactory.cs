using System;
using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players.AnyStateTransitions;
using Core.Characters.Players.States;
using ZenjectFactories.GameobjectContext;

namespace Core.Characters.Players
{
  public class PlayerStateMachineFactory : IStateMachineFactory
  {
    private readonly IGameObjectZenjectFactory _factory;

    public PlayerStateMachineFactory(IGameObjectZenjectFactory factory)
    {
      _factory = factory;
    }

    public Dictionary<Type, State> GetStates()
    {
      return new Dictionary<Type, State>
      {
        {
          typeof(PlayerBootstrapState),
          _factory.Instantiate<PlayerBootstrapState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerBootstrapToIdleTransition>()
            }
          )
        },

        {
          typeof(PlayerIdleState),
          _factory.Instantiate<PlayerIdleState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerIdleToRiseWeaponTransition>(),
              _factory.Instantiate<PlayerIdleToRescueHostageTransition>(),
              _factory.Instantiate<PlayerIdleToDefuseBombTransition>(),
            }
          )
        },

        {
          typeof(PlayerRiseWeaponState),
          _factory.Instantiate<PlayerRiseWeaponState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerRiseWeaponToIdleTransition>(),
              _factory.Instantiate<PlayerRiseWeaponToAttackTransition>(),
            }
          )
        },

        {
          typeof(PlayerAttackState),
          _factory.Instantiate<PlayerAttackState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerAttackToLowWeaponTransition>(),
            }
          )
        },

        {
          typeof(PlayerLowWeaponState),
          _factory.Instantiate<PlayerLowWeaponState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerLowWeaponToIdleTransition>(),
            }
          )
        },

        {
          typeof(PlayerRescueHostageState),
          _factory.Instantiate<PlayerRescueHostageState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerRescueHostageToIdleTransition>(),
            }
          )
        },

        {
          typeof(PlayerDefuseBombState),
          _factory.Instantiate<PlayerDefuseBombState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerDefuseBombToIdleTransition>(),
            }
          )
        },

        {
          typeof(PlayerMoveState),
          _factory.Instantiate<PlayerMoveState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerMoveToIdleTransition>(),
            }
          )
        },

        {
          typeof(PlayerDieState),
          _factory.Instantiate<PlayerDieState>
          (
            new List<Transition>
            {
              _factory.Instantiate<PlayerDieToIdleTransition>(),
            }
          )
        },
      };
    }

    public Dictionary<Type, Transition> GetAnyStateTransitions()
    {
      return new Dictionary<Type, Transition>
      {
        {
          typeof(PlayerAnyStateToMoveTransition),
          _factory.Instantiate<PlayerAnyStateToMoveTransition>()
        },
        {
          typeof(PlayerAnyStateToDieTransition),
          _factory.Instantiate<PlayerAnyStateToDieTransition>()
        },
      };
    }
  }
}