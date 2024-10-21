using System.Collections.Generic;
using Windows;
using ItemSlots;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WindowBackgrounds;
using Zenject;
using ZenjectFactories.SceneContext;

public class PickUpWindow : Window
{
  // ReSharper disable once InconsistentNaming
  public Transform _backgroundContainer;

// ReSharper disable once InconsistentNaming
  public Transform _itemSlotContainer;
  public Image TitleSprite;

  public TextMeshProUGUI Name;
  public TextMeshProUGUI Description;
  public TextMeshProUGUI ContinueButtonText;

  [Inject]
  private HubZenjectFactory _gameLoopZenjectFactory;

  private List<ItemSlot> _itemSlots = new();

  public void Init
  (
    WindowBackground windowBackground,
    Sprite titleSprite,
    ItemSlot itemSlot,
    string pickUpName,
    string description,
    string continueButtonText,
    Sprite itemSlotIcon
  )
  {
    _gameLoopZenjectFactory.InstantiatePrefabForComponent(windowBackground, _backgroundContainer);

    ItemSlot itemslot = _gameLoopZenjectFactory.InstantiatePrefabForComponent(itemSlot, _itemSlotContainer);
    itemslot.Init(itemSlotIcon);
    _itemSlots.Add(itemslot);

    TitleSprite.sprite = titleSprite;
    Name.text = pickUpName;
    Description.text = description;
    ContinueButtonText.text = continueButtonText;
  }

  public override void Initialize()
  {
  }

  protected override void SubscribeUpdates()
  {
  }

  protected override void Cleanup()
  {
    foreach (ItemSlot itemSlot in _itemSlots)
      Destroy(itemSlot.gameObject);

    _itemSlots.Clear();
  }
}