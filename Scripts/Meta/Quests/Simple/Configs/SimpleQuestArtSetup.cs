using System;
using UnityEngine;

namespace Meta.Configs
{
  [Serializable]
  public class SimpleQuestArtSetup : ArtSetup<SimpleQuestId>
  {
    public string Name;
    public string Description = "No description";
    public Sprite Icon;
  }
}