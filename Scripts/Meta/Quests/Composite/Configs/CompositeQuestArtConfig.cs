using UnityEngine;

namespace Meta
{
  [CreateAssetMenu(menuName = "ArtConfigs/CompositeQuest", fileName = "CompositeQuest")]
  public class CompositeQuestArtConfig : ArtConfig<CompositeQuestArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}