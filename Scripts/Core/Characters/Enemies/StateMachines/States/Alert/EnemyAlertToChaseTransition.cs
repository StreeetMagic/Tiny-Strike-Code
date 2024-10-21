using Core.Characters.Enemies.States.Chase;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Alert
{
  public class EnemyAlertToChaseTransition : Transition
  {
    private readonly EnemyAlertTimer _alertTimer;

    public EnemyAlertToChaseTransition(EnemyAlertTimer alertTimer)
    {
      _alertTimer = alertTimer;
    }

    public override void Tick()
    {
      if (_alertTimer.IsCompleted)
        Enter<EnemyChaseState>();
    }
  }
}