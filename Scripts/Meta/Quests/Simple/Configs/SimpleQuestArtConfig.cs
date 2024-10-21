using Loggers;
using UnityEngine;

namespace Meta.Configs
{
  [CreateAssetMenu(menuName = "ArtConfigs/SimpleQuest", fileName = "SimpleQuest")]
  public class SimpleQuestArtConfig : ArtConfig<SimpleQuestArtSetup>
  {
    protected override void Validate()
    {
      foreach (SimpleQuestArtSetup art in Setups)
      {
        if (!art.Icon)
          new DebugLogger().LogError("Icon in " + nameof(SimpleQuestArtConfig) + " with ID " + art.Id + " is null");
      }
    }
  }
}