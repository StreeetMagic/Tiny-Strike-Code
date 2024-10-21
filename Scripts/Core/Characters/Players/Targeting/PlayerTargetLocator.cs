using ConfigProviders;
using Core.Weapons;
using DevConfigs;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerTargetLocator : ITickable
  {
    private readonly PlayerTargetHolder _playerTargetHolder;
    private readonly Transform _transform;
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly PlayerWeaponIdProvider _playerWeaponIdProvider;

    private readonly Collider[] _colliders = new Collider[DevConfig.TargetOverlapColliders];
    private readonly ITargetTrigger[] _targets = new ITargetTrigger[DevConfig.TargetOverlapColliders];

    public PlayerTargetLocator(PlayerTargetHolder playerTargetHolder, Transform transform,
      BalanceConfigProvider balanceConfigProvider, PlayerWeaponIdProvider playerWeaponIdProvider)
    {
      _playerTargetHolder = playerTargetHolder;
      _transform = transform;
      _balanceConfigProvider = balanceConfigProvider;
      _playerWeaponIdProvider = playerWeaponIdProvider;
    }

    public void Tick()
    {
      if (_playerWeaponIdProvider.CurrentId.Value == WeaponId.Unarmed)
        return;

      int count = Physics.OverlapSphereNonAlloc(_transform.position, _balanceConfigProvider.Weapons[_playerWeaponIdProvider.CurrentId.Value].Range, _colliders);

      for (var i = 0; i < count; i++)
      {
        Collider collider1 = _colliders[i];

        if (collider1.gameObject.TryGetComponent(out ITargetTrigger targetTrigger) == false)
        {
          continue;
        }

        if (targetTrigger.Health.IsDead)
        {
          continue;
        }

        if (targetTrigger.IsTargetable == false)
        {
          continue;
        }

        _targets[i] = targetTrigger;
      }

      foreach (ITargetTrigger target in _targets)
      {
        if (target == null)
          continue;

        _playerTargetHolder.AddTargets(target);
      }

      for (var i = 0; i < count; i++)
        _targets[i] = null;
    }
  }
}