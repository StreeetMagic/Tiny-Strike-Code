using System;
using System.Collections.Generic;
using PersistentProgresses;
using SaveLoadServices;
using Utilities;
using Zenject;

namespace Core.Weapons
{
  public class WeaponStorage : IProgressWriter, IInitializable, IDisposable
  {
    private readonly WeaponShop _weaponShop;

    public WeaponStorage(WeaponShop weaponShop)
    {
      _weaponShop = weaponShop;
    }

    public ReactiveList<WeaponId> Weapons { get; } = new();

    public void ReadProgress(ProjectProgress projectProgress)
    {
      Weapons.Value = projectProgress.PlayerWeapons;
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      projectProgress.PlayerWeapons = new List<WeaponId>(Weapons.Value);
    }

    public void Initialize()
    {
      Weapons.Changed += OnWeaponsChanged;
    }

    public void Dispose()
    {
      Weapons.Changed -= OnWeaponsChanged;
    }

    private void OnWeaponsChanged(List<WeaponId> obj)
    {
      _weaponShop.SetAllWeapons();
      _weaponShop.RemoveWeapons(Weapons.Value);
    }

    public void Add(WeaponId id)
    {
      if (Weapons.Contains(id))
        return;

      Weapons.Add(id);

      if (Weapons.Count > 1)
        if (Weapons.Contains(WeaponId.Unarmed))
          Weapons.Remove(WeaponId.Unarmed);
    }
  }
}