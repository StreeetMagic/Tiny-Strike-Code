using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class EnemyActorUserInterface : MonoBehaviour
  {
    public GameObject[] Components;

    [Inject] private IHealth _enemyHealth;

    private void OnEnable()
    {
      _enemyHealth.Died += OnDied;

      foreach (GameObject component in Components)
      {
        if (component.activeSelf == false)
          component.SetActive(true);
      }
    }

    private void OnDisable()
    {
      _enemyHealth.Died -= OnDied;
    }

    private void OnDied(IHealth enemyHealth, int expirity, float corpseRemoveDelay)
    {
      foreach (GameObject component in Components)
        if (component.activeSelf)
          component.SetActive(false);
    }
  }
}