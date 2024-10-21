using Meta.Currencies;
using Meta.Stats;
using Meta.Upgrades;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UpgradeCells
{
  public class UpgradeDim : MonoBehaviour
  {
    public UpgradeCell UpgradeCell;
    public Image Dim;

    [Inject] private UpgradeService _upgradeService;
    [Inject] private CurrencyStorage _currencyStorage;

    private StatId Id => UpgradeCell.UpgradeConfig.Id;

    private void Update()
    {
      if (_upgradeService.IsMax(Id))
      {
        Dim.enabled = false;
        return;
      }

      Dim.enabled = _currencyStorage.Get(CurrencyId.Money).Value < _upgradeService.GetNextUpgradeCost(Id);
    }
  }
}