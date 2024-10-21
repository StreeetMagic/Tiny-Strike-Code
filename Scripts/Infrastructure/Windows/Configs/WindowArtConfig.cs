using Loggers;
using UnityEngine;

namespace Windows.Configs
{
  [CreateAssetMenu(fileName = "Window", menuName = "ArtConfigs/Window")]
  public class WindowArtConfig : ArtConfig<WindowArtSetup>
  {
    protected override void Validate()
    {
      foreach (WindowArtSetup artSetup in Setups)
      {
        if (artSetup.Prefab == null)
        {
           new DebugLogger().LogError("Prefab in " + nameof(WindowArtConfig) + " with ID " + artSetup.Id + " is null");
          return;
        }
      }
    }
  }
}