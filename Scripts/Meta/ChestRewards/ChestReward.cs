using System;

namespace Meta.ChestRewards
{
  [Serializable]
  public class ChestReward
  {
    public ChestRewardId Id;
    public int Count;
    
    public ChestReward(ChestRewardId id, int count)
    {
      Id = id;
      Count = count;
    }
  }
}