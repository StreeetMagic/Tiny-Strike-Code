using System;
using System.Collections.Generic;
using UnityAssetsTools.ParticleImage.Runtime;

namespace VisualEffects.ParticleImages
{
  [Serializable]
  public class ParticleImageArtSetup : ArtSetup<ParticleImageId>
  {
    public List<ParticleImage> Prefabs;
  }
}