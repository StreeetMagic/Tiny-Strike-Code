using UnityEngine;
using ZenjectFactories.SceneContext;
using Object = UnityEngine.Object;

namespace VisualEffects
{
  public class VisualEffectFactory
  {
    private readonly VisualEffectProvider _visualEffectProvider;
    private readonly HubZenjectFactory _zenjectFactory;

    public VisualEffectFactory(HubZenjectFactory zenjectFactory, VisualEffectProvider visualEffectProvider)
    {
      _zenjectFactory = zenjectFactory;
      _visualEffectProvider = visualEffectProvider;
    }

    public GameObject CreateAndDestroy(VisualEffectId visualEffectId, Vector3 position, Quaternion rotation)
    {
      ParticleSystem prefab = _visualEffectProvider.Get(visualEffectId);
      GameObject instance = _zenjectFactory.InstantiatePrefab(prefab.gameObject, position, rotation, null);

      float duration = prefab.main.duration;

      Object.Destroy(instance, duration);
      
      return instance;
    }    
    
    public void CreateAndDestroy(VisualEffectId visualEffectId, Vector3 position, Quaternion rotation, float scale)
    {
      ParticleSystem prefab = _visualEffectProvider.Get(visualEffectId);
      GameObject instance = _zenjectFactory.InstantiatePrefab(prefab.gameObject, position, rotation, null);

      instance.transform.localScale = new Vector3(scale, scale, scale);

      float duration = prefab.main.duration;

      Object.Destroy(instance, duration);
    }

    public GameObject Create(VisualEffectId visualEffectId, Vector3 position, Transform parent, Transform target = null)
    {
      ParticleSystem prefab = _visualEffectProvider.Get(visualEffectId);
      return _zenjectFactory.InstantiatePrefab(prefab.gameObject, position, Quaternion.identity, parent);
    }
  }
}