using UnityEngine;

namespace AssetProviders
{
  public interface IAssetProvider
  {
    T GetArtScriptable<T>() where T : ScriptableObject;
    T GetScriptable<T>(string path) where T : ScriptableObject;
    T[] GetScriptables<T>(string path) where T : ScriptableObject;
  }
}