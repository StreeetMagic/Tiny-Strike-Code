using System;
using Meta.Expirience;
using Zenject;

namespace Core.AimObstacles
{
  public class AimObstacleExpierience : IInitializable, IDisposable
  {
    private readonly AimObstacle _aimObstacle;
    private readonly IHealth _health;
    private readonly ExpierienceStorage _expierienceStorage;

    public AimObstacleExpierience(AimObstacle aimObstacle, IHealth health, ExpierienceStorage expierienceStorage)
    {
      _aimObstacle = aimObstacle;
      _health = health;
      _expierienceStorage = expierienceStorage; 
    }

    public void Initialize()
    {
      _health.Died += OnDied;
    }

    public void Dispose()
    {
      _health.Died -= OnDied; 
    }

    private void OnDied(IHealth health, int arg2, float arg3)
    {
       if (_aimObstacle.Expirience > 0)
         _expierienceStorage.AllPoints.Value += _aimObstacle.Expirience;
    }
  }
}