using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerRescueHostageToIdleTransition : Transition
  {
    private readonly PlayerHostageLocator _playerHostageLocator;

    private bool _isActive;

    public PlayerRescueHostageToIdleTransition(PlayerHostageLocator playerHostageLocator, PlayerHealth playerHealth)
    {
      _playerHostageLocator = playerHostageLocator;
      playerHealth.Damaged += OnDamaged;
    }

    public override void Tick()
    {
      _isActive = true;

      if (!_playerHostageLocator.ClosestHostage.IsResqued())
        return;

      Enter<PlayerIdleState>();
      _isActive = false;
    }

    private void OnDamaged(float obj)
    {
      if (!_isActive)
        return;

      Enter<PlayerIdleState>();
      _isActive = false;
    }
  }
}