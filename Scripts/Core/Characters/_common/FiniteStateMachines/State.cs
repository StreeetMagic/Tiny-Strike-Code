using System;
using System.Collections.Generic;
using Zenject;

namespace Core.Characters.FiniteStateMachines
{
  public abstract class State : ITickable
  {
    private State _activeState;

    private readonly List<Transition> _transitions;

    protected State(List<Transition> transitions)
    {
      _transitions = transitions;

      foreach (Transition transition in _transitions)
      {
        transition.Entered += Enter;
      }
    }

    public event Action<Type> Entered;

    public void Tick()
    {
      OnTick();

      foreach (Transition transition in _transitions)
      {
        transition.SetActiveState(_activeState);
        transition.Tick();
      }
    }

    public abstract void Enter();
    protected abstract void OnTick();
    public abstract void Exit();

    public void SetActiveState(State state)
    {
      _activeState = state;
    }

    private void Enter(Type type)
    {
      Entered?.Invoke(type);
    }
  }
}