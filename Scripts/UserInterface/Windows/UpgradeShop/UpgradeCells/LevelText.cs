using Meta.Upgrades;
using TMPro;
using UnityEngine;
using Zenject;

namespace UpgradeCells
{
  public class LevelText : MonoBehaviour
  {
    public UpgradeCell UpgradeCell;
    public TextMeshProUGUI LevelTextUI;

    [Inject] private UpgradeService _upgradeService;

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
      int currentLevel = _upgradeService.CurrentLevel(Config.Id);
      int maxLevel = _upgradeService.MaxLevel(Config.Id);

      LevelTextUI.text = $"LEVEL {currentLevel}";

      if (currentLevel == maxLevel)
        LevelTextUI.text = "MAX";
    }
  }
}