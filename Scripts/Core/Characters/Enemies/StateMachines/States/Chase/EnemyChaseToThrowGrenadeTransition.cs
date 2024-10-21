using Core.Characters.Enemies.States.ThrowGrenade;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Chase
{
  public class EnemyChaseToThrowGrenadeTransition : Transition
  {
    private readonly EnemyGrenadeThrowerStatus _grenadeThrowerStatus;

    public EnemyChaseToThrowGrenadeTransition(EnemyGrenadeThrowerStatus grenadeThrowerStatus)
    {
      _grenadeThrowerStatus = grenadeThrowerStatus;
    }

    public override void Tick()
    {
      if (_grenadeThrowerStatus.IsReady())
        Enter<EnemyThrowGrenadeState>();
    }
  }
}