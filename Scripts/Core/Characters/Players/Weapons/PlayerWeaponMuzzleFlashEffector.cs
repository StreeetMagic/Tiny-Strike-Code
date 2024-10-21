using System;
using Core.Weapons;
using UnityEngine;
using VisualEffects;

namespace Core.Characters.Players
{
  public class PlayerWeaponMuzzleFlashEffector
  {
    private readonly VisualEffectFactory _visualEffectFactory;

    public PlayerWeaponMuzzleFlashEffector(VisualEffectFactory visualEffectFactory)
    {
      _visualEffectFactory = visualEffectFactory;
    }

    public void Play(Transform parent, WeaponId weaponTypeId)
    {
      VisualEffectId id;

      switch (weaponTypeId)
      {
        case WeaponId.DesertEagle:
          id = VisualEffectId.MuzzleStandardYellow;
          break;

        case WeaponId.Famas:
        case WeaponId.Ak47:
          id = VisualEffectId.MuzzleSmallYellow;
          break;

        case WeaponId.Xm1014:
          id = VisualEffectId.MuzzleSmallYellow;
          break;

        case WeaponId.Unknown:
        case WeaponId.Unarmed:
        case WeaponId.Knife:
        default:
          throw new ArgumentOutOfRangeException(nameof(weaponTypeId), weaponTypeId, null);
      }

      _visualEffectFactory.CreateAndDestroy(id, parent.position, parent.rotation);
    }
  }
}