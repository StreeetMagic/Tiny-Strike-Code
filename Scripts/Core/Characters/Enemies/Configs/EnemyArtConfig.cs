using UnityEngine;

namespace Core.Characters.Enemies
{
  [CreateAssetMenu(fileName = "Enemy", menuName = "ArtConfigs/Enemy")]
  public class EnemyArtConfig : ArtConfig<EnemyArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}