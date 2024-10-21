using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerIdleToRiseWeaponTransition : Transition
  {
    private readonly PlayerTargetHolder _playerTargetHolder;
    private readonly PlayerWeaponMagazineReloader _playerWeaponMagazineReloader;
    private readonly PlayerWeaponAttacker _weaponAttacker;

    public PlayerIdleToRiseWeaponTransition(PlayerTargetHolder playerTargetHolder,
      PlayerWeaponMagazineReloader playerWeaponMagazineReloader, PlayerWeaponAttacker weaponAttacker)
    {
      _playerTargetHolder = playerTargetHolder;
      _playerWeaponMagazineReloader = playerWeaponMagazineReloader;
      _weaponAttacker = weaponAttacker;
    }

    public override void Tick()
    {
      if (_playerWeaponMagazineReloader.IsActive)
        return;

      if (!_weaponAttacker.WeaponWhiteList())
        return;

      if (_playerTargetHolder.HasTarget)
        Enter<PlayerRiseWeaponState>();
    }
  }
}