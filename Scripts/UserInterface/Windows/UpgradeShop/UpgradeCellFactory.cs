using ConfigProviders;
using Meta.Stats;
using Prefabs;
using UnityEngine;
using UpgradeCells;
using ZenjectFactories.SceneContext;

public class UpgradeCellFactory
{
  private readonly BalanceConfigProvider _balanceConfigProvider;
  private readonly HubZenjectFactory _factory;
  private readonly ArtConfigProvider _artConfigProvider;

  public UpgradeCellFactory(BalanceConfigProvider balanceConfigProvider,
    HubZenjectFactory shopWindowFactory, ArtConfigProvider artConfigProvider)
  {
    _balanceConfigProvider = balanceConfigProvider;
    _factory = shopWindowFactory;
    _artConfigProvider = artConfigProvider;
  }

  public void Create(StatId id, Transform parent)
  {
    var cell = _factory.InstantiatePrefabForComponent<UpgradeCell>(PrefabId.UpgradeCell);

    cell.UpgradeArtSetup = _artConfigProvider.Upgrades[id];
    cell.UpgradeConfig = _balanceConfigProvider.Upgrades[id];
    cell.transform.SetParent(parent, false);

    cell.GetComponentInChildren<Icon>().SetIcon(_artConfigProvider.Upgrades[id].Icon);
  }
}