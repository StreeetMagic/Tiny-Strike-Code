using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Weapons
{
  [CreateAssetMenu(fileName = "Weapon", menuName = "Configs/Weapon")]
  public class WeaponConfig : Config<WeaponId>
  {
    private const string CommonBulletParams = nameof(CommonBulletParams);
    private const string BurstParams = nameof(BurstParams);
    
    private bool IsBulletWeapon => WeaponAttackTypeId is WeaponAttackTypeId.Burst or WeaponAttackTypeId.Auto;
    private bool IsFiringBurst => WeaponAttackTypeId == WeaponAttackTypeId.Burst; 
    
    [Tooltip("Тип оружия")]
    public WeaponId WeaponTypeId;
    
    [EnumToggleButtons]  
    [Tooltip("Тип атаки")]
    public WeaponAttackTypeId WeaponAttackTypeId;

    [Range(0, 1)]
    [HideIf(nameof(WeaponAttackTypeId), WeaponAttackTypeId.Unknown)]
    [Tooltip("Время на подготовку к выстрелу - поднятие оружия")]
    public float RaiseTime = .2f;

    [Range(.01f, 100)]
    [HideIf(nameof(WeaponAttackTypeId), WeaponAttackTypeId.Unknown)]
    [Tooltip("Урон")]
    public float Damage = 10;
    
    [Range(.1f, 30)]
    [Tooltip("Дальность атаки")]
    public float Range = 10;

    [FoldoutGroup(CommonBulletParams)]
    [ShowIf(nameof(IsBulletWeapon))]
    [Range(0, 10)]
    [Tooltip("Разлет пуль в градусах")]
    public float BulletSpreadAngle;

    [FoldoutGroup(CommonBulletParams)]
    [ShowIf(nameof(IsBulletWeapon))]
    [Range(10, 30)]
    [Tooltip("Скорость пули метров в сек")]
    public float BulletSpeed = 25f;

    [FoldoutGroup(CommonBulletParams)]
    [ShowIf(nameof(IsBulletWeapon))]
    [Range(1, 20)]
    [Tooltip("Количество пуль на один выстрел")]
    public int BulletsPerShot = 1;

    [FoldoutGroup(CommonBulletParams)]
    [ShowIf(nameof(IsBulletWeapon))]
    [Range(.1f, 20)]
    [Tooltip("Скорострельность выстрелов в секунду")]
    public float FireRate = 5f;

    [FoldoutGroup(CommonBulletParams)]
    [ShowIf(nameof(IsBulletWeapon))]
    [Range(1, 200)]
    [Tooltip("Емкость одного магазина")]
    public int MagazineCapacity = 30;

    [FoldoutGroup(CommonBulletParams)]
    [ShowIf(nameof(IsBulletWeapon))]
    [Range(0, 3)]
    [Tooltip("Длительность перезарядки одного магазина")]
    public float ReloadTime = 1.5f;

    [FoldoutGroup(BurstParams)]
    [Range(1, 5)]
    [ShowIf(nameof(IsFiringBurst))]
    [Tooltip("Количество выстрелов за бёрст")]
    public int ShotsPerBurst = 3;

    [FoldoutGroup(BurstParams)]
    [Range(0, 1)]
    [ShowIf(nameof(IsFiringBurst))]
    [Tooltip("Время между очередями у бёрст оружий")]
    public float TimeBetweenBursts = 0.3f;

    [ShowIf(nameof(WeaponAttackTypeId), WeaponAttackTypeId.Melee)]
    [Range(.1f, 2)]
    [Tooltip("Длительность милишной атаки")]
    public float MeeleAttackDuration = .7f;
  }
}