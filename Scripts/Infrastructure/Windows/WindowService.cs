using System;
using System.Collections.Generic;
using Composite;
using ConfigProviders;
using Core.PickUpTreasures;
using HeadsUpDisplays;
using Meta;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace Windows
{
  public class WindowService
  {
    private readonly HubZenjectFactory _factory;
    private readonly HeadsUpDisplayProvider _headsUpDisplayProvider;
    private readonly CompositeQuestStorage _compositeQuestStorage;
    private readonly SimpleQuestStorage _simpleQuestStorage;
    private readonly ArtConfigProvider _artConfigProvider;

    private readonly Dictionary<WindowId, Window> _windows = new();
    private readonly Dictionary<SimpleQuestId, SimpleQuestWindow> _simpleQuests = new();
    private readonly Dictionary<CompositeQuestId, CompositeQuestWindow> _compositeQuests = new();
    private readonly Dictionary<PickUpTreasureId, PickUpWindow> _pickUpTreasures = new();

    public WindowService(
      HubZenjectFactory factory,
      HeadsUpDisplayProvider headsUpDisplayProvider,
      CompositeQuestStorage storage,
      ArtConfigProvider artConfigProvider,
      SimpleQuestStorage simpleQuestStorage)
    {
      _factory = factory;
      _headsUpDisplayProvider = headsUpDisplayProvider;
      _compositeQuestStorage = storage;
      _artConfigProvider = artConfigProvider;
      _simpleQuestStorage = simpleQuestStorage;
    }

    public Window ActiveWindow { get; private set; }

    public void WarmUp()
    {
      InitializeWindows();
      InitializeCompositeQuestWindows();
      InitializeSimpleQuestWindows();
      CreatePickUpWindow(PickUpTreasureId.GoldKey);
    }

    public Window Open(WindowId windowId)
    {
      Validate(windowId);
      //CloseCurrentWindow();

      Window window = _windows[windowId];
      ActivateWindow(window);

      ActiveWindow = window;
      return window;
    }

    public PickUpWindow Open(WindowId windowId, PickUpTreasureId pickUpTreasureId)
    {
     // CloseCurrentWindow();

      PickUpWindow pickUpWindow = _pickUpTreasures[pickUpTreasureId];
      ActivateWindow(pickUpWindow);

      PickUpWindowId pickUpWindowId = _artConfigProvider.PickUpTreasures[pickUpTreasureId].PickUpWindowId;

      pickUpWindow.Init(
        _artConfigProvider.PickUpWindows[pickUpWindowId].WindowBackground,
        _artConfigProvider.PickUpWindows[pickUpWindowId].TitleSprite,
        _artConfigProvider.PickUpTreasures[pickUpTreasureId].ItemSlot,
        _artConfigProvider.PickUpTreasures[pickUpTreasureId].Name,
        _artConfigProvider.PickUpTreasures[pickUpTreasureId].Description,
        _artConfigProvider.PickUpTreasures[pickUpTreasureId].ContinueButtonText,
        _artConfigProvider.PickUpTreasures[pickUpTreasureId].Icon);

      ActiveWindow = pickUpWindow;
      return pickUpWindow;
    }

    public CompositeQuestWindow Open(CompositeQuestId compositeQuestId)
    {
     // CloseCurrentWindow();

      var compositeQuestWindow = _compositeQuests[compositeQuestId];
      ActivateWindow(compositeQuestWindow);

      ActiveWindow = compositeQuestWindow;
      return compositeQuestWindow;
    }

    public SimpleQuestWindow Open(SimpleQuestId questId)
    {
     // CloseCurrentWindow();

      var simpleQuest = _simpleQuestStorage.Get(questId);
      if (simpleQuest.State.Value == QuestState.UnActivated)
        simpleQuest.State.Value = QuestState.Activated;

      var simpleQuestWindow = _simpleQuests[questId];
      ActivateWindow(simpleQuestWindow);

      ActiveWindow = simpleQuestWindow;
      return simpleQuestWindow;
    }

    public void CloseCurrentWindow()
    {
      ActiveWindow?.Close();
      ActiveWindow = null;
    }

    private void Validate(WindowId windowId)
    {
      if (windowId == WindowId.CompositeQuest ||
          windowId == WindowId.PickUp ||
          windowId == WindowId.SimpleQuest)
      {
        throw new Exception("Use a different method to create these windows.");
      }

      if (windowId == WindowId.Unknown)
      {
        throw new Exception("Unknown window ID provided to the window service.");
      }
    }

    private void InitializeWindows()
    {
      var windowIds = new[]
      {
        WindowId.UpgradeShop,
        WindowId.Debug,
        WindowId.Settings,
        WindowId.Cheats,
        WindowId.DeadScreenWindow,
        WindowId.FullChestWindow,
        WindowId.FullOpenChestWindow,
        WindowId.LevelUpWindow,
        WindowId.FullRewardChestOpenWindow
      };

      foreach (WindowId windowId in windowIds)
        CreateWindow(windowId);
    }

    private void InitializeCompositeQuestWindows()
    {
      var compositeQuestIds = new[]
      {
        CompositeQuestId.KillTerGun,
        CompositeQuestId.KillTerSniper,
        CompositeQuestId.KillTerGrenade,
        CompositeQuestId.KillTerAk47,
        CompositeQuestId.KillHens,
        CompositeQuestId.KillTerShotgun,
        CompositeQuestId.KillTerTutorialBoss
      };

      foreach (CompositeQuestId questId in compositeQuestIds)
        CreateCompositeQuestWindow(questId);
    }

    private void InitializeSimpleQuestWindows()
    {
      var simpleQuestIds = new[]
      {
        SimpleQuestId.BombDefuse,
        SimpleQuestId.HostagesRescue
      };

      foreach (SimpleQuestId questId in simpleQuestIds)
        CreateSimpleQuestWindow(questId);
    }

    private void ActivateWindow(Window window)
    {
      window.gameObject.SetActive(true);
      window.Initialize();
      window.transform.SetParent(HudTransform(), false);
      window.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private Transform HudTransform() =>
      _headsUpDisplayProvider.HeadsUpDisplay.GetComponentInChildren<Canvas>().transform;

    private void CreateWindow(WindowId windowId)
    {
      var window = _factory.InstantiatePrefabForComponent(_artConfigProvider.Windows[windowId].Prefab);
      InitializeWindow(window);
      _windows.Add(windowId, window);
    }

    private void CreateCompositeQuestWindow(CompositeQuestId compositeQuestId)
    {
      CompositeQuestWindow prefab = _artConfigProvider.Windows[WindowId.CompositeQuest].Prefab as CompositeQuestWindow;
      CompositeQuest quest = _compositeQuestStorage.Get(compositeQuestId);

      CompositeQuestWindow compositeQuestWindow = _factory.InstantiatePrefabForComponent(prefab, new List<object> { quest });
      InitializeWindow(compositeQuestWindow);
      _compositeQuests.Add(compositeQuestId, compositeQuestWindow);
    }

    private void CreateSimpleQuestWindow(SimpleQuestId simpleQuestId)
    {
      SimpleQuestWindow prefab = _artConfigProvider.Windows[WindowId.SimpleQuest].Prefab as SimpleQuestWindow;
      SimpleQuest quest = _simpleQuestStorage.Get(simpleQuestId);

      SimpleQuestWindow simpleQuestWindow = _factory.InstantiatePrefabForComponent(prefab, new List<object> { quest });
      InitializeWindow(simpleQuestWindow);
      _simpleQuests.Add(simpleQuestId, simpleQuestWindow);
    }

    private void CreatePickUpWindow(PickUpTreasureId pickUpTreasureId)
    {
      PickUpWindow prefab = _artConfigProvider.Windows[WindowId.PickUp].Prefab as PickUpWindow;
      PickUpWindow pickUpWindow = _factory.InstantiatePrefabForComponent(prefab);
      
      InitializeWindow(pickUpWindow);
      _pickUpTreasures.Add(pickUpTreasureId, pickUpWindow);
    }

    private void InitializeWindow(Window window)
    {
      window.transform.SetParent(HudTransform(), false);
      window.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
      window.gameObject.SetActive(false);
    }

    public void ClearActiveWindow()
    {
      if (!ActiveWindow)
        return;

      ActiveWindow.gameObject.SetActive(false);
    }
  }
}