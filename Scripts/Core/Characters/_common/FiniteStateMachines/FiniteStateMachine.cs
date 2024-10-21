using System;
using System.Collections.Generic;
using System.Linq;
using TimeServices;
using Zenject;

namespace Core.Characters.FiniteStateMachines
{
  public class FiniteStateMachine : ITickable
  {
    private readonly Dictionary<Type, Transition> _anyStateTransitions;
    private readonly Dictionary<Type, State> _states;
    private readonly TimeService _timeService;
    private DateTime _stateEnterTime;
  //  private readonly DebugLogger _logger = new();

    public FiniteStateMachine(IStateMachineFactory stateMachineFactory, TimeService timeService)
    {
      _timeService = timeService;
      _states = stateMachineFactory.GetStates();
      _anyStateTransitions = stateMachineFactory.GetAnyStateTransitions();

      ActiveState = _states.Values.First();
      EnterActiveState();

      foreach (State state in _states.Values)
        state.Entered += OnEntered;

      foreach (Transition transition in _anyStateTransitions.Values)
        transition.Entered += OnEntered;
    }

    public State ActiveState { get; private set; }

    public void Tick()
    {
      if (_timeService.IsPaused)
        return;

      foreach (Transition transition in _anyStateTransitions.Values)
      {
        transition.SetActiveState(ActiveState);
        transition.Tick();
      }

      ActiveState.SetActiveState(ActiveState);
      ActiveState.Tick();
    }

    public void OnEntered(Type toState)
    {
      if (_states.TryGetValue(toState, out State state) == false)
        throw new Exception($"State {toState} not found");

      if (state == ActiveState)
        return;

      ExitActiveState();
      ActiveState = _states[toState];
      EnterActiveState();
    }

    private void EnterActiveState()
    {
      // _stateEnterTime = xDateTime.Now; // Запоминаем время входа в состояние

      ActiveState.Enter();
      //
      // string name = ActiveState.GetType().Name;
      // name = name.Replace("State", "");
      // _logger.LogStateEnter($"<color=green>>{name}</color>");
    }

    private void ExitActiveState()
    {
     // TimeSpan timeInState = DateTime.Now - _stateEnterTime; // Вычисляем время, проведенное в состоянии

      ActiveState.Exit();
      //
      // string name = ActiveState.GetType().Name;
      // name = name.Replace("State", "");
      // _logger.LogStateExit($"<color=red><{name}</color> (Time in state: {timeInState.TotalSeconds:F2} seconds)");
    }
  }
}