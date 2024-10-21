using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.LowWeapon
{
  public class EnemyLowWeaponState : State
  {
    private readonly EnemyWeaponLowerer _lowerer;
    private readonly EnemyAnimatorProvider _enemyAnimatorProvider;
    private readonly EnemyConfig _enemyConfig;

    public EnemyLowWeaponState(List<Transition> transitions, EnemyWeaponLowerer lowerer, EnemyAnimatorProvider enemyAnimatorProvider, 
      EnemyConfig enemyConfig) : base(transitions)
    {
      _lowerer = lowerer;
      _enemyAnimatorProvider = enemyAnimatorProvider;
      _enemyConfig = enemyConfig;
    }

    public override void Enter()
    {
      _lowerer.Set(_enemyConfig.WeaponLoweringTime);
      _enemyAnimatorProvider.Instance.OffStateShooting();
    }

    protected override void OnTick()
    {
    }

    public override void Exit()
    {
    }
  }
}