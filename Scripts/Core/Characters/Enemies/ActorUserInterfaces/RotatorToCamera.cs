using Core.Characters.Players;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class RotatorToCamera : MonoBehaviour
  {
    [Tooltip("Расстояние, на которое нужно приблизиться к камере")]
    public float Offset;

    [Tooltip("Расстояние, на которое нужно поднять по высоте")]
    public float HeightOffset;

    [Inject] private PlayerProvider _playerProvider;

    private Camera _camera;
    private Transform _parentTransform;

    private void Awake()
    {
      _camera = Camera.main;
      _parentTransform = transform.parent;
    }

    private void LateUpdate()
    {
      Vector3 parentPosition = _parentTransform.position;
      Vector3 cameraPosition = _camera.transform.position;

      Vector3 directionToCamera = (cameraPosition - parentPosition).normalized;

      Vector3 newPosition = parentPosition + directionToCamera * Offset;
      newPosition.y += HeightOffset;

      if (!IsVisible(newPosition))
        return;

      transform.position = newPosition;
      transform.LookAt(transform.position + _camera.transform.forward, _camera.transform.up);
    }

    private bool IsVisible(Vector3 position)
    {
      Vector3 viewportPoint = _camera.WorldToViewportPoint(position);
      
      return viewportPoint.z > 0 &&
             viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
             viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }
  }
}