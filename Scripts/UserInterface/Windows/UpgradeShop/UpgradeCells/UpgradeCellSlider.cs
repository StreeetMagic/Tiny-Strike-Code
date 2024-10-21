using Meta.Upgrades;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UpgradeCells
{
  public class UpgradeCellSlider : MonoBehaviour
  {
    public UpgradeCell UpgradeCell;
    public Slider Slider;

    [Inject] private UpgradeService _upgradeService;

    private UpgradeConfig Config => UpgradeCell.UpgradeConfig;

    private void Start()
    {
      UpdateSlider();
      _upgradeService.Changed += UpdateSlider;
    }

    private void OnDestroy()
    {
      _upgradeService.Changed -= UpdateSlider;
    }

    private void UpdateSlider()
    {
      int currentLevel = _upgradeService.CurrentLevel(Config.Id);
      int maxLevel = _upgradeService.MaxLevel(Config.Id);

      Slider.value = currentLevel / (float)maxLevel;
    }
  }
}