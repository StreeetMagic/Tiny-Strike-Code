using System;

namespace HeadsUpDisplays
{
  [Serializable]
  public class HeadsUpDisplayProgress
  {
    public bool ShowUpgradeShopButton;

    public HeadsUpDisplayProgress(bool showUpgradeShopButton)
    {
      ShowUpgradeShopButton = showUpgradeShopButton;
    }
  }
}