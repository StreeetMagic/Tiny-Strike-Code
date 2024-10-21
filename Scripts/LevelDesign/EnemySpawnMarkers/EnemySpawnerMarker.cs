using Core.Characters.Enemies;
using Core.PickUpTreasures;
using Core.Spawners.Enemies;
using Meta;
using UnityEngine;

namespace LevelDesign.EnemySpawnMarkers
{
  [SelectionBase]
  public class EnemySpawnerMarker : MonoBehaviour
  {
    [Tooltip("Тип врага")] 
    public EnemyId EnemyId;

    [Tooltip("Количество врагов одновременно")]
    public int Count = 1;

    [Tooltip("Время между респеуном врагов")]
    public int RespawnTime = 15;

    [Tooltip("Нужно ли респаунить")] 
    public bool Respawns = true;

    [Tooltip("Тип спаунера")] 
    public EnemySpawnerType SpawnerType = EnemySpawnerType.FromStart;

    [Tooltip("Спаунер начнет спаунить после зачистки указанного")]
    public EnemySpawnerMarker SpawnerToClear;

    [Tooltip("Симпл квест необходимый для активации")]
    public SimpleQuestId SimpleQuestToActivate;    
    
    [Tooltip("Симпл квест необходимый для активации")]
    public CompositeQuestId CompositeQuestToActivate;

    [Tooltip("Вкл - случайные маршруты патрулирования. Выкл - появление и патрулирование от первой до последней точки и так по кругу")]
    public bool RandomPatroling;
    
    [Tooltip("Если Unknow - дропа не будет")]
    public PickUpTreasureId PickUpTreasure;

    [Tooltip("Шанс дропа pickUpTreasure")] 
    [Range(0f, 1f)]
    public float PickUpTreasureDropChance = 1f;
    
    [Tooltip("Уничтожаем после дропа pickUpTreasure")] 
    public bool PickUpTreasureDestroyAfterTime = true;
    
    [Tooltip("Время уничтожения pickUpTreasure")] 
    public float PickUpTreasureDestroyTimer = 10f;
    
    //***********************************************************************//

    public EnemySpawner Spawner { get; set; }

    public void Update()
    {
      Spawner?.Tick();
    }
  }
}