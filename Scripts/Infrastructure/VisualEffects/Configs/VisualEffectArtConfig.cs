using Loggers;
using UnityEngine;

namespace VisualEffects.Configs
{
  [CreateAssetMenu(menuName = "ArtConfigs/VisualEffect", fileName = "VisualEffect")]
  public class VisualEffectArtConfig : ArtConfig<VisualEffectArtSetup>
  {
    protected override void Validate()
    {
      foreach (VisualEffectArtSetup art in Setups)
      {
        foreach (ParticleSystem visualEffect in art.Prefabs)
        {
          if (!visualEffect)
            new DebugLogger().LogError("Prefab in " + nameof(VisualEffectArtConfig) + " with ID " + art.Id + " is null");
        }
      }
    }
  }
}