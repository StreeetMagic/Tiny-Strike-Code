using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerIdleState : State
  {
    private readonly PlayerWeaponMagazineReloader _playerWeaponMagazineReloader;
    private readonly PlayerWeaponAmmo _playerWeaponAmmo;

    public PlayerIdleState(List<Transition> transitions, PlayerWeaponMagazineReloader playerWeaponMagazineReloader,
      PlayerWeaponAmmo playerWeaponAmmo) : base(transitions)
    {
      _playerWeaponMagazineReloader = playerWeaponMagazineReloader;
      _playerWeaponAmmo = playerWeaponAmmo;
    }

    public override void Enter()
    {
    }

    protected override void OnTick()
    {
      if (_playerWeaponMagazineReloader.IsActive)
      {
        _playerWeaponMagazineReloader.Tick();
      }
      else if (_playerWeaponAmmo.IsAboveHalf() == false)
      {
        _playerWeaponMagazineReloader.Activate();
        _playerWeaponMagazineReloader.Tick();
      }
    }

    public override void Exit()
    {
    }
  }
}