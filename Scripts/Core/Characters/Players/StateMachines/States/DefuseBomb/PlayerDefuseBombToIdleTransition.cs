using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerDefuseBombToIdleTransition : Transition
  {
     private readonly PlayerBombLocator _playerBombLocator;
     
     private bool _isActive;

     public PlayerDefuseBombToIdleTransition(PlayerBombLocator playerBombLocator, PlayerHealth playerHealth)
     {
       _playerBombLocator = playerBombLocator;
       playerHealth.Damaged += OnDamaged;
     }

     public override void Tick()
    {
      _isActive = true;
      
      if (!_playerBombLocator.ClosestBomb.IsDefused())
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