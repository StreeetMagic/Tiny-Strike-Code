using System;
using System.Collections.Generic;
using Core.Characters.Players;
using Core.Weapons;
using Loggers;
using Meta.Currencies;
using UnityEngine;
using VisualEffects;
using Object = UnityEngine.Object;

namespace Core.PickUpTreasures
{
  public class PickUpTreasureCollector
  {
    private readonly WeaponStorage _playerWeaponStorage;
    private readonly PlayerProvider _playerProvider;
    private readonly VisualEffectProvider _visualEffectProvider;
    private readonly CurrencyStorage _currencyStorage;
    private readonly Dictionary<PickUpTreasureId, Action> _treasureActions;

    public PickUpTreasureCollector(WeaponStorage playerWeaponStorage, PlayerProvider playerProvider,
      VisualEffectProvider visualEffectProvider, CurrencyStorage currencyStorage)
    {
      _playerWeaponStorage = playerWeaponStorage;
      _playerProvider = playerProvider;
      _visualEffectProvider = visualEffectProvider;
      _currencyStorage = currencyStorage;

      _treasureActions = new Dictionary<PickUpTreasureId, Action>
      {
        { PickUpTreasureId.Knife, () => AddWeapon(WeaponId.Knife) },
        { PickUpTreasureId.Xm1014, () => AddWeapon(WeaponId.Xm1014) },
        { PickUpTreasureId.Ak47, () => AddWeapon(WeaponId.Ak47) },
        { PickUpTreasureId.DesertEagle, () => AddWeapon(WeaponId.DesertEagle) },
        { PickUpTreasureId.MedKit, HealPlayer },
        //  { PickUpTreasureId.GoldenChest, () => AddCurrency(CurrencyId.Chest, 1) },
        { PickUpTreasureId.GoldKey, () => AddCurrency(CurrencyId.Key, 1) },
        { PickUpTreasureId.MysteryBox, () => new DebugLogger().LogError("Игрок подобрал pickUpTreasure " + PickUpTreasureId.MysteryBox + " - надо написать логику") }
      };
    }

    public event Action<PickUpTreasureId> PickUpTreasureCollected;

    public void Collect(PickUpTreasureId id)
    {
      if (id == PickUpTreasureId.Unknown)
      {
        new DebugLogger().LogError("У pickUpTresuareId = PickUpTresuareId.Uknown");
        return;
      }

      if (_treasureActions.TryGetValue(id, out Action action))
        action.Invoke();
      else
        throw new ArgumentOutOfRangeException(nameof(id), id, null);

      PickUpTreasureCollected?.Invoke(id);
    }

    private void AddWeapon(WeaponId weaponId)
    {
      _playerWeaponStorage.Add(weaponId);
      _playerProvider.Instance.WeaponIdProvider.CurrentId.Value = weaponId;
    }

    private void HealPlayer()
    {
      _playerProvider.Instance.Health.HealMax();
      ParticleSystem prefab = _visualEffectProvider.Get(VisualEffectId.Heal);
      Object.Instantiate(prefab, _playerProvider.Instance.transform, false);
    }

    private void AddCurrency(CurrencyId currencyId, int amount)
    {
      _currencyStorage.AddPickUpTreasureCurrency(currencyId, amount);
    }
  }
}