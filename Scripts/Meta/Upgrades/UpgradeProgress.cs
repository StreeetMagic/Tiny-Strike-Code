using System;
using Meta.Stats;

namespace Meta.Upgrades
{
  [Serializable]
  public class UpgradeProgress
  {
    public StatId Id;
    public UpgradeSetup Setup;

    public UpgradeProgress(StatId id, UpgradeSetup setup)
    {
      Id = id;
      Setup = setup;
    }
  }
}