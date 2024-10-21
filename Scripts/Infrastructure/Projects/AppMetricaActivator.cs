using Io.AppMetrica;
using UnityEngine;

public static class AppMetricaActivator 
{
  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  private static void Activate() {
    AppMetrica.Activate(new AppMetricaConfig("9c14c169-acb3-4a1b-ae8b-64658a1a1a32") {
      LocationTracking = true,
      NativeCrashReporting = true,
      Logs = true,
      RevenueAutoTrackingEnabled = true,
      SessionsAutoTrackingEnabled = true,
      CrashReporting = true,
      
      FirstActivationAsUpdate = !IsFirstLaunch(),
    });
  }

  private static bool IsFirstLaunch() {
    // Implement logic to detect whether the app is opening for the first time.
    // For example, you can check for files (settings, databases, and so on),
    // which the app creates on its first launch.
    return true;
  }
}