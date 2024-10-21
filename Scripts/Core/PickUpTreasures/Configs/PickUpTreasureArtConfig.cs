using UnityEngine;

namespace Core.PickUpTreasures.Configs
{
  [CreateAssetMenu(fileName = "PickUpTreasure", menuName = "ArtConfigs/PickUpTreasure")]
  public class PickUpTreasureArtConfig : ArtConfig<PickUpTreasureArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}