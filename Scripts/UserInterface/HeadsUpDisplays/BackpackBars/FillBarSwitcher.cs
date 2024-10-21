using System.Collections.Generic;
using Meta.BackpackStorages;
using Meta.Loots;
using Meta.Stats;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.BackpackBars
{
  public class FillBarSwitcher : MonoBehaviour
  {
    public GameObject NotFullBar;
    public GameObject FullBar;
    public GameObject Text;

    [Inject] private PlayerStatsProvider _playerStatsProvider;
    [Inject] private BackpackStorage _backpackStorage;

    private void OnEnable()
    {
      Setup();

      _backpackStorage.LootDrops.Changed += OnLootDropsChanged;
    }

    private void OnLootDropsChanged(List<LootDrop> obj)
    {
      Setup();
    }

    private void Setup()
    {
      bool isFull = _backpackStorage.Volume() >= _playerStatsProvider.GetStat(StatId.BackpackCapacity);

      if (!FullBar || !NotFullBar)
        return;

      FullBar.SetActive(isFull);
      NotFullBar.SetActive(!isFull);
      Text.SetActive(!isFull);
    }
  }
}