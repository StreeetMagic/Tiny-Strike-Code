using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Movers;
using Core.Characters.Players;

namespace Core.Characters.Enemies.States.Chase
{
  public class EnemyChaseState : State
  {
    private readonly IMover _mover;
    private readonly EnemyAnimatorProvider _animatorProvider;
    private readonly PlayerProvider _playerProvider;
    private readonly EnemyConfig _config;
    private readonly HitStatus _hitStatus;

    public EnemyChaseState(List<Transition> transitions, IMover mover, 
      EnemyAnimatorProvider animatorProvider, PlayerProvider playerProvider, EnemyConfig config, 
      HitStatus hitStatus) : base(transitions)
    {
      _mover = mover;
      _animatorProvider = animatorProvider;
      _playerProvider = playerProvider;
      _config = config;
      _hitStatus = hitStatus;
    }

    public override void Enter()
    {
      _animatorProvider.Instance.PlayRun();
    }

    protected override void OnTick()
    {
      _mover.SetDestination(_playerProvider.Instance.transform.position, _config.RunSpeed);
    }

    public override void Exit()
    {
      _mover.Stop();
      _animatorProvider.Instance.StopRun();
      _hitStatus.IsHit = false;
    }
  }
}