using Sirenix.OdinInspector;
using UnityEngine;

namespace HeadsUpDisplays.BackpackBars
{
  public class BackPackBar : MonoBehaviour
  {
    public GameObject BackpackBarContainer;

    [Button]
    public void Show()
    {
      if (!BackpackBarContainer)
        return;

      if (BackpackBarContainer.activeSelf)
        return;

      BackpackBarContainer.SetActive(true);
    }

    [Button]
    public void Hide()
    {
      if (!BackpackBarContainer)
        return;

      if (!BackpackBarContainer.activeSelf)
        return;

      BackpackBarContainer.SetActive(false);
    }
  }
}