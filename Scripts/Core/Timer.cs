using UnityEngine;

namespace Core
{
  public class Timer
  {
    private float _timeLeft;

    public bool IsCompleted => _timeLeft <= 0;

    public void Set(float time)
    {
      _timeLeft = time;
    }

    public void Tick()
    {
      if (_timeLeft > 0)
        _timeLeft -= Time.deltaTime;

      if (_timeLeft < 0)
        _timeLeft = 0;
    }
  }
}