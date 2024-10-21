using UnityEngine;
using Zenject;

namespace Core.AimObstacles
{
  public class AimObstacleActorUserInterface : MonoBehaviour
  {
    public GameObject[] Components;
    
    [Inject] private IHealth _health;

    private void OnEnable()
    {
      _health.Died += OnDied;
    }

    private void OnDisable()
    {
      _health.Died -= OnDied;
    }

    private void OnDied(IHealth enemyHealth, int expirience, float corpseRemoveDelay)
    {
      foreach (GameObject component in Components)
        component.SetActive(false);
    }
  }
}