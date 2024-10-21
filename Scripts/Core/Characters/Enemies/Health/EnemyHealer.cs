using UnityEngine;

namespace Core.Characters.Enemies
{
  public class EnemyHealer
  {
    private readonly IHealth _health;
    private readonly EnemyConfig _config;

    private float _heal;
    private float _timer;

    public EnemyHealer(IHealth enemyHealth, EnemyConfig config)
    {
      _health = enemyHealth;
      _config = config;
    }

    public void Heal()
    {
      if (_health.Current.Value >= _health.Initial)
        return;

      float healthPerSecond = _health.Initial * _config.HealthRegenPercentPerSecond / 100;
      float healthPerFrame = healthPerSecond * Time.deltaTime;

      _heal += healthPerFrame; 
      _health.Current.Value = Mathf.Min(_health.Current.Value + _heal, _health.Initial);
      _heal = 0;
    }
  }
}