using Meta.BackpackStorages;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.BackpackBars
{
  public class BackPackBarSwitcher : MonoBehaviour
  {
    public GameObject BackpackBar;

    [Inject] private BackpackStorage _backpackStorage;

    private void Update()
    {
      BackpackBar.SetActive(!_backpackStorage.IsEmpty());
    }
  }
}