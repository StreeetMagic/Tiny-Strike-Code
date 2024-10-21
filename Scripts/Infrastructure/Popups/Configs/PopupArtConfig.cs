using Loggers;
using UnityEngine;

namespace Popups.Configs
{
  [CreateAssetMenu(fileName = "Popup", menuName = "ArtConfigs/Popup")]
  public class PopupArtConfig : ArtConfig<PopupArtSetup>
  {
    protected override void Validate()
    {
      foreach (PopupArtSetup artSetup in Setups)
      {
        if (artSetup.Prefab == null)
        {
          new DebugLogger().LogError("Prefab in " + nameof(PopupArtConfig) + " with ID " + artSetup.Id + " is null");
          return;
        }
      }
    }
  }
}