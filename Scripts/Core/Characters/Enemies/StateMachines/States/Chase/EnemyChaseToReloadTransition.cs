using Core.Characters.Enemies.States.Reload;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Chase
{
  public class EnemyChaseToReloadTransition : Transition
  {
    private readonly EnemyWeaponMagazine _magazine;

    public EnemyChaseToReloadTransition(EnemyWeaponMagazine magazine)
    {
      _magazine = magazine;
    }

    public override void Tick()
    {
      if (_magazine.IsEmpty)
        Enter<EnemyReloadState>();
    }
  }
}