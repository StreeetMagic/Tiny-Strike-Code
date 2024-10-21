using Core.Characters.Enemies.States.Return;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;
using Core.Spawners.Enemies;
using UnityEngine;

namespace Core.Characters.Enemies.States.Chase
{
  public class EnemyChaseToReturnTransition : Transition
  {
    private readonly EnemySpawner _spawner;
    private readonly EnemyConfig _config;
    private readonly Transform _transform;
    private readonly PlayerProvider _playerProvider;

    public EnemyChaseToReturnTransition(EnemySpawner spawner,
      EnemyConfig config, Transform transform, PlayerProvider playerProvider)
    {
      _spawner = spawner;
      _config = config;
      _transform = transform;
      _playerProvider = playerProvider;
    }

    public override void Tick()
    {
      bool away = Vector3.Distance(_spawner.Transform.position, _transform.position) > _config.PatrolingRadius;
      bool playerDead = PlayerIsDead();

      if (playerDead || away)
      {
        Enter<EnemyReturnState>();
      }
    }

    private bool PlayerIsDead()
    {
      if (!_playerProvider.Instance)
        return false;

      return _playerProvider.Instance.Health.IsDead;
    }
  }
}