using System;

namespace Windows.Configs
{
  [Serializable]
  public class WindowArtSetup : ArtSetup<WindowId>
  {
    public Window Prefab;
  }
}