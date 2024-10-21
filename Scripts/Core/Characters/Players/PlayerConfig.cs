using System.Collections.Generic;
using Core.Weapons;
using Meta.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Characters.Players
{
  [CreateAssetMenu(fileName = "Player", menuName = "Configs/Player")]
  public class PlayerConfig : ScriptableObject
  {
    [Tooltip("Скорость поворота")]
    public float RotationSpeed;
    
    [Tooltip("Время на разминирование бомбы сек")]
    public float BombDefuseDuration = 5;

    [Tooltip("Радиус обезвреживания бомбы м")]
    public float BombDefuseRadius = 5f;
    
    [Tooltip("Радиус освобождения заложников м")]
    public float HostageReleaseRadius = 3f;
        
    [Tooltip("Время на освобождение заложника сек")]
    public float HostageResqueDuration = 2f;
            
    [Tooltip("Замедление скорости при переносе заложника. -0.1f это -10% скорости")]
    public float HostageMoveSlowMultiplier = -0.1f;
    
    [Tooltip("Начальные оружия. Первое оружие будет первым в списке")]
    public List<WeaponId> StartWeapons;
    
    [FormerlySerializedAs("Stats")] 
    [Tooltip("Базовые характеристики игрока")]
    public List<StatSetup> BaseAdditiveStats;
  }
}