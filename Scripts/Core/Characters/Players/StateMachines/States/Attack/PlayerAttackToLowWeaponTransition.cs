using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerAttackToLowWeaponTransition : Transition
  {
    private readonly PlayerTargetHolder _targetHolder;
    private readonly PlayerWeaponMagazineReloader _playerWeaponMagazineReloader;
    private readonly PlayerWeaponAttacker _playerWeaponAttacker;

    public PlayerAttackToLowWeaponTransition(PlayerTargetHolder targetHolder,
      PlayerWeaponMagazineReloader playerWeaponMagazineReloader, PlayerWeaponAttacker playerWeaponAttacker)
    {
      _targetHolder = targetHolder;
      _playerWeaponMagazineReloader = playerWeaponMagazineReloader;
      _playerWeaponAttacker = playerWeaponAttacker;
    }

    public override void Tick()
    {
      if (_playerWeaponMagazineReloader.IsActive)
      {
        Enter<PlayerLowWeaponState>();
        return;
      }

      if (!_playerWeaponAttacker.WeaponWhiteList())
      {
        Enter<PlayerLowWeaponState>();
        return;
      }

      if (_targetHolder.HasTarget == false)
      {
        Enter<PlayerLowWeaponState>();
      }
    }
  }
}