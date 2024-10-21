using System;
using Windows;
using ConfigProviders;
using Meta;
using Meta.Sub;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SubQuestSlot : MonoBehaviour
{
  public Image CurrencyIcon;
  public TextMeshProUGUI CurrencyCount;
  public TextMeshProUGUI Description;
  public Button GoButton;
  public Button ClaimButton;
  public GameObject Dim;
  public Slider Slider;
  public TextMeshProUGUI SliderText;

  [Inject] private ArtConfigProvider _artConfigProvider;
  [Inject] private WindowService _windowService;

  [Inject] public SubQuest SubQuest { get; }

  private void Awake()
  {
    CurrencyIcon.sprite = _artConfigProvider.Currencies[SubQuest.Setup.Reward.Id].Sprite;
    CurrencyCount.text = SubQuest.Setup.Reward.Quantity.ToString();
    Description.text = _artConfigProvider.CompositeQuests[SubQuest.Id].Name + " " + SubQuest.Setup.Quantity + " times";
      
    GoButton.onClick.AddListener(() => _windowService.CloseCurrentWindow());

    ClaimButton.onClick.AddListener(() => { SubQuest.State.Value = QuestState.RewardTaken; });
  }

  private void OnEnable()
  {
    SubQuest.State.ValueChanged += OnStateChanged;
    OnStateChanged(SubQuest.State.Value);
  }

  private void OnDisable()
  {
    SubQuest.State.ValueChanged -= OnStateChanged;
  }

  private void Update()
  {
    int count = SubQuest.CompletedQuantity.Value;
    Slider.value = count / (float)SubQuest.Setup.Quantity;
    SliderText.text = count + "/" + SubQuest.Setup.Quantity;
  }

  private void OnStateChanged(QuestState state)
  {
    switch (state)
    {
      case QuestState.Activated:
        ActivateState();
        break;

      case QuestState.RewardReady:
        RewardReadyState();
        break;

      case QuestState.RewardTaken:
        RewardTakenState();
        break;

      case QuestState.UnActivated:
        break;
        
      case QuestState.Unknown:
      default:
        throw new ArgumentOutOfRangeException(nameof(state), state, null);
    }
  }

  private void ActivateState()
  {
    DisableAll();
    GoButton.gameObject.SetActive(true);
  }

  private void RewardReadyState()
  {
    DisableAll();
    ClaimButton.gameObject.SetActive(true);
  }

  private void RewardTakenState()
  {
    DisableAll();
    Dim.SetActive(true);
  }

  private void DisableAll()
  {
    GoButton.gameObject.SetActive(false);
    ClaimButton.gameObject.SetActive(false);
    Dim.SetActive(false);
  }
}