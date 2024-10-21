using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerRiseWeaponToIdleTransition : Transition
  {
    private readonly PlayerTargetHolder _playerTargetHolder;

    public PlayerRiseWeaponToIdleTransition(PlayerTargetHolder playerTargetHolder)
    {
      _playerTargetHolder = playerTargetHolder;
    }

    public override void Tick()
    {
      if (_playerTargetHolder.HasTarget == false)
        Enter<PlayerIdleState>();
    }
  }
}