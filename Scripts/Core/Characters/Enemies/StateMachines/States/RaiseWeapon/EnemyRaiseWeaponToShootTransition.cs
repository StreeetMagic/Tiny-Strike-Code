using Core.Characters.Enemies.States.Shoot;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.RaiseWeapon
{
  public class EnemyRaiseWeaponToShootTransition : Transition
  {
    private readonly WeaponRaiseTimer _weaponRaiser;

    public EnemyRaiseWeaponToShootTransition(WeaponRaiseTimer weaponRaiser)
    {
      _weaponRaiser = weaponRaiser;
    }

    public override void Tick()
    {
      _weaponRaiser.Tick();
      
      if (_weaponRaiser.IsCompleted)
        Enter<EnemyShootState>();
    }
  }
}