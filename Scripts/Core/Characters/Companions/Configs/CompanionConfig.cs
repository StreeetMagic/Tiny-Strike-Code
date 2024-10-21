using UnityEngine;

namespace Core.Characters.Companions.Configs
{
  [CreateAssetMenu(fileName = "Companion", menuName = "Configs/Companion")]
  public class CompanionConfig : Config<CompanionId>
  {
    [Range(0, 20)]
    [Tooltip("Скорость движения во время патрулирования")]
    public float MoveSpeed = 5f;
    
    [Range(0, 40)]
    [Tooltip("Скорострельность: выстрелов в секунду")]
    public int FireRate = 10;

    [Range(0, 100)]
    [Tooltip("Урон пули")] 
    public float BulletDamage = 5;
    
    [Range(10, 50)]
    [Tooltip("Скорость полета пули метров в сек")] 
    public float BulletSpeed = 25;

    [Range(1, 50)]
    [Tooltip("Емкость магазина")]
    public int MagazineCapacity = 10;
    
    [Range(0, 10)]
    [Tooltip("Время перезарядки")]
    public float MagazineReloadTime = 2f;
    
    [Range(1, 10)]
    [Tooltip("Пуль за один выстрел")]
    public int BulletsPerShot = 1;    
    
    [Range(0, 10)]
    [Tooltip("Угол разброса пуль")]
    public float BulletSpreadAngle = 1;    
    
    [Range(.1f, 1f)]
    [Tooltip("Длительность подъема оружия")]
    public float RaiseWeaponDuration = .2f;
    
    [Range(.1f, 1f)]
    [Tooltip("Длительность опускания оружия")]
    public float WeaponLoweringTime = .2f;
  }
}