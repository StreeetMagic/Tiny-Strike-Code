using System;
using Windows;
using ConfigProviders;
using Meta;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SimpleQuestWindow : Window
{
  public GameObject ActivatedSlot;
  public GameObject RewardReadySlot;
  public Image QuestIcon;
  public Image[] CurrencyIcons;
  public TextMeshProUGUI[] CurrencyTexts;
  public TextMeshProUGUI[] Descriptions;
  public TextMeshProUGUI[] SliderTexts;
  public TextMeshProUGUI Description;
  public Slider[] Sliders;
  public Button GoButton;
  public Button ClaimButton;
  public FlyRewardSpawner FlyRewardSpawner;

  [Inject] private ArtConfigProvider _artConfigProvider;
  [Inject] private SimpleQuest _simpleQuest;
  [Inject] private WindowService _windowService;

  public SimpleQuest SimpleQuest => _simpleQuest;

  public override void Initialize()
  {
    QuestIcon.sprite = _artConfigProvider.SimpleQuests[_simpleQuest.Config.Id].Icon;
    Description.text = _artConfigProvider.SimpleQuests[_simpleQuest.Config.Id].Description;

    foreach (Image image in CurrencyIcons)
      image.sprite = _artConfigProvider.Currencies[_simpleQuest.Config.Reward.Id].Sprite;

    foreach (TextMeshProUGUI text in CurrencyTexts)
      text.text = _simpleQuest.Config.Reward.Quantity.ToString();

    foreach (TextMeshProUGUI text in Descriptions)
      text.text = _artConfigProvider.SimpleQuests[_simpleQuest.Config.Id].Name;

    UpdateUI();
    CloseButton.gameObject.SetActive(true);
  }

  protected override void SubscribeUpdates()
  {
    GoButton.onClick.AddListener(DisableWindow);

    ClaimButton.onClick.AddListener(() =>
    {
      _simpleQuest.State.Value = QuestState.RewardTaken;
      _simpleQuest.GainReward();
      FlyRewardSpawner.SpawnFlyRewardParticles();
      DisableWindow();
    });

    _simpleQuest.State.ValueChanged += OnStateChanged;
    _simpleQuest.CompletedQuantity.ValueChanged += OnCompletedQuantityChanged;
  }

  protected override void Cleanup()
  {
    GoButton.onClick.RemoveListener(DisableWindow);

    ClaimButton.onClick.RemoveAllListeners();

    _simpleQuest.State.ValueChanged -= OnStateChanged;
    _simpleQuest.CompletedQuantity.ValueChanged -= OnCompletedQuantityChanged;
  }

  private void OnStateChanged(QuestState newState)
  {
    UpdateUI();
  }

  private void OnCompletedQuantityChanged(int newQuantity)
  {
    UpdateUI();
  }

  private void UpdateUI()
  {
    foreach (Slider slider in Sliders)
      slider.value = (float)_simpleQuest.CompletedQuantity.Value / _simpleQuest.Config.Quantity;

    foreach (TextMeshProUGUI text in SliderTexts)
      text.text = _simpleQuest.CompletedQuantity.Value + "/" + _simpleQuest.Config.Quantity;

    switch (_simpleQuest.State.Value)
    {
      case QuestState.UnActivated:
        UnActivated();
        break;

      case QuestState.Activated:
        Activated();
        break;

      case QuestState.RewardReady:
        RewardReady();
        break;

      case QuestState.RewardTaken:
        RewardTaken();
        break;

      case QuestState.Unknown:
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private void UnActivated()
  {
    DisableAll();
  }

  private void Activated()
  {
    DisableAll();
    ActivatedSlot.SetActive(true);
  }

  private void RewardReady()
  {
    DisableAll();
    RewardReadySlot.SetActive(true);
  }

  private void RewardTaken()
  {
    DisableAll();
    RewardReadySlot.SetActive(true);
    //ClaimButton.gameObject.SetActive(false);
  }

  private void DisableWindow()
  {
    WindowCloseAnimationController.CloseWindow();
    Invoke(nameof(CloseWindow), WindowCloseAnimationController.fadeOutDuration);
  }

  private void CloseWindow()
  {
    _windowService.CloseCurrentWindow();
  }

  private void DisableAll()
  {
    ActivatedSlot.SetActive(false);
    RewardReadySlot.SetActive(false);

    GoButton.gameObject.SetActive(true);
    ClaimButton.gameObject.SetActive(true);
  }
}