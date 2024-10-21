using Core.Characters.FiniteStateMachines;
using Core.Characters.Players.States;

namespace Core.Characters.Players.AnyStateTransitions
{
  public class PlayerAnyStateToDieTransition : Transition
  {
    private readonly PlayerHealth _playerHealth;

    public PlayerAnyStateToDieTransition(PlayerHealth playerHealth)
    {
      _playerHealth = playerHealth;
    }

    public override void Tick()
    {
      if (_playerHealth.IsDead)
        return;
      
      if (_playerHealth.IsImmortal)
        return;

      if (_playerHealth.Current.Value <= 0)
        Enter<PlayerDieState>();
    }
  }
}