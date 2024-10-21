using Loggers;
using UnityEngine;

[CreateAssetMenu(menuName = "ArtConfigs/PickUpWindow", fileName = "PickUpWindow")]
public class PickUpWindowArtConfig : ArtConfig<PickUpWindowArtSetup>
{
  protected override void Validate()
  {
    foreach (PickUpWindowArtSetup art in Setups)
    {
      if (!art.TitleSprite)
        new DebugLogger().LogError("TitleSprite in " + nameof(PickUpWindowArtConfig) + " with Id" + art.Id + " is null");

      if (!art.WindowBackground)
        new DebugLogger().LogError("WindowBackground in " + nameof(PickUpWindowArtConfig) + " with Id" + art.Id + " is null");
    }
  }
}