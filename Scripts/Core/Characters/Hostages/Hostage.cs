using LevelDesign;
using UnityEngine;

namespace Core.Characters
{
  public class Hostage : MonoBehaviour
  {
    public float ResqueProgress { get; private set; }
    public HostageSpawnMarker SpawnMarker { get; set; }

    public bool IsResqued() =>
      ResqueProgress >= 1;

    public void ResqueTick(float wholeDuration)
    {
      ResqueProgress += Time.deltaTime / wholeDuration;

      if (ResqueProgress > 1)
        ResqueProgress = 1;
    }

    public void ResetProgress()
    {
      ResqueProgress = 0;
    }
  }
}