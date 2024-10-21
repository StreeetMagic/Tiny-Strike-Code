using System;
using Meta.LevelUp;

namespace Meta.Expirience
{
  [Serializable]
  public class ExpirienceSetup
  {
    public int Level;
    public int Expierience;
    public LevelUpReward[] Rewards;
  }
}