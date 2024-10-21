using Meta.Upgrades;
using UnityEngine;
using Zenject;

namespace Buttons
{
  public class OpenUpgradeShopWindowButton : OpenWindowButton
  {
    public GameObject ArrowUp;

    [Inject] private UpgradeService _upgradeService;

    private void Update()
    {
      if (_upgradeService.HasAvailableUpgrades())
      {
        if (!ArrowUp.activeSelf)
          ArrowUp.SetActive(true);
      }
      else
      {
        if (ArrowUp.activeSelf)
          ArrowUp.SetActive(false);
      }
    }
  }
}