using UnityEngine;

namespace Meta
{
  [CreateAssetMenu(fileName = "Quest", menuName = "ArtConfigs/Quest")]
  public class QuestArtConfig : ArtConfig<QuestArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}