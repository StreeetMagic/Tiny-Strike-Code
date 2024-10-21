using Prefabs;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace Core.Characters.Players
{
  public class PlayerFactory
  {
    private readonly HubZenjectFactory _factory;
    private readonly PlayerProvider _playerProvider;
    private readonly PlayerRespawnPosition _playerRespawnPosition;

    public PlayerFactory(HubZenjectFactory factory, PlayerProvider playerProvider,
      PlayerRespawnPosition playerRespawnPosition)
    {
      _factory = factory;
      _playerProvider = playerProvider;
      _playerRespawnPosition = playerRespawnPosition;
    }

    public void Create(Transform parent)
    {
      Vector3 position = _playerRespawnPosition.Position();
      
      Player player = _factory.InstantiatePrefabForComponent<Player>(PrefabId.Player, position, Quaternion.identity, parent);

      player.transform.SetParent(null);
      _playerProvider.Instance = player.GetComponent<PlayerInstaller>();
    }

    public void Destroy()
    {
      PlayerInstaller player = _playerProvider.Instance;

      Object.Destroy(player.gameObject);
      _playerProvider.Instance = null;
    }
  }
}