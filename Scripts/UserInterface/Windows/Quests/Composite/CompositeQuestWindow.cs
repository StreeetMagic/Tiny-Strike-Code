using System.Collections.Generic;
using Windows;
using ConfigProviders;
using Meta;
using Meta.Sub;
using Prefabs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ZenjectFactories.SceneContext;

namespace Composite
{
  public class CompositeQuestWindow : Window
  {
    public Transform SubQuestsContainer;
    public Image QuestIcon;
    public Image RewardIcon;
    public TextMeshProUGUI RewardQuantity;
    public Slider AllQuestsSlider;
    public TextMeshProUGUI AllQuestsText;
    public Button ClaimAllButton;
    public TextMeshProUGUI Description;

    [Inject] private BalanceConfigProvider _balanceConfigProvider;
    [Inject] private HubZenjectFactory _gameLoopZenjectFactory;
    [Inject] private ArtConfigProvider _artConfigProvider;
    [Inject] private CompositeQuestStorage _storage;
    [Inject] private CompositeQuest _compositeQuest;

    private readonly List<SubQuestSlot> _subQuestSlots = new List<SubQuestSlot>();

    public CompositeQuest CompositeQuest => _compositeQuest;

    public override void Initialize()
    {
      CloseButton.gameObject.SetActive(true);
      
      Description.text = _artConfigProvider.CompositeQuests[_compositeQuest.Config.Id].Description;
      CreateSubQuestSlots();
      SortSubQuestSlots();

      QuestIcon.sprite = _artConfigProvider.CompositeQuests[_compositeQuest.Config.Id].Icon;
      RewardIcon.sprite = _artConfigProvider.Currencies[_balanceConfigProvider.CompositeQuests[_compositeQuest.Config.Id].Reward.Id].Sprite;
      RewardQuantity.text = _balanceConfigProvider.CompositeQuests[_compositeQuest.Config.Id].Reward.Quantity.ToString();

      if (_compositeQuest.State.Value == QuestState.UnActivated)
      {
        _compositeQuest.State.Value = QuestState.Activated;

        foreach (SubQuest subQuest in _compositeQuest.SubQuests)
        {
          if (subQuest.State.Value == QuestState.UnActivated)
            subQuest.State.Value = QuestState.Activated;
        }
      }
    }

    protected override void SubscribeUpdates()
    {
      ClaimAllButton.onClick.AddListener(() =>
      {
        foreach (var subQuest in _compositeQuest.SubQuests)
          if (subQuest.State.Value == QuestState.RewardReady)
            subQuest.State.Value = QuestState.RewardTaken;
      });
    }

    protected override void Cleanup()
    {
      foreach (SubQuestSlot slot in _subQuestSlots)
        Destroy(slot.gameObject);
      
      _subQuestSlots.Clear();
    }

    private void Update()
    {
      UpdateAllQuestsInfo();
      UpdateClaimAllButton();
      SortSubQuestSlots();
    }

    private void UpdateClaimAllButton()
    {
      int count = 0;

      foreach (SubQuest subQuest in _compositeQuest.SubQuests)
        if (subQuest.State.Value == QuestState.RewardReady)
          count++;

      ClaimAllButton.gameObject.SetActive(count > 0);
    }

    private void UpdateAllQuestsInfo()
    {
      List<SubQuest> subQuests = _compositeQuest.SubQuests;
      int completed = _compositeQuest.CompletedQuests();

      AllQuestsSlider.maxValue = subQuests.Count;
      AllQuestsSlider.value = completed;
      AllQuestsText.text = $"{completed}/{subQuests.Count}";
    }

    private void CreateSubQuestSlots()
    {
      for (var i = 0; i < _balanceConfigProvider.CompositeQuests[_compositeQuest.Config.Id].SubQuests.Count; i++)
      {
        _subQuestSlots.Add
        (
          _gameLoopZenjectFactory.InstantiatePrefabForComponent<SubQuestSlot>(PrefabId.SubQuestSlot, SubQuestsContainer, new List<object> { _compositeQuest.SubQuests[i] })
        );
      }
    }

    private void SortSubQuestSlots()
    {
      _subQuestSlots
        .Sort((slot1, slot2) =>
        {
          if (slot1.SubQuest.State.Value == QuestState.RewardTaken && slot2.SubQuest.State.Value == QuestState.RewardTaken)
            return 0;

          if (slot1.SubQuest.State.Value == QuestState.RewardTaken)
            return 1;

          if (slot2.SubQuest.State.Value == QuestState.RewardTaken)
            return -1;

          return 0;
        });

      foreach (SubQuestSlot slot in _subQuestSlots)
        slot.transform.SetAsLastSibling();
    }
  }
}