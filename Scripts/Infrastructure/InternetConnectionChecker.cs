using UnityEngine;
using Projects;
using Zenject;

public class InternetConnectionChecker : ITickable
{
  private const float UpdateInterval = 1f;

  private float _timeLeft;

  [Inject]
  private ProjectData _projectData;

  public void Tick()
  {
    _timeLeft -= Time.deltaTime;

    if (_timeLeft > 0)
      return;

    switch (Application.internetReachability)
    {
      case NetworkReachability.NotReachable:
        _projectData.DisableInternetConnection();
        break;

      case NetworkReachability.ReachableViaCarrierDataNetwork:
        _projectData.EnableInternetConnection();
        break;

      case NetworkReachability.ReachableViaLocalAreaNetwork:
        _projectData.EnableInternetConnection();
        break;
    }

    _timeLeft = UpdateInterval;
  }
}