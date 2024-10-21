using System.Collections.Generic;
using Prefabs;
using UnityEngine;

namespace ZenjectFactories
{
  public interface IZenjectFactory
  {
    T Instantiate<T>();
    T Instantiate<T>(params object[] args);
   
    GameObject InstantiatePrefab(GameObject gameObject);
    GameObject InstantiatePrefab(GameObject gameObject, Transform parent);
    GameObject InstantiatePrefab(GameObject gameObject, Vector3 position, Quaternion quaternion, Transform parent);
    
    T InstantiatePrefabForComponent<T>(T behaviour) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(T behaviour, Transform parent) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(T behaviour, Vector3 position, Transform parent) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(T behaviour, Vector3 position, Quaternion quaternion, Transform parent) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(PrefabId prefabId) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(PrefabId prefabId, IEnumerable<object> extraArgs) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(PrefabId prefabId, Transform parent) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(PrefabId prefabId, Transform parent, IEnumerable<object> extraArgs) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(PrefabId prefabId, Vector3 position, Transform parent) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(PrefabId prefabId, Vector3 position, Transform parent, IEnumerable<object> extraArgs) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(PrefabId prefabId, Vector3 position, Quaternion quaternion, Transform parent) where T : MonoBehaviour;
    T InstantiatePrefabForComponent<T>(PrefabId prefabId, Vector3 position, Quaternion quaternion, Transform parent, IEnumerable<object> extraArgs) where T : MonoBehaviour;
  }
}