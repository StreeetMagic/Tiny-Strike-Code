using UnityEngine;

namespace Meta.ChestRewards
{
  [CreateAssetMenu(fileName = "ChestReward", menuName = "ArtConfigs/ChestReward")]
  public class ChestRewardArtConfig : ArtConfig<ChestRewardArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}