using System;
using ItemSlots;
using UnityEngine;

namespace Meta.ChestRewards
{
  [Serializable]
  public class ChestRewardArtSetup : ArtSetup<ChestRewardId>
  {
    public ItemSlot2 ItemSlot;

    public Sprite Icon;
  }
}