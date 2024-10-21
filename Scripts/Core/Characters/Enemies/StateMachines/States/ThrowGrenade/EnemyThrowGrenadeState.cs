using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.ThrowGrenade
{
  public class EnemyThrowGrenadeState : State
  {
    private readonly EnemyConfig _config;
    private readonly EnemyGrenadeThrower _configGrenadeThrower;
    private readonly EnemyAnimatorProvider _animatorProvider;
    private readonly EnemyToPlayerRotator _rotator;
    private readonly EnemyGrenadeThrowTimer _grenadeThrowTimer;

    private bool _thrown;

    public EnemyThrowGrenadeState(List<Transition> transitions, EnemyConfig config,
      EnemyAnimatorProvider animatorProvider, EnemyGrenadeThrower configGrenadeThrower,
      EnemyToPlayerRotator rotator, EnemyGrenadeThrowTimer grenadeThrowTimer) : base(transitions)
    {
      _config = config;
      _animatorProvider = animatorProvider;
      _configGrenadeThrower = configGrenadeThrower;
      _rotator = rotator;
      _grenadeThrowTimer = grenadeThrowTimer;
    }

    public override void Enter()
    {
      _animatorProvider.Instance.GrenadeThrown += OnGrenadeThrown;
      _thrown = false;
    }

    protected override void OnTick() 
    {
      _rotator.Rotate();

      if (_thrown)
        return;

      _thrown = true;
      _animatorProvider.Instance.PlayGrenadeThrow(_config.GrenadeThrowDuration);
      _grenadeThrowTimer.Reset();
    }

    public override void Exit()
    {
      _animatorProvider.Instance.GrenadeThrown -= OnGrenadeThrown;
    }

    private void OnGrenadeThrown()
    {
      _configGrenadeThrower.Lauch();
    }
  }
}