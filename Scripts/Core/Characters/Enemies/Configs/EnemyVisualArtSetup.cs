using System;
using AudioServices.Sounds;
using UnityEngine;
using VisualEffects;

namespace Core.Characters.Enemies
{
  [Serializable]
  public class EnemyVisualArtSetup : ArtSetup<EnemyVisualId>
  {
    [Space]
    public VisualEffectId MuzzleFlashId;
    public VisualEffectId Bullet;
    public VisualEffectId Impact;
    public VisualEffectId Panic;
    
    public SoundId AttackSound;
    public SoundId ReloadSound;
  }
}