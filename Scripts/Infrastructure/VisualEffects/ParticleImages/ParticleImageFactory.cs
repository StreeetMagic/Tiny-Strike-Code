using UnityAssetsTools.ParticleImage.Runtime;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace VisualEffects.ParticleImages
{
  public class ParticleImageFactory
  {
    private readonly HubZenjectFactory _zenjectFactory;
    private readonly ParticleImageProvider _particleImageProvider;

    public ParticleImageFactory(HubZenjectFactory zenjectFactory, ParticleImageProvider particleImageProvider)
    {
      _zenjectFactory = zenjectFactory;
      _particleImageProvider = particleImageProvider;
    }

    public ParticleImage Create(ParticleImageId visualEffectId, Vector3 position, Transform parent, Transform target = null, int amount = 0)
    {
      ParticleImage prefab = _particleImageProvider.Get(visualEffectId);
      GameObject moneyObject = _zenjectFactory.InstantiatePrefab(prefab.gameObject, position, Quaternion.identity, parent);

      moneyObject.transform.SetParent(parent);

      var partImage = moneyObject.GetComponent<ParticleImage>();

      partImage.main.attractorTarget = target;
      partImage.Play();

      return partImage;
    }
  }
}