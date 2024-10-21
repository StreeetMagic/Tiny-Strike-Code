using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerRiseWeaponToAttackTransition : Transition
  {
    private readonly WeaponRaiseTimer _timer;

    public PlayerRiseWeaponToAttackTransition(WeaponRaiseTimer timer)
    {
      _timer = timer;
    }

    public override void Tick()
    {
      if (_timer.IsCompleted)
        Enter<PlayerAttackState>();
      
      _timer.Tick();
    }
  }
}