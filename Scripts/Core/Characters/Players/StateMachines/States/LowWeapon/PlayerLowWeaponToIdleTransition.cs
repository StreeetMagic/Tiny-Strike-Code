using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerLowWeaponToIdleTransition : Transition
  {
    private readonly PlayerWeaponLowTimer _playerWeaponLowTimer;

    public PlayerLowWeaponToIdleTransition(PlayerWeaponLowTimer playerWeaponLowTimer)
    {
      _playerWeaponLowTimer = playerWeaponLowTimer;
    }

    public override void Tick()
    {
      if (!_playerWeaponLowTimer.IsCompleted)
        Enter<PlayerIdleState>();

      _playerWeaponLowTimer.Tick();
    }
  }
}