using ConfigProviders;
using Core.Characters.Players;
using Core.Grenades;
using Prefabs;
using UnityEngine;
using ZenjectFactories.SceneContext;
using Random = UnityEngine.Random;

namespace Core.Characters.Enemies
{
  public class EnemyGrenadeThrower
  {
    private readonly PlayerProvider _playerProvider;
    private readonly HubZenjectFactory _gameLoopZenjectFactory;
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly EnemyConfig _config;
    private readonly Transform _transform;
    private readonly EnemyGrenadeStorage _grenadeStorage;

    public EnemyGrenadeThrower(PlayerProvider playerProvider, HubZenjectFactory gameLoopZenjectFactory,
      BalanceConfigProvider balanceConfigProvider, EnemyConfig config, EnemyGrenadeStorage grenadeStorage, Transform transform)
    {
      _playerProvider = playerProvider;
      _gameLoopZenjectFactory = gameLoopZenjectFactory;
      _balanceConfigProvider = balanceConfigProvider;
      _config = config;
      _grenadeStorage = grenadeStorage;
      _transform = transform;
    }

    public void Lauch()
    {
      if (!_config.UnlimitedGrenadesCount)
        _grenadeStorage.SpendGrenade();

      GrenadeTypeId grenadeTypeId = _config.GrenadeTypeId;

      //var grenade = _gameLoopZenjectFactory.InstantiateGameObject(PrefabId.Grenade, _transform).GetComponent<Grenade>();
      var grenade = _gameLoopZenjectFactory.InstantiatePrefabForComponent<Grenade>(PrefabId.Grenade, _transform);

      grenade.transform.SetParent(null);
      grenade.transform.position = _transform.position;

      Vector3 targetPosition = _playerProvider.Instance.transform.position;

      var offset = .6f;

      float xOffset = Random.Range(-offset, offset);
      float zOffset = Random.Range(-offset, offset);

      Vector3 newPosition = new Vector3(targetPosition.x + xOffset, targetPosition.y, targetPosition.z + zOffset);

      var mover = grenade.GetComponent<GrenadeMover>();
      mover.Init(_balanceConfigProvider.Grenades[grenadeTypeId], _transform.position, newPosition);

      var detonator = grenade.GetComponent<GrenadeDetonator>();
      detonator.Init(_balanceConfigProvider.Grenades[grenadeTypeId], _balanceConfigProvider.Grenades[_config.GrenadeTypeId].DetonationRadius);

      mover.Throw();
    }
  }
}