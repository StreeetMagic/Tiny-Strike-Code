using UnityEngine;

namespace Core.Weapons.Configs
{
  [CreateAssetMenu(menuName = "ArtConfigs/Weapon", fileName = "Weapon")]
  public class WeaponArtConfig : ArtConfig<WeaponArtSetup>
  {
    protected override void Validate()
    {
    }
  }
}