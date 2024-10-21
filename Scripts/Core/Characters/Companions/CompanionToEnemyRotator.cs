using Core.Characters.Players;
using UnityEngine;

namespace Core.Characters.Companions
{
  public class CompanionToEnemyRotator
  {
    private readonly Companion _companion;
    private readonly PlayerProvider _playerProvider;

    public CompanionToEnemyRotator(Companion companion, PlayerProvider playerProvider)
    {
      _companion = companion;
      _playerProvider = playerProvider;
    }

    public void Rotate()
    {
      if (!_playerProvider.Instance)
        return;

      if (_playerProvider.Instance.TargetHolder.CurrentTarget == null)
        return;

      Vector3 target = _playerProvider.Instance.TargetHolder.CurrentTarget.TargetPoint.transform.position;
      Vector3 direction = target - _companion.transform.position;

      direction.y = 0f;

      _companion.transform.rotation = Quaternion.LookRotation(direction);
    }
  }
}