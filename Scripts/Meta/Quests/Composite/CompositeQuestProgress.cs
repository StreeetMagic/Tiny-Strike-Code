using System;
using System.Collections.Generic;
using Meta.Sub;

namespace Meta
{
  [Serializable]
  public class CompositeQuestProgress
  {
    public CompositeQuestId Id;
    public QuestState State;
    public List<SubQuestProgress> SubQuests;
    public bool RewardGained;

    public CompositeQuestProgress(CompositeQuestId id, QuestState state, List<SubQuestProgress> subQuests, bool rewardGained)
    {
      Id = id;
      State = state;
      SubQuests = subQuests;
      RewardGained = rewardGained;
    }
  }
}