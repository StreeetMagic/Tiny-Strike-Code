using System;
using AudioServices;
using Core.Weapons;
using Meta;
using Meta.Currencies;
using Meta.Expirience;
using Meta.Upgrades;
using Projects;
using SaveLoadServices;
using SceneLoaders;
using UnityEngine;
using Zenject;

namespace Scenes.LoadProgress
{
  public class LoadProgressInitializer : MonoBehaviour, IInitializable
  {
    private ISaveLoadService _saveLoadService;
    private SceneLoader _sceneLoader;
    
    private ProjectData _projectData;
    
    private UpgradeService _upgradeService;
    private AudioService _audioService;
    private CompositeQuestStorage _compositeQuestStorage;
    private SimpleQuestStorage _simpleQuestStorage;
    private WeaponStorage _weaponsStorage;
    private CurrencyStorage _currencyStorage;
    private ExpierienceStorage _expierienceStorage;
    private AdvertismentService _advertismentService;

    [Inject]
    private void Construct(ISaveLoadService saveLoadService, SceneLoader sceneLoader, UpgradeService upgradeService, AudioService audioService,
      CompositeQuestStorage compositeQuestStorage, SimpleQuestStorage simpleQuestStorage, ProjectData projectData, WeaponStorage weaponsStorage,
      ExpierienceStorage expierienceStorage, CurrencyStorage currencyStorage, AdvertismentService advertismentService)
    {
      _saveLoadService = saveLoadService;
      _sceneLoader = sceneLoader;
      _upgradeService = upgradeService;
      _audioService = audioService;
      _compositeQuestStorage = compositeQuestStorage;
      _simpleQuestStorage = simpleQuestStorage;
      _projectData = projectData;
      _weaponsStorage = weaponsStorage;
      _expierienceStorage = expierienceStorage;
      _currencyStorage = currencyStorage;
      _advertismentService = advertismentService;
    }

    public void Initialize()
    {
      _saveLoadService.ProgressReaders.Add(_upgradeService);
      _saveLoadService.ProgressReaders.Add(_audioService);
      _saveLoadService.ProgressReaders.Add(_compositeQuestStorage);
      _saveLoadService.ProgressReaders.Add(_simpleQuestStorage);
      _saveLoadService.ProgressReaders.Add(_weaponsStorage);
      _saveLoadService.ProgressReaders.Add(_currencyStorage);
      _saveLoadService.ProgressReaders.Add(_expierienceStorage);
      _saveLoadService.ProgressReaders.Add(_advertismentService);

      _saveLoadService.LoadProgress();

      LoadScene();
    }

    private void LoadScene()
    {
      switch (_projectData.InitialSceneId)
      {
        case SceneId.Unknown:
          throw new Exception("Unknown scene id");

        case SceneId.DefaultHubDust:
          _sceneLoader.Load(SceneId.DefaultHubDust);
          break;

        case SceneId.VladHubTest:
          _sceneLoader.Load(SceneId.VladHubTest);
          break;

        case SceneId.ValeraHubTest:
          _sceneLoader.Load(SceneId.ValeraHubTest);
          break;

        case SceneId.VovaHubTest:
          _sceneLoader.Load(SceneId.VovaHubTest);
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}