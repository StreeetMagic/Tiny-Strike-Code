using Meta.Configs;
using Meta.Currencies;
using SaveLoadServices;
using Utilities;

namespace Meta
{
  public class SimpleQuest : Quest
  {
    public SimpleQuest(QuestState state, SimpleQuestConfig config, CurrencyStorage currencyStorage, int completedQuantity, ISaveLoadService saveLoadService) 
      : base(currencyStorage, saveLoadService)
    {
      Config = config;

      State = new ReactiveProperty<QuestState>(state);
      CompletedQuantity = new ReactiveProperty<int>(completedQuantity);

      CompletedQuantity.ValueChanged += OnCompletedQuantityChanged;
    }

    public SimpleQuestConfig Config { get; }
    public ReactiveProperty<int> CompletedQuantity { get; }

    public void GainReward()
    {
      CurrencyStorage.OnRewardGain(Config.Reward);
      SaveLoadService.SaveProgress(ToString());
    }

    private void OnCompletedQuantityChanged(int count)
    {
      if (count >= Config.Quantity)
        State.Value = QuestState.RewardReady;
    }
  }
}