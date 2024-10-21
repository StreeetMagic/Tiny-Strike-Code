using Core.Spawners.Enemies;

namespace Core.Characters.Enemies.Phases
{
  public class EnemyPhase
  {
    private readonly EnemyConfig _enemyConfig;
    private readonly EnemySpawnerFactory _enemySpawnerFactory;
    private readonly EnemyInstaller _enemyInstaller;

    public EnemyPhase(EnemyConfig enemyConfig, IHealth health, EnemyInstaller enemyInstaller, EnemySpawnerFactory enemySpawnerFactory)
    {
      _enemyConfig = enemyConfig;
      _enemyInstaller = enemyInstaller;
      _enemySpawnerFactory = enemySpawnerFactory;

      health.Current.ValueChanged += OnHealthChanged;
    }

    public bool Passed { get; private set; }

    private void OnHealthChanged(float current)
    {
      if (!_enemyConfig.HasPhases)
        return;

      if (Passed)
        return;

      float max = _enemyConfig.InitialHealth;
      float threshold = max * _enemyConfig.PhaseThreshold;

      if (current < threshold)
      {
        Passed = true;
        SpawnEnemies();
      }
    }

    private void SpawnEnemies()
    {
      _enemySpawnerFactory.CreateSingle(_enemyConfig.EnemyTypeId, _enemyInstaller.transform, _enemyConfig.EnemySpawnCount);
    }
  }
}