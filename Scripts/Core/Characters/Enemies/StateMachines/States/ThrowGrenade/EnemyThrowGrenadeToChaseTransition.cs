using Core.Characters.Enemies.States.Chase;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;
using UnityEngine;

namespace Core.Characters.Enemies.States.ThrowGrenade
{
  public class EnemyThrowGrenadeToChaseTransition : Transition
  {
    private readonly EnemyConfig _config;
    private readonly PlayerProvider _playerProvider;
    
    private float _timeLeft;

    public EnemyThrowGrenadeToChaseTransition(EnemyConfig config, PlayerProvider playerProvider)
    {
      _config = config;
      _playerProvider = playerProvider;
      _timeLeft = config.GrenadeThrowDuration;
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
      
      _timeLeft -= Time.deltaTime;

      if (_timeLeft < 0)
      {
        _timeLeft = _config.GrenadeThrowDuration;

        Enter<EnemyChaseState>();
      }
    }
  }
}