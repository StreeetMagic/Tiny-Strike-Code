using System.Collections.Generic;
using Core.Grenades;
using Meta.Loots;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Characters.Enemies
{
  [CreateAssetMenu(fileName = "Enemy", menuName = "Configs/Enemy")]
  public class EnemyConfig : Config<EnemyId>
  {
    private const string EnemyMovementValues = nameof(EnemyMovementValues);
    private const string EnemyMeleeValues = nameof(EnemyMeleeValues);

    [Tooltip("Бессмертие")]
    public bool IsImmortal;

    [Tooltip("Приоритет при выборе целей High - максимальный, Low - минимальный")]
    [EnumToggleButtons]
    public TargetPriority TargetPriority = TargetPriority.Medium;
    
    [Tooltip("Если выкл, то не атакуют игрока")]
    public bool IsAttacker = true;
    
    [Tooltip("Следит за игроком?")]
    public bool LookAtPlayer;
    
    [Tooltip("Есть анимация смерти?")]
    public bool HasDeathAnimation = true;

    [Tooltip("Показывать лут, который выпадет после смерти?")]
    public bool ShowLoot = true;

    [Tooltip("Время жизни трупа")] 
    [Range(.01f, 10)]
    public float CorpseRemoveDelay = 2f;

    [Range(1, 10000)]
    [HideIf(nameof(IsImmortal))]
    [Tooltip("Начальное здоровье")] 
    public float InitialHealth = 100f;

    [FormerlySerializedAs("HealthRegenerationRate")]
    [Range(0, 100)] 
    [HideIf(nameof(IsImmortal))]
    [Tooltip("Процент регенерации в секунду при возвращении на базу")]
    public float HealthRegenPercentPerSecond = 100;

    [Range(0, 10000)]
    [Tooltip("Награда за убийство")] 
    public int Expirience = 100;

    [Tooltip("Набор наград при убийстве")] 
    public List<LootDrop> LootDrops;

    [FoldoutGroup(EnemyMovementValues)]
    [Range(0, 20)]
    [Tooltip("Скорость движения во время патрулирования")]
    public float MoveSpeed = 5f;

    [FoldoutGroup(EnemyMovementValues)]
    [Range(0, 20)]
    [Tooltip("Скорость бега при преследовании игрока и возвращении на место")]
    public float RunSpeed = 10f;

    [FoldoutGroup(EnemyMovementValues)]
    [Range(0, 10)]
    [Tooltip("Время простоя в состоянии покоя")]
    public float IdleDuration = .5f;

    [FoldoutGroup(EnemyMovementValues)]
    [Range(0.01f, 50)]
    [Tooltip("Максимальное расстояние преследования игрока от центра спаунера")]
    public float PatrolingRadius = 20;

    [FoldoutGroup(EnemyMovementValues)]
    [Range(0.01f, 50)]
    [Tooltip("Радиус запроса помощи от противников из других спаунеров. На арене всегда максимальный")]
    public float AssistCallRadius = 10f;

    [Tooltip("Время ошеломления после получения урона перед агром")]
    [Range(.01f, 5)]
    public float AlertDuration = .2f;

    [Tooltip("Радиус агра если рядом пробежал игрок")]
    [Range(0.01f, 10)]
    public float AggroRadius = 3f;
    
    /************************/
    [Space]
    [FoldoutGroup(EnemyMeleeValues)]
    [Range(0, 10)]
    [Tooltip("Радиус милишной атаки")] 
    public float MeleeRange = 2;
    
    [FoldoutGroup(EnemyMeleeValues)]
    [Range(0, 10)]
    [Tooltip("Время милишной атаки")]
    public float MeeleAttackDuration = .2f; 
    
    [FoldoutGroup(EnemyMeleeValues)]
    [Range(0, 1000)]
    [Tooltip("Урон милишной атаки")]
    public float MeeleAttackDamage = 10;
    /************************/

    [Space]
    [Tooltip("Стреляющий")] 
    public bool IsShooter;

    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(0, 40)]
    [Tooltip("Радиус стрельбы")] 
    public float ShootRange = 10f;

    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(0, 40)]
    [Tooltip("Скорострельность: выстрелов в секунду")]
    public float FireRate = 10;

    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(0, 100)]
    [Tooltip("Урон пули")] 
    public float BulletDamage = 5;
    
    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(10, 50)]
    [Tooltip("Скорость полета пули метров в сек")] 
    public float BulletSpeed = 25;
    
    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(1, 50)]
    [Tooltip("Емкость магазина")]
    public int MagazineCapacity = 10;
    
    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(0, 10)]
    [Tooltip("Время перезарядки")]
    public float MagazineReloadTime = 2f;
    
    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(1, 10)]
    [Tooltip("Пуль за один выстрел")]
    public int BulletsPerShot = 1;    
    
    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(0, 10)]
    [Tooltip("Угол разброса пуль")]
    public float BulletSpreadAngle = 1;    
    
    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(.1f, 1f)]
    [Tooltip("Длительность подъема оружия")]
    public float WeaponRisingTime = .2f;
    
    [FoldoutGroup(nameof(IsShooter))]
    [ShowIf(nameof(IsShooter))]
    [Range(.1f, 1f)]
    [Tooltip("Длительность опускания оружия")]
    public float WeaponLoweringTime = .2f;

    /************************/

    [Space]
    [Tooltip("Способность бросать гранату")] 
    public bool IsGrenadeThrower = true;

    [FoldoutGroup(nameof(IsGrenadeThrower))]
    [ShowIf(nameof(IsGrenadeThrower))]
    [Tooltip("ID гранаты")]
    public GrenadeTypeId GrenadeTypeId = GrenadeTypeId.Frag;

    [FoldoutGroup(nameof(IsGrenadeThrower))]
    [ShowIf(nameof(IsGrenadeThrower))]
    [Range(0, 30)]
    [Tooltip("Кулдаун бросания гранаты в секундах")]
    public float GrenadeThrowCooldown = 10f;
    
    [FoldoutGroup(nameof(IsGrenadeThrower))]
    [ShowIf(nameof(IsGrenadeThrower))]
    [Range(.1f, 5f)]
    [Tooltip("Случайная задержка перед броском гранаты. От нуля и до текущего значения")]
    public float GrenadeThrowRandomDelay = 1f;
    
    [FoldoutGroup(nameof(IsGrenadeThrower))]
    [ShowIf(nameof(IsGrenadeThrower))]
    [Range(.1f, 3f)]
    [Tooltip("Длительность броска гранаты")]
    public float GrenadeThrowDuration = 1f;
    
    [FoldoutGroup(nameof(IsGrenadeThrower))]
    [ShowIf(nameof(IsGrenadeThrower))]
    [Range(.1f, 5f)]
    [Tooltip("Время, после которого в неподвижную цель полетит граната")]
    public float TargetStandsOnSamePositionTime = 1f;
    
    [FoldoutGroup(nameof(IsGrenadeThrower))]
    [ShowIf(nameof(IsGrenadeThrower))]
    [Tooltip("Неограниченное количество гранат")]
    public bool UnlimitedGrenadesCount;
    
    [FoldoutGroup(nameof(IsGrenadeThrower))]
    [ShowIf(nameof(IsGrenadeThrower))]
    [Range(1, 10)]
    [Tooltip("Доступно гранат")]
    public int MaxGrenadesCount = 3;

    [FoldoutGroup(nameof(IsGrenadeThrower))]
    [ShowIf(nameof(IsGrenadeThrower))]
    [Range(1, 10)]
    [Tooltip("Радиус броска гранат")]
    public float GrenadeThrowRange = 5f;

    [FoldoutGroup(nameof(HasPhases))]
    [Tooltip("Есть ли фазы врага")]
    public bool HasPhases;
    
    [FoldoutGroup(nameof(HasPhases))]
    [ShowIf(nameof(HasPhases))]
    [Tooltip("Граница фазы врага")]
    public float PhaseThreshold = 0.5f;
    
    [FoldoutGroup(nameof(HasPhases))]
    [ShowIf(nameof(HasPhases))]
    [Tooltip("Тип спаунящихся врагов")]
    public EnemyId EnemyTypeId = EnemyId.TerKnife;
    
    [FoldoutGroup(nameof(HasPhases))]
    [ShowIf(nameof(HasPhases))]
    [Tooltip("Количество спаунящихся врагов")]
    public int EnemySpawnCount = 2;
    
    [FoldoutGroup(nameof(HasPhases))]
    [ShowIf(nameof(HasPhases))]
    [Tooltip("Множитель скорострельности")]
    public float FireRateMultiplier = 2f;
    
    [FoldoutGroup(nameof(HasPhases))]
    [ShowIf(nameof(HasPhases))]
    [Tooltip("Множитель урона")]
    public float DamageMultiplier = 2f;
    
    [FoldoutGroup(nameof(HasPhases))]
    [ShowIf(nameof(HasPhases))]
    [Tooltip("Множитель длительности броска гранаты")]
    public float GrenadeThrowDurationMultiplier = .5f;
  }
}