using System;
using System.Collections.Generic;

namespace Core.Characters.FiniteStateMachines
{
  public interface IStateMachineFactory
  {
    Dictionary<Type, State> GetStates();
    Dictionary<Type, Transition> GetAnyStateTransitions();
  }
}