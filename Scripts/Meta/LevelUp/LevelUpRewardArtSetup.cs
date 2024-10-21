using System;
using ItemSlots;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.LevelUp
{
  [Serializable]
  public class LevelUpRewardArtSetup : ArtSetup<LevelUpRewardId>
  {
    public ItemSlot2 ItemSlot;

    public Sprite Icon;

    public bool IsWeapon;

    [ShowIf(nameof(IsWeapon))]
    public string WeaponName;
  }
}