using System.Collections.Generic;
using Meta.Currencies;
using Meta.Sub;
using SaveLoadServices;
using Utilities;

namespace Meta
{
  public class CompositeQuest : Quest
  {
    public CompositeQuest(QuestState state, CompositeQuestConfig config, List<SubQuest> subQuests,
      bool rewardGained, CurrencyStorage currencyStorage, ISaveLoadService saveLoadService) : base(currencyStorage, saveLoadService)
    {
      Config = config;
      State = new ReactiveProperty<QuestState>(state);
      SubQuests = subQuests;
      RewardGained = rewardGained;

      foreach (SubQuest subQuest in SubQuests)
        subQuest.State.ValueChanged += OnSubQuestStateChanged;

      State.ValueChanged += OnStateValueChanged;
    }

    public CompositeQuestConfig Config { get; }
    public List<SubQuest> SubQuests { get; }
    public bool RewardGained { get; private set; }

    public int CompletedQuests()
    {
      int count = 0;

      foreach (SubQuest subQuest in SubQuests)
        if (subQuest.State.Value is QuestState.RewardReady or QuestState.RewardTaken)
          count++;

      return count;
    }

    private void OnSubQuestStateChanged(QuestState subQuestState)
    {
      int activatedCount = 0;
      int rewardedReadyCount = 0;

      foreach (SubQuest subQuest in SubQuests)
      {
        switch (subQuest.State.Value)
        {
          case QuestState.Activated:
            activatedCount++;
            break;

          case QuestState.RewardReady:
            rewardedReadyCount++;
            break;
        }
      }

      if (rewardedReadyCount == 0 && activatedCount > 0)
      {
        if (State.Value != QuestState.Activated)
          State.Value = QuestState.Activated;
      }
      else if (rewardedReadyCount > 0)
      {
        if (State.Value != QuestState.RewardReady)
          State.Value = QuestState.RewardReady;
      }
      else if (activatedCount == 0 && rewardedReadyCount == 0)
      {
        if (State.Value != QuestState.RewardTaken)
          State.Value = QuestState.RewardTaken;
      }
      else
      {
        throw new System.Exception("Invalid quest state");
      }
    }

    private void OnStateValueChanged(QuestState state)
    {
      if (state == QuestState.RewardTaken)
        GainReward();
    }

    private void GainReward()
    {
      if (RewardGained)
        return;

      RewardGained = true;
      CurrencyStorage.OnRewardGain(Config.Reward);
      SaveLoadService.SaveProgress(ToString());
    }
  }
}