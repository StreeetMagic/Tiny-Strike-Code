using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using UnityEngine;

namespace Core.Characters.Enemies.States.MeleeAttack
{
  public class EnemyMeleeAttackState : State
  {
    private readonly EnemyConfig _config;
    private readonly EnemyMeleeAttacker _meleeAttacker;
    private readonly EnemyAnimatorProvider _animatorProvider;
    private readonly EnemyToPlayerRotator _toPlayerRotator;

    private float _timeLeft;

    public EnemyMeleeAttackState(List<Transition> transitions, EnemyConfig config,
      EnemyMeleeAttacker meleeAttacker, EnemyAnimatorProvider animatorProvider,
      EnemyToPlayerRotator toPlayerRotator) : base(transitions)
    {
      _config = config;
      _meleeAttacker = meleeAttacker;
      _animatorProvider = animatorProvider;
      _toPlayerRotator = toPlayerRotator;
    }

    public override void Enter()
    {
      _animatorProvider.Instance.KnifeHit += OnHitEventListener;
      _timeLeft = 0;
    }

    protected override void OnTick()
    {
      _toPlayerRotator.Rotate();

      if (_timeLeft <= 0)
        StartAttack();

      _timeLeft -= Time.deltaTime;
    }

    public override void Exit()
    {
      _animatorProvider.Instance.KnifeHit -= OnHitEventListener;
    }

    private void StartAttack()
    {
      _animatorProvider.Instance.PlayRandomKnifeHitAnimation(_config.MeeleAttackDuration);
      _timeLeft = _config.MeeleAttackDuration;
    }

    private void OnHitEventListener()
    {
      _meleeAttacker.Attack();
    }
  }
}