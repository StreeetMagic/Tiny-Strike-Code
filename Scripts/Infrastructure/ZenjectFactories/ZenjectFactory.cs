using System.Collections.Generic;
using ConfigProviders;
using Prefabs;
using UnityEngine;
using Zenject.Source.Main;

namespace ZenjectFactories
{
  public abstract class ZenjectFactory : IZenjectFactory
  {
    private readonly DevConfigProvider _devConfigProvider;
    private readonly IInstantiator _instantiator;

    public ZenjectFactory(IInstantiator instantiator, DevConfigProvider devConfigProvider)
    {
      _instantiator = instantiator;
      _devConfigProvider = devConfigProvider;
    }

    public T Instantiate<T>() =>
      _instantiator.Instantiate<T>();

    public T Instantiate<T>(params object[] args) =>
      _instantiator.Instantiate<T>(args);
    
    //***//

    public GameObject InstantiatePrefab(GameObject gameObject) =>
      _instantiator.InstantiatePrefab(gameObject);

    public GameObject InstantiatePrefab(GameObject gameObject, Transform parent) =>
      _instantiator.InstantiatePrefab(gameObject, parent);

    public GameObject InstantiatePrefab(GameObject gameObject, Vector3 position, Quaternion quaternion, Transform parent) =>
      _instantiator.InstantiatePrefab(gameObject, position, quaternion, parent);

    //**//
    
    public T InstantiatePrefabForComponent<T>(T behaviour) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(behaviour);
    
    public T InstantiatePrefabForComponent<T>(T behaviour, IEnumerable<object> extraArgs) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(behaviour, extraArgs);

    public T InstantiatePrefabForComponent<T>(T behaviour, Transform parent) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(behaviour, parent);

    public T InstantiatePrefabForComponent<T>(T behaviour, Vector3 position, Transform parent) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(behaviour, position, Quaternion.identity, parent);

    public T InstantiatePrefabForComponent<T>(T behaviour, Vector3 position, Quaternion quaternion, Transform parent) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(behaviour, position, quaternion, parent);

    //**//

    public T InstantiatePrefabForComponent<T>(PrefabId prefabId) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(_devConfigProvider.GetPrefab(prefabId));

    public T InstantiatePrefabForComponent<T>(PrefabId prefabId, IEnumerable<object> extraArgs) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(_devConfigProvider.GetPrefab(prefabId), extraArgs);

    public T InstantiatePrefabForComponent<T>(PrefabId prefabId, Transform parent) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(_devConfigProvider.GetPrefab(prefabId), parent);
    
    public T InstantiatePrefabForComponent<T>(PrefabId prefabId, Transform parent, IEnumerable<object> extraArgs) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(_devConfigProvider.GetPrefab(prefabId), parent, extraArgs);

    public T InstantiatePrefabForComponent<T>(PrefabId prefabId, Vector3 position, Transform parent) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(_devConfigProvider.GetPrefab(prefabId), position, Quaternion.identity, parent);
    
    public T InstantiatePrefabForComponent<T>(PrefabId prefabId, Vector3 position, Transform parent, IEnumerable<object> extraArgs) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(_devConfigProvider.GetPrefab(prefabId), position, Quaternion.identity, parent, extraArgs);

    public T InstantiatePrefabForComponent<T>(PrefabId prefabId, Vector3 position, Quaternion quaternion, Transform parent) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(_devConfigProvider.GetPrefab(prefabId), position, quaternion, parent);
    
    public T InstantiatePrefabForComponent<T>(PrefabId prefabId, Vector3 position, Quaternion quaternion, Transform parent, IEnumerable<object> extraArgs) where T : MonoBehaviour =>
      _instantiator.InstantiatePrefabForComponent<T>(_devConfigProvider.GetPrefab(prefabId), position, quaternion, parent, extraArgs);
  }
}