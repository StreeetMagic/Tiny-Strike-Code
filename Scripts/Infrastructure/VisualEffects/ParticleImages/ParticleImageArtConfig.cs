using Loggers;
using UnityAssetsTools.ParticleImage.Runtime;
using UnityEngine;

namespace VisualEffects.ParticleImages
{
  [CreateAssetMenu(fileName = "ParticleImage", menuName = "ArtConfigs/ParticleImage")]
  public class ParticleImageArtConfig : ArtConfig<ParticleImageArtSetup>
  {
    protected override void Validate()
    {
      foreach (ParticleImageArtSetup art in Setups)
      {
        foreach (ParticleImage particleImage in art.Prefabs)
        {
          if (!particleImage)
            new DebugLogger().LogError("Prefab in " + nameof(ParticleImageArtConfig) + " with ID " + art.Id + " is null");
        }
      }
    }
  }
}