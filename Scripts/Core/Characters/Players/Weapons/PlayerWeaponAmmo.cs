using System;
using System.Collections.Generic;
using ConfigProviders;
using Core.Weapons;
using Utilities;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerWeaponAmmo : IInitializable, IDisposable
  {
    private readonly WeaponStorage _weaponStorage;
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly PlayerWeaponIdProvider _playerWeaponIdProvider;

    private readonly Dictionary<WeaponId, ReactiveProperty<int>> _ammo = new();

    public PlayerWeaponAmmo(WeaponStorage weaponStorage, BalanceConfigProvider balanceConfigProvider,
      PlayerWeaponIdProvider playerWeaponIdProvider)
    {
      _weaponStorage = weaponStorage;
      _balanceConfigProvider = balanceConfigProvider;
      _playerWeaponIdProvider = playerWeaponIdProvider;
    }

    public ReactiveProperty<int> GetAmmo(WeaponId weaponTypeId)
    {
      if (_ammo.TryGetValue(weaponTypeId, out ReactiveProperty<int> ammo))
        return ammo;

      throw new Exception("Unknown weapon type id: " + weaponTypeId);
    }

    public void Initialize()
    {
      AddAmmos(_weaponStorage.Weapons.Value);

      _weaponStorage.Weapons.Changed += AddAmmos;
      _playerWeaponIdProvider.CurrentId.ValueChanged += OnCurrentIdChanged;
    }

    public void Dispose()
    {
      _weaponStorage.Weapons.Changed -= AddAmmos;
      _playerWeaponIdProvider.CurrentId.ValueChanged -= OnCurrentIdChanged;
    }

    public bool IsAboveHalf()
    {
      float value = _ammo[_playerWeaponIdProvider.CurrentId.Value].Value;
      float magazineCapacity = _balanceConfigProvider.Weapons[_playerWeaponIdProvider.CurrentId.Value].MagazineCapacity;
    
      float half = (float)Math.Floor(magazineCapacity / 2);
    
      return value > half;
    }

    public bool TryGetAmmo(WeaponId weaponTypeId, int count)
    {
      if (_ammo[weaponTypeId].Value < count)
        return false;

      _ammo[weaponTypeId].Value -= count;
      return true;
    }

    public void Reload(WeaponId weaponTypeId)
    {
      if (_ammo[weaponTypeId].Value == _balanceConfigProvider.Weapons[weaponTypeId].MagazineCapacity)
        return;

      _ammo[weaponTypeId].Value = _balanceConfigProvider.Weapons[weaponTypeId].MagazineCapacity;
    }

    private void OnCurrentIdChanged(WeaponId id)
    {
      Reload(id);
    }

    private void AddAmmos(IReadOnlyList<WeaponId> weapons)
    {
      foreach (WeaponId weapon in weapons)
      {
        if (_ammo.ContainsKey(weapon))
          continue;

        _ammo.Add(weapon, new ReactiveProperty<int>(_balanceConfigProvider.Weapons[weapon].MagazineCapacity));
      }
    }
  }
}