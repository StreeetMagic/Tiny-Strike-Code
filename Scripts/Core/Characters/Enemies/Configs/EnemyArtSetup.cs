using System;
using AudioServices.Sounds;
using UnityEngine;

namespace Core.Characters.Enemies
{
  [Serializable]
  public class EnemyArtSetup : ArtSetup<EnemyId>
  {
    [Space]
    public EnemyVisualId VisualEffectsSetupId;
    
    public EnemyMeshModel EnemyMeshModelPrefab;
    
    [Tooltip("Звук при получении урона")]
    public SoundId DamageTakenSound = SoundId.HitMarker;
    
    public SoundId DieSound = SoundId.Gachi;
  }
}