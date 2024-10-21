using System;
using Core.Weapons;

namespace Core.Characters.Players
{
  public class PlayerWeaponAttackAnimationController
  {
    private readonly PlayerAnimatorController _playerAnimator;

    public PlayerWeaponAttackAnimationController(PlayerAnimatorController playerAnimator)
    {
      _playerAnimator = playerAnimator;
    }

    public void Play(WeaponId id, float meleeAttackDuration)
    {
      switch (id)
      {
        case WeaponId.Unknown:
          throw new ArgumentOutOfRangeException(nameof(id), id, null);
        
        case WeaponId.Unarmed:
          break;

        case WeaponId.Knife:
          _playerAnimator.PlayRandomKnifeHitAnimation(meleeAttackDuration);
          break;

        case WeaponId.DesertEagle:
          _playerAnimator.PlayPistolShoot();
          break;

        case WeaponId.Famas:
        case WeaponId.Ak47:
          _playerAnimator.PlayRifleShoot();
          break;

        case WeaponId.Xm1014:
          _playerAnimator.PlayShotgunShoot();
          break;

        default:
          throw new ArgumentOutOfRangeException(nameof(id), id, null);
      }
    }
  }
}