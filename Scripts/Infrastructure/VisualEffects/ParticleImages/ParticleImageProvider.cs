using ConfigProviders;
using RandomServices;
using UnityAssetsTools.ParticleImage.Runtime;

namespace VisualEffects.ParticleImages
{
  public class ParticleImageProvider
  {
    private readonly ArtConfigProvider _artConfigProvider;
    private readonly RandomService _randomService;

    public ParticleImageProvider(ArtConfigProvider artConfigProvider, RandomService randomService)
    {
      _artConfigProvider = artConfigProvider;
      _randomService = randomService;
    }

    public ParticleImage Get(ParticleImageId id) =>
      _artConfigProvider
        .ParticleImages[id]
        .Prefabs[_randomService.GetRandomInt(_artConfigProvider.ParticleImages[id].Prefabs.Count - 1)];
  }
}