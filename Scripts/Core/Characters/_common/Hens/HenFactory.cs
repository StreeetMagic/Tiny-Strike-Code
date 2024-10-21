using Core.Characters.Players;
using Prefabs;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace Core.Characters.Hens
{
  public class HenFactory
  {
    private readonly HubZenjectFactory _factory;
    private readonly PlayerProvider _playerProvider;

    public HenFactory(HubZenjectFactory factory, PlayerProvider playerProvider)
    {
      _factory = factory;
      _playerProvider = playerProvider;
    }

    public Hen Create()
    {
      Vector3 position = _playerProvider.Instance.HenContainer.GetRandomSpawnPoint().position;

      // var hen = _factory.InstantiateGameObject(PrefabId.Hen).GetComponent<Hen>();
      var hen = _factory.InstantiatePrefabForComponent<Hen>(PrefabId.Hen);

      hen.transform.position = position;
      hen.transform.SetParent(null);
      return hen;
    }
  }
}