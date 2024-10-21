using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.RaiseWeapon
{
  public class EnemyRaiseWeaponState : State
  {
    private readonly EnemyAnimatorProvider _enemyAnimatorProvider;
    private readonly WeaponRaiseTimer _weaponRaiser;
    private readonly EnemyConfig _enemyConfig;

    public EnemyRaiseWeaponState(List<Transition> transitions,
      WeaponRaiseTimer weaponRaiser, EnemyAnimatorProvider enemyAnimatorProvider, EnemyConfig enemyConfig) : base(transitions)
    {
      _weaponRaiser = weaponRaiser;
      _enemyAnimatorProvider = enemyAnimatorProvider;
      _enemyConfig = enemyConfig;
    }

    public override void Enter()
    {
      _weaponRaiser.Set(_enemyConfig.WeaponRisingTime);
      _enemyAnimatorProvider.Instance.OnStateShooting();
    }

    protected override void OnTick()
    {
    }

    public override void Exit()
    {
    }
  }
}