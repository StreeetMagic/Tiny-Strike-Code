using System;
using Loggers;
using Zenject;

namespace Core.Characters.FiniteStateMachines
{
  public abstract class Transition : ITickable
  {
    protected State ToState;

    private readonly DebugLogger _logger = new DebugLogger();

    private int _processCount;

    public event Action<Type> Entered;

    public abstract void Tick();

    public void SetActiveState(State state)
    {
      ToState = state;
    }

    protected void Enter<T>() where T : State
    {
      if (ToState.GetType() == typeof(T))
        return;

      string message = GetType().Name;
      message = message.Replace("Transition", "");
      message = message.Replace("State", "");
      _logger.LogTransition($"<color=yellow>#{message}</color>");

      Entered?.Invoke(typeof(T));
    }
  }
}