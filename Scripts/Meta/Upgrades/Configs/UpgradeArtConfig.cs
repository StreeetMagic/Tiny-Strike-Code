using Loggers;
using UnityEngine;

namespace Meta.Upgrades.Configs
{
  [CreateAssetMenu(menuName = "ArtConfigs/" + "Upgrade", fileName = "Upgrade")]
  public class UpgradeArtConfig : ArtConfig<UpgradeArtSetup>
  {
    protected override void Validate()
    {
      foreach (UpgradeArtSetup art in Setups)
      {
        if (!art.Icon)
          new DebugLogger().LogError("Icon in " + nameof(UpgradeArtConfig) + " with ID " + art.Id + " is null");
      }
    }
  }
}