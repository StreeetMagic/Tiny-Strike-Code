using System.Linq;
using LevelDesign.Maps;
using UnityEngine;
using Zenject;

namespace Meta.Chests
{
  public class ChestManager : ITickable
  {
    private readonly MapProvider _mapProvider;

    private Chest[] _chests;

    public ChestManager(MapProvider mapProvider)
    {
      _mapProvider = mapProvider;
    }

    public void Start()
    {
      _chests = _mapProvider.Map.Chests.ToArray();

      foreach (Chest chest in _chests)
        chest.Disable();

      if (_chests.Length == 0)
        return;

      ActivateRandomDisabledChest();
    }

    public void Tick()
    {
      foreach (Chest chest in _chests)
      {
        if (chest.IsActive && chest.IsOpened)
        {
          chest.Disable();
          ActivateRandomDisabledChest(chest);
        }
      }
    }

    private void ActivateRandomDisabledChest()
    {
      Chest randomChest;

      Chest[] disabledChests =
        _chests
          .Where(c => !c.IsActive)
          .ToArray();

      if (disabledChests.Length == 0)
        return;

      randomChest = disabledChests[Random.Range(0, disabledChests.Length)];

      randomChest.gameObject.SetActive(true);
      randomChest.Enable();
    }

    private void ActivateRandomDisabledChest(Chest excludedChest)
    {
      Chest[] disabledChests = _chests
        .Where(c => !c.IsActive && c != excludedChest)
        .ToArray();

      if (disabledChests.Length == 0)
        return;

      Chest randomChest = disabledChests[Random.Range(0, disabledChests.Length)];
      randomChest.Enable();
    }
  }
}