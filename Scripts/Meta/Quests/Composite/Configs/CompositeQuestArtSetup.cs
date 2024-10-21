using System;
using UnityEngine;

namespace Meta
{
  [Serializable]
  public class CompositeQuestArtSetup : ArtSetup<CompositeQuestId>
  {
    public string Name;
    public string Description = "No description"; 
    public Sprite Icon;
  }
}