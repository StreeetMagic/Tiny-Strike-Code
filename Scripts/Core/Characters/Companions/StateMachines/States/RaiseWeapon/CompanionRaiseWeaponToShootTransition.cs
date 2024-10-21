using Core.Characters.Companions.States.Shoot;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Companions.States.RaiseWeapon
{
  public class CompanionRaiseWeaponToShootTransition : Transition
  {
    private readonly WeaponRaiseTimer _timer;

    public CompanionRaiseWeaponToShootTransition(WeaponRaiseTimer timer)
    {
      _timer = timer;
    }

    public override void Tick()
    {
      if (_timer.IsCompleted)
        Enter<CompanionShootState>();
    }
  }
}