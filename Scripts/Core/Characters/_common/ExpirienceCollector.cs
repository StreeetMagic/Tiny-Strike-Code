using System;
using Meta.Expirience;
using Zenject;

namespace Core.Characters
{
  public class ExpirienceCollector : IInitializable, IDisposable
  {
    private readonly ExpierienceStorage _expierienceStorage;
    private readonly IHealth _health;
    
    public ExpirienceCollector(ExpierienceStorage expierienceStorage, IHealth health)
    {
      _expierienceStorage = expierienceStorage;
      _health = health;
    }

    public void Initialize()
    {
      _health.Died += OnDied;
    }

    public void Dispose()
    {
      _health.Died -= OnDied;
    }

    private void OnDied(IHealth enemyHealth, int expirience, float corpseRemoveDelay)
    {
      _expierienceStorage.AllPoints.Value += expirience;
    }
  }
}