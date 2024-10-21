using ConfigProviders;
using Meta.Stats;
using Meta.Upgrades;
using TMPro;
using UnityEngine;
using Zenject;

namespace UpgradeCells
{
  public class DescriptionText : MonoBehaviour
  {
    public UpgradeCell UpgradeCell;
    public TextMeshProUGUI DescriptionTextUI;

    [Inject] private UpgradeService _upgradeService;
    [Inject] private BalanceConfigProvider _balanceConfigProvider;
    [Inject] private ArtConfigProvider _artConfigProvider;
    [Inject] private PlayerStatsProvider _playerStatsProvider;

    private UpgradeConfig Config => UpgradeCell.UpgradeConfig;
    private StatId Id => Config.Id;

    private float _initialValue;

    private void Start()
    {
      _initialValue = _playerStatsProvider.GetStat(Id);
      
      UpdateText();
      
      _upgradeService.Changed += UpdateText;
    }

    private void OnDestroy()
    {
      _upgradeService.Changed -= UpdateText;
    }

    private void UpdateText()
    {
      float currentValue = _playerStatsProvider.GetStat(Id);

      if (_upgradeService.IsMax(Id))
      {
        DescriptionTextUI.text = $"{currentValue}";
        return;
      }

      float nextLevelValue = _upgradeService.NextValue(Id);

      DescriptionTextUI.text = $"{currentValue} -> {_initialValue + nextLevelValue}";
    }
  }
}