using System;
using System.Runtime.InteropServices;
using Builds;
using Firebase;
using SaveLoadServices;
using SceneLoaders;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Firebase.Extensions;

namespace Projects
{
  public class ProjectInitializer : MonoBehaviour, IInitializable
  {
    public BuildTypeId BuildType = BuildTypeId.Playable;

    private const int Width = 1080;
    private const int Height = 1920;

    // ReSharper disable once NotAccessedField.Local
    private static FirebaseApp s_app;

    private SceneLoader _sceneLoader;
    private ProjectData _projectData;
    private AdvertismentService _adService;

    [Inject]
    private void Construct(SceneLoader sceneLoader, ProjectData projectData, AdvertismentService adService)
    {
      _sceneLoader = sceneLoader;
      _projectData = projectData;
      _adService = adService;
    }

    public void Initialize()
    {
      string sceneName = SceneManager.GetActiveScene().name;

      switch (sceneName)
      {
        case nameof(SceneId.Initial):
          InitializeProject(ConfigId.Default, SceneId.DefaultHubDust);
          break;
        
        case nameof(SceneId.VladHubTestStarter):
          InitializeProject(ConfigId.Vlad, SceneId.VladHubTest);
          break;

        case nameof(SceneId.ValeraHubTestStarter):
          InitializeProject(ConfigId.Valera, SceneId.ValeraHubTest);
          break;

        case nameof(SceneId.VovaHubTestStarter):
          InitializeProject(ConfigId.Vova, SceneId.VovaHubTest);
          break;

        default:
          throw new Exception("С этой сцены нельзя стартовать");
      }
    }

    private void InitializeProject(ConfigId configId, SceneId initialSceneId)
    {
      switch (BuildType)
      {
        case BuildTypeId.Playable:
          InitFirebase();
          _adService.InitCas();
          break;

        case BuildTypeId.Creative:
          Debug.Log("В креативном билде FireBase не работает");
          break;

        case BuildTypeId.Unknown:
        default:
          throw new Exception("В префабе ProjectContext (ProjectInitializer) не указан тип сборки (BuildType)");
      }

      if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        Screen.SetResolution(Width, Height, FullScreenMode.Windowed);

      _projectData.ConfigId = configId;
      _projectData.InitialSceneId = initialSceneId;
      _sceneLoader.Load(SceneId.LoadConfigs);
    }

    private static void InitFirebase()
    {
      FirebaseApp
        .CheckAndFixDependenciesAsync()
        .ContinueWithOnMainThread
        (
          task =>
          {
            DependencyStatus dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
              s_app = FirebaseApp.DefaultInstance;
            }
            else
            {
              Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
          }
        );
    }
  }
}