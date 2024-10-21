using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using Zenject;

namespace Core.Weapons
{
  public class WeaponShop : IInitializable, IDisposable
  {
    public ReactiveList<WeaponId> Weapons { get; } = new();

    public void SetAllWeapons()
    {
      Weapons.Value = Enum
        .GetValues(typeof(WeaponId))
        .Cast<WeaponId>()
        .Where(x => x != WeaponId.Unknown)
        .ToList();
    }

    public void RemoveWeapons(IEnumerable<WeaponId> weapons)
    {
      foreach (WeaponId weapon in weapons)
      {
        if (!Weapons.Value.Contains(weapon))
          continue;

        Weapons.Remove(weapon);
      }
    }

    public void Initialize()
    {
      Weapons.Changed += OnWeaponsChanged;
    }

    public void Dispose()
    {
      Weapons.Changed -= OnWeaponsChanged;
    }

    private void OnWeaponsChanged(List<WeaponId> weapons)
    {
    }
  }
}