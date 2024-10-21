using System;
using UnityEngine;

namespace AssetProviders
{
  public class ResourceFolderAssetProvider : IAssetProvider
  {
    public T GetArtScriptable<T>() where T : ScriptableObject =>
      GetScriptable<T>(typeof(T).Name.Replace("ArtConfig", ""));

    public T GetScriptable<T>(string path) where T : ScriptableObject =>
      Resources.Load<T>(path) ?? throw new Exception("Asset not found: " + typeof(T).Name + " " + path);

    public T[] GetScriptables<T>(string path) where T : ScriptableObject =>
      Resources.LoadAll<T>(path) ?? throw new Exception("Assets not found: " + typeof(T).Name + " " + path);
  }
}