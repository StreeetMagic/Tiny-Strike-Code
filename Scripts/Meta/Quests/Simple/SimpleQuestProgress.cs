using System;

namespace Meta
{
  [Serializable]
  public class SimpleQuestProgress
  {
    public SimpleQuestId Id;
    public QuestState State;
    public int CompletedQuantity;

    public SimpleQuestProgress(SimpleQuestId id, QuestState state, int completedQuantity)
    {
      Id = id;
      State = state;
      CompletedQuantity = completedQuantity;
    }
  }
}