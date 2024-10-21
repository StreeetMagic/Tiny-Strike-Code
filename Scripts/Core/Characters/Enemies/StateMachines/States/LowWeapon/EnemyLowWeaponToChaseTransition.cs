using Core.Characters.Enemies.States.Chase;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.LowWeapon
{
  public class EnemyLowWeaponToChaseTransition : Transition
  {
    private readonly EnemyWeaponLowerer _lowerer;

    public EnemyLowWeaponToChaseTransition(EnemyWeaponLowerer lowerer)
    {
      _lowerer = lowerer;
    }

    public override void Tick()
    {
      _lowerer.Tick();

      if (_lowerer.IsCompleted)
        Enter<EnemyChaseState>();
    }
  }
}