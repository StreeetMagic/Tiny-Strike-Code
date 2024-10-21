using System;
using AudioServices.Sounds;
using UnityEngine;

namespace Core.Weapons.Configs
{
  [Serializable]
  public class WeaponArtSetup : ArtSetup<WeaponId>
  {
    public string Name;
    public string Description;
    public Sprite Icon;
    public SoundId AttackSound;
    public SoundId ReloadSound;
  }
}