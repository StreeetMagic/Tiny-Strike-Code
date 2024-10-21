using UnityEngine;

namespace Core.Projectiles
{
  public class ProjectileMover
  {
    private readonly float _speed;
    private Vector3 _currentPosition;
    private Vector3 _futurePosition;
    
    public ProjectileMover(float speed)
    {
      _speed = speed;
    }

    public bool MoveProjectile(Transform transform, LayerMask layerMask, out RaycastHit hit)
    {
      _currentPosition = transform.position;
      Vector3 direction = transform.forward * (_speed * Time.deltaTime);
      _futurePosition = _currentPosition + direction;

      if (Physics.Linecast(_currentPosition, _futurePosition, out hit, layerMask))
      {
        transform.position = hit.point;
        return false;
      }
      else
      {
        transform.position = _futurePosition;
        return true;
      }
    }
  }
}