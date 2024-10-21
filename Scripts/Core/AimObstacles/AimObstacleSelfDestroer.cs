using UnityEngine;
using Zenject;

namespace Core.AimObstacles
{
  public class AimObstacleSelfDestroer : IInitializable, ITickable
  {
    private readonly IHealth _health;
    private readonly GameObject _gameObject;

    private float _timeLeft;
    private bool _isDead;

    public AimObstacleSelfDestroer(IHealth health, GameObject gameObject)
    {
      _health = health;
      _gameObject = gameObject;
    }

    public void Initialize()
    {
      _health.Died += OnDied;
    }

    private void OnDied(IHealth health, int exp, float delay)
    {
      _timeLeft = delay;
      _isDead = true;
    }

    public void Tick()
    {
      if (!_isDead)
        return;

      if (_timeLeft <= 0)
        Object.Destroy(_gameObject);
      else
        _timeLeft -= Time.deltaTime;
    }
  }
}