using System.Collections.Generic;
using Scenes;
using UnityEngine;
using Zenject;

namespace Projects
{
  public enum PlatformId
  {
    Uknown = 0,

    Android = 1,
    IOS = 2,
    UnityEditor = 3,
    WindowsComputer = 4,
  }

  public class ProjectData : IInitializable
  {
    private readonly Dictionary<SceneId, SceneTypeId> _scenes = new()
    {
      { SceneId.DefaultHubDust, SceneTypeId.Core },
      { SceneId.VladHubTest, SceneTypeId.Core },
      { SceneId.ValeraHubTest, SceneTypeId.Core },
      { SceneId.VovaHubTest, SceneTypeId.Core },

      { SceneId.DefaultArenaSand, SceneTypeId.Arena },

      { SceneId.Unknown, SceneTypeId.Infrastructure },
      { SceneId.Initial, SceneTypeId.Infrastructure },
      { SceneId.Empty, SceneTypeId.Infrastructure },
      { SceneId.LoadConfigs, SceneTypeId.Infrastructure },
      { SceneId.LoadProgress, SceneTypeId.Infrastructure },
      { SceneId.ChooseGameMode, SceneTypeId.Infrastructure },
    };

    public SceneId InitialSceneId { get; set; }
    public ConfigId ConfigId { get; set; }

    public bool IsUnityEditor { get; private set; }
    public PlatformId PlatformId { get; private set; }

    public bool HasInternetConnection { get; private set; }

    public void Initialize()
    {
      PlatformId = PlatformId.Uknown;

#if UNITY_EDITOR
      IsUnityEditor = true;
#endif

#if UNITY_ANDROID
      PlatformId = PlatformId.Android;
#endif

#if UNITY_IOS
      PlatformId = PlatformId.iOS;
#endif

#if UNITY_STANDALONE_WIN
      PlatformId = PlatformId.WindowsComputer;
#endif

      if (PlatformId == PlatformId.Uknown)
        throw new System.Exception("Мы не смогли определить PlatformId");
      else
      {
        //Debug.Log("PlatformId: " + PlatformId);
      }

      //Debug.Log("IsUnityEditor: " + IsUnityEditor);
    }

    public void EnableInternetConnection()
    {
      HasInternetConnection = true;
    }

    public void DisableInternetConnection()
    {
      HasInternetConnection = false;
    }
  }
}