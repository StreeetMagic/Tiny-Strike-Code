using Core.Characters.Players;
using UnityEngine;

namespace Core.Characters.Enemies
{
  public class EnemyToPlayerRotator
  {
    private readonly Enemy _enemy;
    private readonly PlayerProvider _playerProvider;

    public EnemyToPlayerRotator(Enemy enemy, PlayerProvider playerProvider)
    {
      _enemy = enemy;
      _playerProvider = playerProvider;
    }

    public void Rotate()
    {
      Vector3 target = _playerProvider.Instance.transform.position;

      Vector3 direction = target - _enemy.transform.position;

      _enemy.transform.rotation = Quaternion.LookRotation(direction);
    }
  }
}