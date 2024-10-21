using System.Collections.Generic;
using Windows;
using ConfigProviders;
using HeadsUpDisplays;
using Meta.Stats;
using TimeServices;
using UnityEngine;
using Zenject;

public class UpgradeWindow : Window
{
  [SerializeField]
  private Transform _container;

  // ReSharper disable once CollectionNeverUpdated.Local
  private readonly List<GameObject> _otherStuff = new();

  [Inject]
  private BalanceConfigProvider _balanceConfigProvider;

  [Inject]
  private UpgradeCellFactory _upgradeCellFactory;

  [Inject]
  private HeadsUpDisplayProvider _headsUpDisplayProvider;

  [Inject]
  private TimeService _timeService;

  private void Start()
  {
    CreateUpgradeCells();
    CollectOtherStuff();
    DisableOtherStuff();
  }

  public override void Initialize()
  {
  }

  protected override void SubscribeUpdates()
  {
  }

  protected override void Cleanup()
  {
  }

  private void OnDisable()
  {
    EnableOtherStuff();
  }

  private void CollectOtherStuff()
  {
    //_otherStuff.Add(_headsUpDisplayProvider.FloatingJoystick.gameObject);
  }

  private void EnableOtherStuff() =>
    _otherStuff
      .ForEach(otherStuff => otherStuff.SetActive(true));

  private void DisableOtherStuff() =>
    _otherStuff
      .ForEach(otherStuff => otherStuff.SetActive(false));

  private void CreateUpgradeCells()
  {
    int upgradesCount = _balanceConfigProvider.Upgrades.Count;

    List<StatId> keys = new List<StatId>(_balanceConfigProvider.Upgrades.Keys);

    for (int i = 0; i < upgradesCount; i++)
    {
      _upgradeCellFactory.Create(keys[i], _container);
    }
  }
}