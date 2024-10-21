using UnityEngine;

namespace Meta.LevelUp
{
  [CreateAssetMenu(fileName = "LevelUp", menuName = "ArtConfigs/LevelUp")]
  public class LevelUpRewardArtConfig : ArtConfig<LevelUpRewardArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}