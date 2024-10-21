using Core.Characters.Enemies.States.Chase;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Reload
{
  public class EnemyReloadToChaseTransition : Transition
  {
    private readonly EnemyWeaponMagazine _magazine;

    public EnemyReloadToChaseTransition(EnemyWeaponMagazine magazine)
    {
      _magazine = magazine;
    }

    public override void Tick()
    {
      if (_magazine.IsEmpty == false)
        Enter<EnemyChaseState>();
    }
  }
}