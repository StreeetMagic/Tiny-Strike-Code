using System.Collections.Generic;
using System.Linq;
using AssetProviders;
using Prefabs;
using UnityEngine;

namespace ConfigProviders
{
  public class DevConfigProvider
  {
    private readonly IAssetProvider _assetProvider;
    
    private Dictionary<PrefabId, GameObject> _prefabs;
    
    public DevConfigProvider(IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }
    
    public void LoadConfigs() =>
      _prefabs = 
        _assetProvider
          .GetArtScriptable<PrefabArtConfig>()
          .Prefabs
          .ToDictionary(prefabPathSetup => prefabPathSetup.Id, prefabPathSetup => prefabPathSetup.Prefab);

    public GameObject GetPrefab(PrefabId id) => 
      _prefabs[id];

    public T GetPrefabForComponent<T>(PrefabId id) where T : MonoBehaviour => 
      _prefabs[id].GetComponent<T>();
  }
}