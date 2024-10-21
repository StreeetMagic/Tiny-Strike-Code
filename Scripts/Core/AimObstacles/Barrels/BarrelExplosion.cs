using UnityEngine;
using Zenject;

namespace Core.AimObstacles.Barrels
{
  public class BarrelExplosion : MonoBehaviour
  {
    private const int Count = 50; 
    
    public float Radius = 5;
    public float Damage = 50;

    [Inject] private IHealth _health;
    
    private readonly Collider[] _colliders = new Collider[Count];

    private void OnEnable()
    {
      _health.Died += OnDied;
    }

    private void OnDisable()
    {
      _health.Died -= OnDied;
    }

    private void OnDied(IHealth arg1, int arg2, float arg3)
    {
      int count = Physics.OverlapSphereNonAlloc(transform.position, Radius, _colliders); 
      
      for (int i = 0; i < count; i++)
      {
        if (_colliders[i].TryGetComponent(out ITargetTrigger targetTrigger))
          targetTrigger.TakeDamage(Damage);
      }
    }
  }
}