using Core.Characters.Enemies.Phases;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class EnemyGrenadeThrowTimer : ITickable
  {
    private readonly EnemyConfig _config;
    private readonly EnemyPhase _enemyPhase;
    private float _timeLeft;

    public EnemyGrenadeThrowTimer(EnemyConfig config, EnemyPhase enemyPhase)
    {
      _config = config;
      _enemyPhase = enemyPhase;
    }

    public bool IsUp => _timeLeft < 0;

    public void Reset()
    {
      _timeLeft = ConfigGrenadeThrowCooldown();
    }

    public void Tick()
    {
      _timeLeft -= Time.deltaTime;
    }

    private float ConfigGrenadeThrowCooldown()
    {
      if (_enemyPhase.Passed)
        return _config.GrenadeThrowCooldown * _config.GrenadeThrowDurationMultiplier;
      
      return _config.GrenadeThrowCooldown;
    }
  }
}