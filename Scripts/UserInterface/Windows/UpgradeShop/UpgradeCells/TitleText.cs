using TMPro;
using UnityEngine;

namespace UpgradeCells
{
  public class TitleText : MonoBehaviour
  {
    public UpgradeCell UpgradeCell;
    public TextMeshProUGUI Title;
    
    private void Start()
    {
      Title.text = UpgradeCell.UpgradeArtSetup.Title;
    }
  }
}