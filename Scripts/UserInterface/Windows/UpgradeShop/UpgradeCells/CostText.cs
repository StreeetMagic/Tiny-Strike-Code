using ConfigProviders;
using Meta.Upgrades;
using TMPro;
using UnityEngine;
using Zenject;

namespace UpgradeCells
{
  public class CostText : MonoBehaviour
  {
    public UpgradeCell UpgradeCell;
    public TextMeshProUGUI CostTextUI;

    [Inject] private UpgradeService _upgradeService;
    [Inject] private BalanceConfigProvider _balanceConfigProvider;

    private UpgradeConfig Config => UpgradeCell.UpgradeConfig;

    private void Start()
    {
      UpdateText();

      _upgradeService.Changed += UpdateText;
    }

    private void OnDestroy()
    {
      _upgradeService.Changed -= UpdateText;
    }

    private void UpdateText()
    {
      if (_upgradeService.IsMax(Config.Id))
        CostTextUI.text = "MAX";
      else
        PrintText();
    }

    private void PrintText()
    {
      CostTextUI.text = $"{_upgradeService.NextLevelCost(Config.Id)}";
    }
  }
}