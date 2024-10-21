using UnityEngine;

namespace Core.Characters.Enemies
{
  [CreateAssetMenu(fileName = "EnemyVisualEffectsSetup", menuName = "ArtConfigs/EnemyVisualEffectsSetup")]
  public class EnemyVisualArtConfig : ArtConfig<EnemyVisualArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}