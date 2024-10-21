using System;
using AudioServices.Sounds;
using ItemSlots;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.PickUpTreasures.Configs
{
  [Serializable]
  public class PickUpTreasureArtSetup : ArtSetup<PickUpTreasureId>
  {
    public Sprite Icon;
    public PickUpTreasureView Prefab;
    public SoundId PickUpSound;

    [FoldoutGroup("PickUpWindow")] 
    public PickUpWindowId PickUpWindowId = PickUpWindowId.Black;
    
    [FoldoutGroup("PickUpWindow")] 
    public string Name = "Reward";

    [FoldoutGroup("PickUpWindow")] 
    public string Description = "Default weapon";
    
    [FoldoutGroup("PickUpWindow")]
    public string ContinueButtonText = "Continue";
    
    [FoldoutGroup("PickUpWindow")]
    public ItemSlot ItemSlot;
  }
}