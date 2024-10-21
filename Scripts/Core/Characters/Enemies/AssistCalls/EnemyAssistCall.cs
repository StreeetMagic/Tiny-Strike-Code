using DevConfigs;
using UnityEngine;

namespace Core.Characters.Enemies
{
  public class EnemyAssistCall
  {
    private readonly Transform _transform;
    private readonly EnemyConfig _enemyConfig;

    private readonly Collider[] _enemies;

    public EnemyAssistCall(Transform transform, EnemyConfig enemyConfig)
    {
      _transform = transform;
      _enemyConfig = enemyConfig;

      _enemies = new Collider[DevConfig.EnemyAssistCallOverlapCount];
    }

    public void Call()
    {
      int count = Physics.OverlapSphereNonAlloc(_transform.position, _enemyConfig.AssistCallRadius, _enemies);

      for (int i = 0; i < count; i++)
        if (_enemies[i].TryGetComponent(out ITargetTrigger trigger))
          trigger.Hit();
    }
  }
}