using Core.Characters.Enemies.States.Chase;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;
using UnityEngine;

namespace Core.Characters.Enemies.States.MeleeAttack
{
  public class EnemyMeleeAttackToChaseTransition : Transition
  {
    private readonly EnemyConfig _config;
    private readonly PlayerProvider _playerProvider;
    private readonly Transform _transform;

    public EnemyMeleeAttackToChaseTransition(EnemyConfig config, PlayerProvider playerProvider,
      Transform transform)
    {
      _config = config;
      _playerProvider = playerProvider;
      _transform = transform;
    }

    public override void Tick()
    {
      if (!_playerProvider.Instance)
      {
        Enter<EnemyChaseState>();
        return;
      }
      
      if (_playerProvider.Instance.Health.IsDead)
      {
        Enter<EnemyChaseState>();
        return;
      }

      if (Vector3.Distance(_transform.position, _playerProvider.Instance.transform.position) >= _config.MeleeRange)
      {
        Enter<EnemyChaseState>();
      }
    }
  }
}