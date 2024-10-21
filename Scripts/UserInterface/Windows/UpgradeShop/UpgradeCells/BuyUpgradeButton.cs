using Buttons;
using Meta.Currencies;
using Meta.Stats;
using Meta.Upgrades;
using Zenject;

namespace UpgradeCells
{
  public class BuyUpgradeButton : BaseButton
  {
    public UpgradeCell UpgradeCell;

    [Inject] private UpgradeService _upgradeService;
    [Inject] private CurrencyStorage _currencyStorage;

    private StatId Id => UpgradeCell.UpgradeConfig.Id;

    private void Start()
    {
      SetupButton();
    }

    private void SetupButton() =>
      Button.onClick.AddListener(OnClick);

    private void OnClick()
    {
      _upgradeService.BuyUpgrade(Id);
    }
  }
}