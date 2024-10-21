using UnityEngine;

namespace Prefabs
{
  [CreateAssetMenu(fileName = "Prefab", menuName = "ArtConfigs/Prefab")]
  public class PrefabArtConfig : ScriptableObject
  {
    public PrefabSetup[] Prefabs;
  }
}