using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  [RequireComponent(typeof(Collider))]
  public class PlayerTargetTrigger : MonoBehaviour
  {
    public Collider Collider { get; private set; }

    [Inject] private PlayerHealth _playerHealth;

    public bool IsTargeted { get; set; }
    
    private void Awake()
    {
      Collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
      _playerHealth.Died += OnDied;
    }

    private void OnDisable()
    {
      _playerHealth.Died -= OnDied;
    }

    public void TakeDamage(float damage)
    {
      _playerHealth.TakeDamage(damage);
    }

    private void OnDied()
    {
      Collider.enabled = false;
    }
  }
}