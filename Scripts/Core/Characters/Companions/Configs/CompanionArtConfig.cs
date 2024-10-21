using UnityEngine;

namespace Core.Characters.Companions.Configs
{
  [CreateAssetMenu(fileName = "Companion", menuName = "ArtConfigs/Companion")]
  public class CompanionArtConfig : ArtConfig<CompanionArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}