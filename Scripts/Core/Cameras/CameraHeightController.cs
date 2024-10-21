using Core.Characters.Players;
using UnityEngine;
using Zenject;

namespace Core.Cameras
{
  public class CameraHeightController : MonoBehaviour
  {
    [Inject] private PlayerProvider _playerProvider;
    [Inject] private CameraProvider _cameraProvider;

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;

      if (_playerProvider.Instance.InputHandler == null)
        return;

      if (_playerProvider.Instance.MoveSpeed.IsMoving)
        RaiseCamera();
      else
        DownCamera();
    }

    private void RaiseCamera()
    {
      _cameraProvider.TopCamera.VirtualCamera.Priority = 11;
      _cameraProvider.BotCamera.VirtualCamera.Priority = 10;
    }

    private void DownCamera()
    {
      _cameraProvider.TopCamera.VirtualCamera.Priority = 10;
      _cameraProvider.BotCamera.VirtualCamera.Priority = 11;
    }
  }
}