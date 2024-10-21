using Core.Characters.Enemies.States.MeleeAttack;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;
using UnityEngine;

namespace Core.Characters.Enemies.States.Chase
{
  public class EnemyChaseToMeleeAttackTransition : Transition
  {
    private readonly EnemyConfig _config;
    private readonly PlayerProvider _playerProvider;
    private readonly Transform _transform;

    public EnemyChaseToMeleeAttackTransition(EnemyConfig config, PlayerProvider playerProvider,
      Transform transform)
    {
      _config = config;
      _playerProvider = playerProvider;
      _transform = transform;
    }

    public override void Tick()
    {
      if (Vector3.Distance(_transform.position, _playerProvider.Instance.transform.position) < _config.MeleeRange)
        Enter<EnemyMeleeAttackState>();
    }
  }
}