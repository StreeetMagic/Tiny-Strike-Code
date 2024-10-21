using AudioServices;
using ConfigProviders;
using SceneLoaders;
using UnityEngine;
using Zenject;

namespace Scenes.LoadConfigs
{
  public class LoadConfigsInitializer : MonoBehaviour, IInitializable
  {
    [Inject] private SceneLoader _sceneLoader;

    [Inject] private BalanceConfigProvider _balanceConfigProvider;
    [Inject] private ArtConfigProvider _artConfigProvider;
    [Inject] private DevConfigProvider _devConfigProvider;

    [Inject] private AudioService _audioService;

    public void Initialize()
    {
      _balanceConfigProvider.LoadConfigs();
      _artConfigProvider.LoadConfigs();
      _devConfigProvider.LoadConfigs();

      _audioService.Init();
      _sceneLoader.Load(SceneId.LoadProgress);
    }
  }
}