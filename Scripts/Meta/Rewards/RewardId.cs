using System;
using System.Drawing;

namespace Meta.Rewards
{
  public enum RewardId
  {
    Unknown = 0,
    BackpackCapacity = 10001,
    Key = 2,
    Chest = 3,
    Expirience = 5,
    Ak47 = 7,
  }

  public enum RewardSlotVisualEffectId
  {
    //их может быть несколько
    
    Unknown = 0,
    
    Test1 = 901,
    Test2 = 902,
    Test3 = 903
  }

  public enum FullRewardWindowBackgroudId
  {
    Unknown = 0,
    
    Blue = 1,
    Orange = 2
  }

  [Serializable]
  public class RewardSlotBackgroundColorSetup
  {
    public Color Main;
    public Color Light;
    public Color Glow;
    public Color Border;
  }
}