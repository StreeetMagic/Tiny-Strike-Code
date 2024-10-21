using System;

namespace Meta.Upgrades
{
  [Serializable]
  public class UpgradeSetup
  {
    public float Value;
    public int Cost;
    
    public UpgradeSetup(float value, int cost)
    {
      Value = value;
      Cost = cost;
    }
  }
}