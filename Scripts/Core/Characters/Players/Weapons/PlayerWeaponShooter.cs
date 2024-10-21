using AudioServices;
using AudioServices.Sounds;
using ConfigProviders;
using Core.Projectiles.Scripts;
using Core.Weapons;
using UnityEngine;
using Utilities;

namespace Core.Characters.Players
{
  public class PlayerWeaponShooter
  {
    private readonly ProjectileFactory _projectileFactory;
    private readonly AudioService _audioService;
    private readonly PlayerWeaponAmmo _weaponAmmo;
    private readonly PlayerWeaponMagazineReloader _reloader;
    private readonly PlayerWeaponShootingPoint _shootingPoint;
    private readonly PlayerWeaponMuzzleFlashEffector _muzzleFlashEffector;
    private readonly PlayerWeaponIdProvider _weaponIdProvider;
    private readonly PlayerTargetHolder _targetHolder;
    private readonly ArtConfigProvider _artConfigProvider;

    public PlayerWeaponShooter(ProjectileFactory projectileFactory, AudioService audioService,
      PlayerWeaponAmmo playerWeaponAmmo, PlayerWeaponMagazineReloader reloader, PlayerWeaponShootingPoint shootingPoint,
      PlayerWeaponMuzzleFlashEffector muzzleFlashEffector, PlayerWeaponIdProvider playerWeaponIdProvider,
      PlayerTargetHolder playerTargetHolder, ArtConfigProvider artConfigProvider)
    {
      _projectileFactory = projectileFactory;
      _audioService = audioService;
      _weaponAmmo = playerWeaponAmmo;
      _reloader = reloader;
      _shootingPoint = shootingPoint;
      _muzzleFlashEffector = muzzleFlashEffector;
      _weaponIdProvider = playerWeaponIdProvider;
      _targetHolder = playerTargetHolder;
      _artConfigProvider = artConfigProvider;
    }

    public void Shoot(WeaponConfig weaponConfig)
    {
      if (_weaponAmmo.TryGetAmmo(weaponConfig.WeaponTypeId, 1) == false)
      {
        _reloader.Activate();
        return;
      }

      for (int i = 0; i < weaponConfig.BulletsPerShot; i++)
      {
        if (_targetHolder.CurrentTarget == null)
          return;

        Vector3 directionToTarget = _targetHolder.CurrentTarget.TargetPoint.position - _shootingPoint.Transform.position;

        _projectileFactory.CreatePlayerProjectile(_shootingPoint.Transform, directionToTarget.AddAngle(weaponConfig.BulletSpreadAngle), _weaponIdProvider.CurrentId.Value);
      }

      _muzzleFlashEffector.Play(_shootingPoint.Transform, _weaponIdProvider.CurrentId.Value);

      SoundId sound = _artConfigProvider.Weapons[_weaponIdProvider.CurrentId.Value].AttackSound;
      _audioService.Play(sound);
    }
  }
}