using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Characters.Enemies
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        public const string Death1 = nameof(Death1);
        public const string Death2 = nameof(Death2);
        public const string Death3 = nameof(Death3);
        public const string Death4 = nameof(Death4);
        public const string IsRun = nameof(IsRun);
        public const string IsWalk = nameof(IsWalk);
        private const string GrenadeThrow = "GrenadeThrow";
        private const string Panic = "Panic";
        private const string Reload = "Reload";
        private const string WeaponUp = "WeaponUp";
        private const string StartShooting = "StartShooting";
        private const string Idle = "Idle";

        private static readonly int s_granadeThrow = Animator.StringToHash(GrenadeThrow);
        private static readonly int s_reload = Animator.StringToHash(Reload);
        private static readonly int s_startShooting = Animator.StringToHash(StartShooting);
        private static readonly int s_weaponUp = Animator.StringToHash(WeaponUp);
        private static readonly int s_idle = Animator.StringToHash(Idle);

        [Header("Knife hit animations")] public const string KnifeHit1 = "KnifeHit1";
        public const string KnifeHit2 = "KnifeHit2";
        public const string KnifeHit3 = "KnifeHit3";

        [Header("Shoot animations")] public const string Shoot = nameof(Shoot);
        public const string RifleShoot = nameof(RifleShoot);

        private readonly List<string> _deaths = new() { Death1, Death2, Death3, Death4 };

        public Animator Animator;

        private static readonly int s_shoot = Animator.StringToHash(Shoot);
        private static readonly int s_rifleShoot = Animator.StringToHash(RifleShoot);
        private static readonly int s_isWalk = Animator.StringToHash(IsWalk);
        private static readonly int s_isRun = Animator.StringToHash(IsRun);
        private static readonly int s_panic = Animator.StringToHash(Panic);

        public event Action KnifeHit;
        public event Action GrenadeThrown;

        public void PlayDeathAnimation()
        {
            Animator.SetTrigger(_deaths[Random.Range(0, _deaths.Count)]);
        }

        public void OnHit()
        {
            KnifeHit?.Invoke();
        }

        private AnimatorOverrideController _overrideController;

        [Button]
        public void PlayRandomKnifeHitAnimation(float duration)
        {
            string[] animations = new string[] { KnifeHit1, KnifeHit2, KnifeHit3 };
            int randomIndex = Random.Range(0, animations.Length);
            string selectedAnimation = animations[randomIndex];

            float animationLength = Animator.runtimeAnimatorController.animationClips
                .First(clip => clip.name == selectedAnimation).length;

            float speed = animationLength / duration;

            Animator.speed = speed;
            Animator.SetTrigger(selectedAnimation);
        }

        public void PlayGrenadeThrow(float duration)
        {
            Animator.speed = 1;

            AnimationClip weaponUpClip =
                Animator
                    .runtimeAnimatorController
                    .animationClips
                    .FirstOrDefault(clip => clip.name == GrenadeThrow);

            if (!weaponUpClip)
                throw new Exception("Grenade throw animation not found");

            float animationLength = weaponUpClip.length;
            float speed = animationLength / duration;

            Animator.speed = speed;
            Animator.SetTrigger(s_granadeThrow);
        }

        public void PlayIdle()
        {
            Animator.SetTrigger(s_idle);
        }
        
        public void PlayPanic(float configAlertDuration)
        {
            float animationLength = Animator.runtimeAnimatorController.animationClips
                .First(clip => clip.name == Panic).length;

            float speed = animationLength / configAlertDuration;

            Animator.speed = speed;
            Animator.SetTrigger(s_panic);
        }

        public void PlayRun()
        {
            Animator.speed = 1;
            Animator.SetBool(s_isWalk, false);
            Animator.SetBool(s_isRun, true);
        }

        public void PlayWalk()
        {
            Animator.speed = 1;
            Animator.SetBool(s_isRun, false);
            Animator.SetBool(s_isWalk, true);
        }

        public void StopRun()
        {
            Animator.SetBool(s_isRun, false);
        }

        public void StopWalk()
        {
            Animator.SetBool(s_isWalk, false);
        }

        public void PlayShootAnimation()
        {
            Animator.SetTrigger(s_shoot);
        }

        public void PlayRifleShootAnimation()
        {
            Animator.SetTrigger(s_rifleShoot);
        }

        public void PlayReload()
        {
            Animator.SetTrigger(s_reload);
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

        private void ReloadFinished()
        {
            //new DebugLogger().Log("ReloadFinished");
        }

        private void PlayPanicEffect()
        {
            //new DebugLogger().Log("PlayPanicEffect");
        }

        private void GrenadeThrew()
        {
            GrenadeThrown?.Invoke();
        }

        private void SetWeaponUp()
        {
            //new DebugLogger().Log("SetWeaponUp");
            Animator.SetBool(s_weaponUp, true);
        }

        private void SetWeaponDown()
        {
            //new DebugLogger().Log("SetWeaponDown");
            Animator.SetBool(s_weaponUp, false);
        }
    }
}