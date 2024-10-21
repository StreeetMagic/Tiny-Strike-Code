using Meta.Rewards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.Configs
{
  [CreateAssetMenu(menuName = "Configs/SimpleQuest", fileName = "SimpleQuest")]
  public class SimpleQuestConfig : Config<SimpleQuestId>
  {
    public CurrencyReward Reward;

    [ValidateInput(nameof(ValidateNonNegative))]
    public int Quantity;
    
    private bool ValidateNonNegative(int value)
    {
      return value >= 0;
    }
  }
}