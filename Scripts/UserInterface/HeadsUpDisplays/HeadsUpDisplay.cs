using System;
using System.Collections.Generic;
using Windows;
using Builds;
using ConfigProviders;
using Core.Characters.Players;
using Meta.BackpackStorages;
using Meta.Currencies;
using Meta.Expirience;
using PersistentProgresses;
using Projects;
using SaveLoadServices;
using Sirenix.OdinInspector;
using Tutorials;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays
{
  public class HeadsUpDisplay : MonoBehaviour, IProgressWriter
  {
    public Transform MoneyResourceIconContainer;
    public GameObject UpgradeShopButton;
    public GameObject BackPackFullText;
    public GameObject CheatButton;
    public GameObject DisableInterfaceButton;

    [SerializeField]
    private List<GameObject> _panels = new();

    [Inject]
    private CurrencyStorage _currencyStorage;

    [Inject]
    private TutorialProvider _tutorialProvider;

    [Inject]
    private ExpierienceStorage _expierienceStorage;

    [Inject]
    private WindowService _windowService;

    [Inject]
    private PlayerProvider _playerProvider;

    [Inject]
    private BackpackStorage _backpackStorage;

    [Inject]
    private BalanceConfigProvider _balanceConfigProvider;

    //private float _visibleTime = 3f;
    private float _timeLeft;

    private void Awake()
    {
      BuildTypeId buildType = FindObjectOfType<ProjectInitializer>().BuildType;

      switch (buildType)
      {
        case BuildTypeId.Playable:
        {
          CheatButton.SetActive(false);
          DisableInterfaceButton.SetActive(false);
          break;
        }

        case BuildTypeId.Creative:
        {
          CheatButton.SetActive(true);
          DisableInterfaceButton.SetActive(true);

          break;
        }

        case BuildTypeId.Unknown:
        default:
        {
          Debug.LogError("В конфиге Project не указан тип сборки");
          throw new ArgumentOutOfRangeException();
        }
      }
    }

    private void OnEnable()
    {
      _currencyStorage.LootDropedFromBackpack.ValueChanged += OnLootDropedFromBackpack;
      //  _currencyStorage.HasChestsOrKeys.ValueChanged += OnHasChestsOrKeys;
      _tutorialProvider.Instance.State.ValueChanged += OnTutorialStateChanged;
      _expierienceStorage.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
      _currencyStorage.LootDropedFromBackpack.ValueChanged -= OnLootDropedFromBackpack;
      //  _currencyStorage.HasChestsOrKeys.ValueChanged -= OnHasChestsOrKeys;
      _tutorialProvider.Instance.State.ValueChanged -= OnTutorialStateChanged;
      _expierienceStorage.LevelChanged -= OnLevelChanged;
    }

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;

      SwitchBackPackFullText();

      if (_timeLeft > 0)
        _timeLeft -= Time.deltaTime;
    }

    public void ReadProgress(ProjectProgress projectProgress)
    {
      UpgradeShopButton.SetActive(projectProgress.HeadsUpDisplayProgress.ShowUpgradeShopButton);
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      projectProgress.HeadsUpDisplayProgress.ShowUpgradeShopButton = UpgradeShopButton.activeSelf;
    }

    private void SwitchBackPackFullText()
    {
      bool isBackPackFull = _backpackStorage.IsFull();
      bool hasTargetInRange = _playerProvider.Instance.TargetHolder.HasTarget;

      if (isBackPackFull && hasTargetInRange)
      {
        if (!BackPackFullText.activeSelf)
        {
          BackPackFullText.SetActive(true);
        }
      }
      else
      {
        if (BackPackFullText.activeSelf)
        {
          BackPackFullText.SetActive(false);
        }
      }
    }

    private void OnLevelChanged(int value)
    {
      _windowService.Open(WindowId.LevelUpWindow);
    }

    private void OnTutorialStateChanged(TutorialState state)
    {
      if (state == TutorialState.BombDefused)
        EnableObjects();
    }

    private void OnLootDropedFromBackpack(int count)
    {
      if (count != 1)
        return;

      if (!UpgradeShopButton.activeSelf)
        UpgradeShopButton.SetActive(true);
    }

    [Button]
    private void EnableObjects()
    {
      foreach (GameObject panel in _panels)
        panel.SetActive(true);
    }
  }
}