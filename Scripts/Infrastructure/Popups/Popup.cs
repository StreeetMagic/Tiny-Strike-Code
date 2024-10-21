using UnityEngine;

namespace Popups
{
  public class Popup : MonoBehaviour
  {
    public bool DestoroySelf;
    public float SecondsToDestroy = 2f;

    private float _timeLeft;

    private void OnEnable()
    {
      _timeLeft = SecondsToDestroy; 
    }

    private void Update()
    {
      if (!DestoroySelf)
        return;

      _timeLeft -= Time.deltaTime;

      if (_timeLeft < 0)
      {
        Destroy(gameObject);
      }
    }
  }
}