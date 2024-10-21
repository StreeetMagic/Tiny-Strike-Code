using System;
using System.Collections.Generic;
using Core.Characters.Companions.States;
using Core.Characters.Companions.States.LowWeapon;
using Core.Characters.Companions.States.RaiseWeapon;
using Core.Characters.Companions.States.Shoot;
using Core.Characters.FiniteStateMachines;
using ZenjectFactories.GameobjectContext;

namespace Core.Characters.Companions
{
  public class CompanionStateMachineFactory : IStateMachineFactory
  {
    private readonly IGameObjectZenjectFactory _factory;

    public CompanionStateMachineFactory(IGameObjectZenjectFactory factory)
    {
      _factory = factory;
    }

    public Dictionary<Type, State> GetStates()
    {
      return new Dictionary<Type, State>
      {
        {
          typeof(CompanionBootstrapState),
          _factory.Instantiate<CompanionBootstrapState>
          (
            new List<Transition>
            {
              _factory.Instantiate<CompanionBootstrapToIdleTransition>()
            }
          )
        },

        {
          typeof(CompanionIdleState),
          _factory.Instantiate<CompanionIdleState>
          (
            new List<Transition>
            {
              _factory.Instantiate<CompanionIdleToFollowPlayerTransition>(),
              _factory.Instantiate<CompanionIdleToRaiseWeaponTransition>()
            }
          )
        },

        {
          typeof(CompanionFollowPlayerState),
          _factory.Instantiate<CompanionFollowPlayerState>
          (
            new List<Transition>
            {
              _factory.Instantiate<CompanionFollowPlayerToIdleTransition>()
            }
          )
        },

        {
          typeof(CompanionRaiseWeaponState),
          _factory.Instantiate<CompanionRaiseWeaponState>
          (
            new List<Transition>
            {
              _factory.Instantiate<CompanionRaiseWeaponToShootTransition>()
            }
          )
        },

        {
          typeof(CompanionShootState),
          _factory.Instantiate<CompanionShootState>
          (
            new List<Transition>
            {
              _factory.Instantiate<CompanionShootToLowWeaponTransition>()
            }
          )
        },

        {
          typeof(CompanionLowWeaponState),
          _factory.Instantiate<CompanionLowWeaponState>
          (
            new List<Transition>
            {
              _factory.Instantiate<CompanionLowWeaponToIdleTransition>()
            }
          )
        }
      };
    }

    public Dictionary<Type, Transition> GetAnyStateTransitions()
    {
      return new Dictionary<Type, Transition>();
    }
  }
}