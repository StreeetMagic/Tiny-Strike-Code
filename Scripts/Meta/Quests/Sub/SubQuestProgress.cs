using System;

namespace Meta.Sub
{
  [Serializable]
  public class SubQuestProgress
  {
    public CompositeQuestId Id;
    public int CompletedQuantity;
    public QuestState State;

    public SubQuestProgress(CompositeQuestId id, int completedQuantity, QuestState state)
    {
      Id = id;
      CompletedQuantity = completedQuantity;
      State = state;
    }
  }
}