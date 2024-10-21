using System;

namespace TimeServices
{
  public class TimeService
  {
    public event Action Paused;
    public event Action UnPaused;

    public bool IsPaused { get; private set; }

    public void Pause(string sender = null)
    {
      IsPaused = true;

      Paused?.Invoke();

      //Debug.Log("Time paused by " + sender);
    }

    public void UnPause()
    {
      IsPaused = false;

      UnPaused?.Invoke();
    }
  }
}