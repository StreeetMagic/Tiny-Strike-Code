using Meta.Currencies;
using SaveLoadServices;
using Utilities;

namespace Meta.Sub
{
  public class SubQuest : Quest
  {
    public SubQuest(int completedQuantity, QuestState state,
      ISaveLoadService saveLoadService, CurrencyStorage currencyStorage, SubQuestSetup setup, CompositeQuestId id) : base(currencyStorage, saveLoadService)
    {
      CompletedQuantity = new ReactiveProperty<int>(completedQuantity);
      State = new ReactiveProperty<QuestState>(state);
      Setup = setup;
      Id = id;

      CompletedQuantity.ValueChanged += OnCompletedQuantityChanged;
      State.ValueChanged += OnStateChanged;
    }

    public ReactiveProperty<int> CompletedQuantity { get; }
    public SubQuestSetup Setup { get; }
    public CompositeQuestId Id { get; }

    private void OnCompletedQuantityChanged(int count)
    {
      SaveLoadService.SaveProgress(ToString());

      if (count >= Setup.Quantity && State.Value != QuestState.RewardReady && State.Value != QuestState.RewardTaken)
        State.Value = QuestState.RewardReady;
    }

    private void OnStateChanged(QuestState state)
    {
      if (state == QuestState.RewardTaken)
        CurrencyStorage.OnRewardGain(Setup.Reward);
    }
  }
}