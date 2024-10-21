using System;

namespace Popups.Configs
{
  [Serializable]
  public class PopupArtSetup : ArtSetup<PopupId>
  {
    public Popup Prefab;
  }
}