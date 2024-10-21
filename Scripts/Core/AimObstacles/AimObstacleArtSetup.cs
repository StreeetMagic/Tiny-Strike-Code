using System;
using AudioServices.Sounds;

namespace Core.AimObstacles
{
  [Serializable]
  public class AimObstacleArtSetup : ArtSetup<AimObstacleId>
  {
    public SoundId HitSound;
    public SoundId DestroySound;
  }
}