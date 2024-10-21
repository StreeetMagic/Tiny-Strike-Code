using TimeServices;
using UnityEngine;
using Zenject;

namespace Core.Grenades
{
  public class Grenade : MonoBehaviour
  {
    [Inject] private TimeService _timeService;

    private void Update()
    {
      if (_timeService.IsPaused)
        Destroy(gameObject);
    }
  }
}