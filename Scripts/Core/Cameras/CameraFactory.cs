using Cinemachine;
using ConfigProviders;
using Core.Characters.Players;
using Prefabs;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace Core.Cameras
{
  public class CameraFactory
  {
    private const string BotCamera = nameof(BotCamera);
    private const string TopCamera = nameof(TopCamera);

    private readonly HubZenjectFactory _factory;
    private readonly PlayerProvider _playerFactory;
    private readonly CameraProvider _cameraProvider;
    private readonly DevConfigProvider _devConfigProvider;

    public CameraFactory(HubZenjectFactory factory,
      PlayerProvider playerFactory, CameraProvider cameraProvider, DevConfigProvider devConfigProvider)
    {
      _factory = factory;
      _playerFactory = playerFactory;
      _cameraProvider = cameraProvider;
      _devConfigProvider = devConfigProvider;
    }

    public void Create(Transform parent)
    {
      var player = _playerFactory.Instance;

      CreateCamera(parent, player.Transform, BotCamera, 11);
      CreateCamera(parent, player.Transform, TopCamera, 10);

      _cameraProvider.MainCamera = Object.FindObjectOfType<Camera>();
    }

    private void CreateCamera(Transform parent, Transform player, string cameraType, int priority)
    {
      PrefabId prefabId = cameraType == BotCamera
        ? PrefabId.BotCamera
        : PrefabId.TopCamera;

      var prefab = _devConfigProvider.GetPrefabForComponent<TopDownCamera>(prefabId);

      var camera = _factory.InstantiatePrefabForComponent(prefab, parent);

      if (cameraType == BotCamera)
        _cameraProvider.BotCamera = camera;
      else if (cameraType == TopCamera)
        _cameraProvider.TopCamera = camera;

      camera.transform.SetParent(null);

      var cmCam = camera.GetComponent<CinemachineVirtualCamera>();
      cmCam.Priority = priority;
      cmCam.Follow = player;
      cmCam.LookAt = player;
    }
  }
}