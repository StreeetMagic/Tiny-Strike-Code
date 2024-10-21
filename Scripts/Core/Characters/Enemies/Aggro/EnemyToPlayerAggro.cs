using Core.Characters.Players;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class EnemyToPlayerAggro : ITickable
  {
    private readonly PlayerProvider _playerProvider;
    private readonly EnemyConfig _enemyConfig;
    private readonly Transform _transform;
    private readonly IHealth _health;
    private readonly HitStatus _hitStatus;

    public EnemyToPlayerAggro(PlayerProvider playerProvider, EnemyConfig enemyConfig, Transform transform, IHealth health, HitStatus hitStatus)
    {
      _playerProvider = playerProvider;
      _enemyConfig = enemyConfig;
      _transform = transform;
      _health = health;
      _hitStatus = hitStatus;
    }

    public void Tick()
    {
      if (_hitStatus.IsHit)
        return;

      if (!_playerProvider.Instance)
        return;

      float aggroDistance = _enemyConfig.AggroRadius;

      float distance = Vector3.Distance(_playerProvider.Instance.transform.position, _transform.position);

      if (distance > aggroDistance)
        return;

      _health.NotifyOtherEnemies();
    }
  }
}