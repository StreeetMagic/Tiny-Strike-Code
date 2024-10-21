using System;
using AudioServices;
using AudioServices.Sounds;
using ConfigProviders;
using Core.Weapons;
using UnityEngine;

namespace Core.Characters.Players
{
  public class PlayerWeaponAttacker
  {
    private readonly BalanceConfigProvider _balanceConfigProvider;

    private readonly PlayerWeaponIdProvider _weaponIdProvider;
    private readonly PlayerTargetHolder _targetHolder;
    private readonly PlayerWeaponAttackAnimationController _weaponAttackAnimationController;
    private readonly PlayerWeaponShooter _weaponShooter;
    private readonly PlayerAnimatorController _animatorController;
    private readonly AudioService _audioService;
    private readonly PlayerRotator _rotator;

    private float _timeLeft;
    private float _burstPauseLeft;
    private int _burstShots;

    public PlayerWeaponAttacker(BalanceConfigProvider balanceConfigProvider, PlayerWeaponIdProvider playerWeaponIdProvider,
      PlayerTargetHolder playerTargetHolder, PlayerWeaponAttackAnimationController playerWeaponAttackAnimationController,
      PlayerWeaponShooter playerWeaponShooter, AudioService audioService, PlayerAnimatorController playerAnimatorController, PlayerRotator rotator)
    {
      _balanceConfigProvider = balanceConfigProvider;
      _weaponIdProvider = playerWeaponIdProvider;
      _targetHolder = playerTargetHolder;
      _weaponAttackAnimationController = playerWeaponAttackAnimationController;
      _weaponShooter = playerWeaponShooter;
      _audioService = audioService;
      _animatorController = playerAnimatorController;
      _rotator = rotator;
    }
    
    public bool IsAttacking { get; set; }

    public void Tick()
    {
      if (!_targetHolder.HasTarget)
        return;

      _rotator.RotateTowardsDirection(_targetHolder.LookDirectionToTarget());

      if (!CanAttack())
        return;

      _rotator.RotateImminately(_targetHolder.LookDirectionToTarget());
      Attack();
    }

    public void ResetValues()
    {
      _timeLeft = 0;
      _burstPauseLeft = WeaponConfig().TimeBetweenBursts;
      _burstShots = 0;
    }

    public bool WeaponWhiteList()
    {
      WeaponId currentWeaponId = _weaponIdProvider.CurrentId.Value;

      if (_targetHolder.CurrentTarget == null)
        return false;

      ITargetTrigger currentTarget = _targetHolder.CurrentTarget;

      if (currentTarget.WeaponWhiteList.Count == 0)
        return true;

      return currentTarget.WeaponWhiteList.Contains(currentWeaponId);
    }

    private void Attack()
    {
      switch (WeaponConfig().WeaponAttackTypeId)
      {
        case WeaponAttackTypeId.Unknown:
          throw new Exception("WeaponAttackTypeId.Unknown");

        case WeaponAttackTypeId.Burst:
          Burst();
          break;

        case WeaponAttackTypeId.Auto:
          _timeLeft = Cooldown();
          _weaponShooter.Shoot(WeaponConfig());
          break;

        case WeaponAttackTypeId.Melee:
          Strike();
          _audioService.Play(SoundId.KnifeHit);
          _timeLeft = Cooldown();
          break;

        case WeaponAttackTypeId.Throw:
          throw new NotImplementedException();

        default:
          throw new Exception("Unknown WeaponAttackTypeId");
      }

      PlayAttackAnimation();
    }

    private bool CanAttack()
    {
      if (_timeLeft > 0)
      {
        _timeLeft -= Time.deltaTime;
        return false;
      }
      else
      {
        return true;
      }
    }

    private void Burst()
    {
      if (_burstPauseLeft > 0)
      {
        _burstPauseLeft -= Time.deltaTime;
        return;
      }

      _weaponShooter.Shoot(WeaponConfig());
      _burstShots++;
      _timeLeft = Cooldown();

      if (_burstShots < WeaponConfig().ShotsPerBurst)
        return;

      _burstPauseLeft = WeaponConfig().TimeBetweenBursts;
      _burstShots = 0;
    }

    private void Strike()
    {
      _animatorController.KnifeHit += OnKnifeHit;
    }

    private void OnKnifeHit()
    {
      if (_targetHolder.CurrentTarget == null)
        return;

      _targetHolder.CurrentTarget.TakeDamage(WeaponConfig().Damage);

      _animatorController.KnifeHit -= OnKnifeHit;
    }

    private WeaponConfig WeaponConfig()
    {
      return _balanceConfigProvider.Weapons[(_weaponIdProvider.CurrentId.Value)];
    }

    private float Cooldown()
    {
      if (_weaponIdProvider.CurrentId.Value == WeaponId.Knife)
        return _balanceConfigProvider.Weapons[(WeaponId.Knife)].MeeleAttackDuration;

      float fireRate = 1 / WeaponConfig().FireRate;

      return fireRate;
    }

    private void PlayAttackAnimation()
    {
      _weaponAttackAnimationController.Play(WeaponConfig().WeaponTypeId, WeaponConfig().MeeleAttackDuration);
    }
  }
}