namespace LevelDesign.EnemySpawnMarkers
{
  public enum EnemySpawnerType
  {
    Uknown = 0,
     
    FromStart = 1,
    OnOtherSpawnerCleared = 2,
    OnSimpleQuestActivated = 3,
    OnCompositeQuestActivated = 4
  }
}