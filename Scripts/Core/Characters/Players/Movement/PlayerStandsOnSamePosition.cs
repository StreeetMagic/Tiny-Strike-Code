using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerStandsOnSamePosition : ITickable
  {
    private readonly Transform _transform;

    private Vector3 _lastPosition;

    public PlayerStandsOnSamePosition(Transform transform)
    {
      _transform = transform;

      _lastPosition = _transform.position;
      TimeOnSamePosition = 0;
    }

    public float TimeOnSamePosition { get; private set; }

    public void Tick()
    {
      if (_transform.position == _lastPosition)
        TimeOnSamePosition += Time.deltaTime;
      else
        TimeOnSamePosition = 0;

      _lastPosition = _transform.position;
    }
  }
}