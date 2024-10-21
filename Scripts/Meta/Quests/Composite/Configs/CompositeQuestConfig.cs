using System.Collections.Generic;
using Meta.Rewards;
using Meta.Sub;
using UnityEngine;

namespace Meta
{
  [CreateAssetMenu(menuName = "Configs/CompositeQuest", fileName = "CompositeQuest")]
  public class CompositeQuestConfig : Config<CompositeQuestId>
  {
    public CurrencyReward Reward;

    public List<SubQuestSetup> SubQuests;
  }
}