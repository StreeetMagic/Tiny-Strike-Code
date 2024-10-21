using ConfigProviders;
using Core.Characters.Companions;
using Core.Characters.Companions.Configs;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace Core.Spawners.Companions
{
  public class CompanionSpawner
  {
    private readonly ArtConfigProvider _artConfigProvider;
    private readonly HubZenjectFactory _hubZenjectFactory;
    private readonly BalanceConfigProvider _balanceConfigProvider;

    public CompanionSpawner(ArtConfigProvider artConfigProvider, HubZenjectFactory hubZenjectFactory,
      BalanceConfigProvider balanceConfigProvider)
    {
      _artConfigProvider = artConfigProvider;
      _hubZenjectFactory = hubZenjectFactory;
      _balanceConfigProvider = balanceConfigProvider;
    }

    public Companion Spawn(CompanionId id, TransformContainer container)
    {
      Companion prefab = _artConfigProvider.Companions[id].Prefab;
      Companion instance = _hubZenjectFactory.InstantiatePrefabForComponent(prefab, container.Transform.position, Quaternion.identity, container.Transform);
      instance.Installer.TransformContainer = container;
      instance.Installer.Config = _balanceConfigProvider.Companions[id];
      instance.transform.SetParent(null);

      return null;
    }
  }
}