using Meta.Upgrades;
using Meta.Upgrades.Configs;
using UnityEngine;
using Zenject;

namespace UpgradeCells
{
  public class UpgradeCell : MonoBehaviour
  {
    public GameObject GreenButton;
    public GameObject AdButton;

    [Inject]
    private UpgradeService _upgradeService;

    [Inject]
    private AdvertismentService _advertismentService;

    public UpgradeConfig UpgradeConfig { get; set; }
    public UpgradeArtSetup UpgradeArtSetup { get; set; }

    private void Update()
    {
      if (_upgradeService.IsMax(UpgradeConfig.Id))
      {
        DisableGreenButton();
        DisableAdButton();
        return;
      }

      if (_upgradeService.CanBuyNextUpgrade(UpgradeConfig.Id))
      {
        EnableGreenButton();
        DisableAdButton();
      }
      else
      {
        DisableGreenButton();
        EnableAdButton();
      }
    }

    private void EnableAdButton()
    {
      if (!AdButton.activeSelf)
        AdButton.SetActive(true);
    }

    private void EnableGreenButton()
    {
      if (!GreenButton.activeSelf)
        GreenButton.SetActive(true);
    }

    private void DisableAdButton()
    {
      if (AdButton.activeSelf)
        AdButton.SetActive(false);
    }

    private void DisableGreenButton()
    {
      if (GreenButton.activeSelf)
        GreenButton.SetActive(false);
    }
  }
}