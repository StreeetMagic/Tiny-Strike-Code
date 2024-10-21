using UnityEngine;

namespace Core.AimObstacles
{
  [CreateAssetMenu(fileName = "AimObstacle", menuName = "ArtConfigs/AimObstacle")]
  public class AimObstacleArtConfig : ArtConfig<AimObstacleArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}