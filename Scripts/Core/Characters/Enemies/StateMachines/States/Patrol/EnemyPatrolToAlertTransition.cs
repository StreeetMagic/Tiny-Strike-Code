using Core.Characters.Enemies.States.Alert;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Patrol
{
  public class EnemyPatrolToAlertTransition : Transition
  {
    private readonly HitStatus _hitStatus;
    private readonly EnemyConfig _enemyConfig;

    public EnemyPatrolToAlertTransition(HitStatus hitStatus, EnemyConfig enemyConfig)
    {
      _hitStatus = hitStatus;
      _enemyConfig = enemyConfig;
    }

    public override void Tick()
    {
      if (_enemyConfig.IsAttacker == false)
        return;
      
      if (_hitStatus.IsHit)
        Enter<EnemyAlertState>();
    }
  }
}