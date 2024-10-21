using UnityEngine;
using Zenject;

namespace Core.AimObstacles
{
  public class AimObstacleHealthBarSwitcher : MonoBehaviour
  {
    public GameObject[] Components;

    [Inject] private IHealth _health;

    public void Start()
    {
      _health.Current.ValueChanged += OnHealthChanged;
      OnHealthChanged(0);
    }

    public void OnDestroy()
    {
      _health.Current.ValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float _)
    {
      if (_health.IsFull)
        foreach (GameObject component in Components)
          component.SetActive(false);
      else
        foreach (GameObject component in Components)
          component.SetActive(true);
    }
  }
}