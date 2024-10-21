using Windows;
using AudioServices;
using AudioServices.Sounds;
using Core;
using Core.Cameras;
using Core.Characters.Hens;
using Core.Characters.Players;
using Core.Spawners.Enemies;
using CoroutineRunners;
using DG.Tweening;
using HeadsUpDisplays;
using LevelDesign.Maps;
using Meta;
using Meta.BackpackStorages;
using Meta.Chests;
using Meta.Stats;
using Meta.Upgrades;
using SaveLoadServices;
using SceneLoaders;
using Tutorials;
using UnityEngine;
using Zenject;

namespace Scenes.Hubs
{
  public class HubInitializer : MonoBehaviour
  {
    [Inject]
    private SceneLoader _sceneLoader;

    [Inject]
    private ISaveLoadService _saveLoadService;

    [Inject]
    private HubInstaller _hubInstaller;

    [Inject]
    private UpgradeService _upgradeService;

    [Inject]
    private AudioService _audioService;

    [Inject]
    private PlayerStatsProvider _playerStatsProvider;

    [Inject]
    private ICoroutineRunner _runner;

    [Inject]
    private CompositeQuestStorage _compositeQuestStorage;

    [Inject]
    private BackpackStorage _backpackStorage;

    [Inject]
    private HenSpawner _henSpawner;

    [Inject]
    private MapFactory _mapFactory;

    [Inject]
    private CameraFactory _cameraFactory;

    [Inject]
    private EnemySpawnerFactory _enemySpawnerFactory;

    [Inject]
    private HeadsUpDisplayFactory _headsUpDisplayFactory;

    [Inject]
    private PlayerFactory _playerFactory;

    [Inject]
    private TutorialFactory _tutorialFactory;

    [Inject]
    private MapProvider _mapProvider;

    [Inject]
    private ChestManager _chestManager;

    [Inject]
    private WindowService _windowService;

    [Inject]
    private PlayerRespawnPosition _playerRespawnPosition;
    
    private void Start()
    {
      Time.timeScale = 1f;

      _saveLoadService.ProgressReaders.Add(_playerRespawnPosition);
      _saveLoadService.LoadProgress();

      _mapFactory.Init();
      _playerStatsProvider.Create();
      _playerFactory.Create(_hubInstaller.transform);
      _cameraFactory.Create(_hubInstaller.transform);
      _enemySpawnerFactory.Create();
      _tutorialFactory.Create();
      _headsUpDisplayFactory.Create(_hubInstaller.transform);
      _chestManager.Start();
      
      _saveLoadService.LoadProgress();

      _audioService.Play(SoundId.TestMusic);
      _windowService.WarmUp();
    }

    public void Restart()
    {
      Destroy();
      _playerStatsProvider.ClearAllCheats();

      _sceneLoader.Load(SceneId.LoadProgress);
    }

    private void Destroy()
    {
      _runner.StopAllCoroutines();
      DOTween.KillAll();
      Time.timeScale = 0f;
      _audioService.StopAll();

      _saveLoadService.ProgressReaders.Remove(_playerRespawnPosition);
      _henSpawner.DeSpawnAll();
      _headsUpDisplayFactory.Destroy();
      //  _tutorialFactory.Destroy();
      _enemySpawnerFactory.Destroy();
      //_cameraFactory.Destroy();
      _playerFactory.Destroy();
      _playerStatsProvider.Stop();
      _mapFactory.Dispose();
    }

    // private void LogScenes()
    // {
    //   List<SceneId> sceneList = _sceneLoader.LoadedScenes;
    //
    //   var scenes = "";
    //
    //   foreach (SceneId scene in sceneList)
    //     scenes += scene + " ";
    //
    //   Debug.Log(scenes);
    // }
  }
}