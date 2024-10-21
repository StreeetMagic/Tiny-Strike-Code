using Core.Characters.Players;
using RandomServices;
using UnityEngine;

namespace Core.Characters.Enemies
{
  public class EnemyGrenadeThrowerStatus
  {
    private readonly PlayerProvider _playerProvider;
    private readonly EnemyConfig _config;
    private readonly EnemyGrenadeStorage _grenadeStorage;
    private readonly EnemyGrenadeThrowTimer _grenadeThrowTimer;
    private readonly RandomService _randomService;
    private readonly Transform _transform;

    private float _randomThrowDelayLeft;

    public EnemyGrenadeThrowerStatus(PlayerProvider playerProvider, EnemyConfig config, 
      EnemyGrenadeStorage grenadeStorage, EnemyGrenadeThrowTimer grenadeThrowTimer, RandomService randomService, Transform transform)
    {
      _playerProvider = playerProvider;
      _config = config;
      _grenadeStorage = grenadeStorage;
      _grenadeThrowTimer = grenadeThrowTimer;
      _randomService = randomService;
      _transform = transform;
    }

    public bool IsReady()
    {
      if (Vector3.Distance(_playerProvider.Instance.transform.position, _transform.position) > _config.GrenadeThrowRange)
        return false;
      
      if (_config.IsGrenadeThrower == false)
        return false;

      if (_grenadeStorage.HasGrenades == false)
        return false;

      if (TargetIsStandsOnSamePosition() == false)
      {
        _randomThrowDelayLeft = _randomService.GetRandomFloat(_config.GrenadeThrowRandomDelay);

        return false;
      }

      if (_grenadeThrowTimer.IsUp == false)
        return false;

      if (_randomThrowDelayLeft > 0)
      {
        _randomThrowDelayLeft -= Time.deltaTime;
        return false;
      }

      return true;
    }

    private bool TargetIsStandsOnSamePosition() =>
      _playerProvider.Instance.StandsOnSamePosition.TimeOnSamePosition >= _config.TargetStandsOnSamePositionTime;
  }
}