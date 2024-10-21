using System;

namespace Core.Characters.Companions.Configs
{
  [Serializable]
  public class CompanionArtSetup : ArtSetup<CompanionId>
  {
    public Companion Prefab;
  }
}