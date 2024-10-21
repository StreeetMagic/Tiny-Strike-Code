using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

// ReSharper disable All

namespace Core.Characters.Players
{
  public class PlayerAnimatorController : MonoBehaviour
  {
    public const string Death1 = nameof(Death1);
    public const string Death2 = nameof(Death2);
    public const string Death3 = nameof(Death3);
    public const string Death4 = nameof(Death4);
    public const string Idle = nameof(Idle);

    private const string Run = "isRun";
    private const string PistolShoot = "PistolShoot";
    private const string RifleShoot = "RifleShoot";
    private const string ShotgunShoot = "ShotgunShoot";
    private const string GrenadeThrow = "GrenadeThrow";
    private const string IsReload = "IsReload";
    private const string WeaponUP = "WeaponUP";
    private const string StartShooting = "StartShooting";
    private const string IsInteract = "isInteract";

    public Animator Animator;

    public const string KnifeHit1 = "KnifeHit1";
    public const string KnifeHit2 = "KnifeHit2";
    public const string KnifeHit3 = "KnifeHit3";

    private static readonly int s_isRun = Animator.StringToHash(Run);
    private static readonly int s_idle = Animator.StringToHash(Idle);
    private static readonly int s_pistolShoot = Animator.StringToHash(PistolShoot);
    private static readonly int s_rifleShoot = Animator.StringToHash(RifleShoot);
    private static readonly int s_shotgunShoot = Animator.StringToHash(ShotgunShoot);
    private static readonly int s_granadeThrow = Animator.StringToHash(GrenadeThrow);
    private static readonly int s_reload = Animator.StringToHash(IsReload);
    private static readonly int s_interact = Animator.StringToHash(IsInteract);

    private static readonly int s_isShoot = Animator.StringToHash("isShoot");
    private static readonly int s_startShooting = Animator.StringToHash(StartShooting);
    private static readonly int s_weaponUp = Animator.StringToHash("WeaponUp");

    private readonly List<string> _deaths = new() { Death1, Death2, Death3, Death4 };

    public event Action KnifeHit;

    public void PlayRunAnimation()
    {
      Animator.speed = 1;
      Animator.SetBool(s_isRun, true);
    }

    public void Stop()
    {
      Animator.SetBool(s_isRun, false);
    }

    [Button]
    public void PlayRandomKnifeHitAnimation(float duration)
    {
      Animator.speed = 1;
      string[] animations = new string[] { KnifeHit1, KnifeHit2, KnifeHit3 };
      int randomIndex = Random.Range(0, animations.Length);
      string selectedAnimation = animations[randomIndex];

      AnimationClip weaponUpClip =
        Animator
          .runtimeAnimatorController
          .animationClips
          .FirstOrDefault(clip => clip.name == selectedAnimation);

      float animationLength = weaponUpClip.length;
      float speed = animationLength / duration;
      Animator.speed = speed;
      Animator.SetTrigger(selectedAnimation);
    }

    public void PlayPistolShoot()
    {
      Animator.speed = 1;
      Animator.SetTrigger(s_pistolShoot);
    }

    public void PlayRifleShoot()
    {
      Animator.speed = 1;
      Animator.SetTrigger(s_rifleShoot);
    }

    public void PlayShotgunShoot()
    {
      Animator.speed = 1;
      Animator.SetTrigger(s_shotgunShoot);
    }

    public void PlayGrenadeThrow() => Animator.SetTrigger(s_granadeThrow);

    public void PlayReload()
    {
      Animator.speed = 1;
      Animator.SetBool(s_reload, true);
    }

    public void StopReload() => Animator.SetBool(s_reload, false);

    public void PlayDeathAnimation()
    {
      Animator.SetTrigger(_deaths[Random.Range(0, _deaths.Count)]);
    }

    public void StopShoot()
    {
      Animator.SetBool(s_isShoot, false);
    }

    [Button]
    public void RaiseWeapon(float duration)
    {
      Animator.speed = 1;

      AnimationClip weaponUpClip =
        Animator
          .runtimeAnimatorController
          .animationClips
          .FirstOrDefault(clip => clip.name == WeaponUP);

      float animationLength = weaponUpClip.length;
      float speed = animationLength / duration;

      Animator.speed = speed;
      Animator.SetTrigger(s_weaponUp);
    }

    public void OnStateShooting()
    {
      Animator.SetBool(s_startShooting, true);
    }

    public void OffStateShooting()
    {
      SetWeaponDown();
      Animator.SetBool(s_startShooting, false);
    }

    public void OnHit()
    {
      KnifeHit?.Invoke();
    }

    private void ReloadFinished()
    {
    }

    private void GrenadeThrew()
    {
    }

    private void SetWeaponUp()
    {
      Animator.SetBool(s_weaponUp, true);
    }

    private void SetWeaponDown()
    {
      Animator.SetBool(s_weaponUp, false);
    }

    public void PLayIdle()
    {
      Animator.SetBool(s_startShooting, false);
      Animator.SetTrigger(s_idle);
    }

    public void PlayDefuseBomb()
    {
      print("Начинаем дефузинг");
      Animator.SetBool(s_interact, true);
    }
    
    public void StopDefuseBomb()
    {
      Animator.SetBool(s_interact, false);
    }
  }
}