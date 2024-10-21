using System;
using UnityEngine;
using Utilities;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerMoveSpeed : ITickable
  {
    private readonly Transform _transform;
    private Vector3 _previousPosition;

    public ReactiveProperty<float> CurrentMoveSpeed { get; } = new(0f);
    public bool IsMoving => CurrentMoveSpeed.Value > 0f;

    public PlayerMoveSpeed(Transform transform)
    {
      _transform = transform;
      _previousPosition = _transform.position;
    }

    public void Tick()
    {
      if (_previousPosition != _transform.position)
      {
        float speed = (_transform.position - _previousPosition).magnitude / Time.fixedDeltaTime;

        if (Math.Abs(speed - CurrentMoveSpeed.Value) > .01f)
          CurrentMoveSpeed.Value = speed;
      }
      else
      {
        if (CurrentMoveSpeed.Value > 0f)
          CurrentMoveSpeed.Value = 0f;
      }

      _previousPosition = _transform.position;
    }
  }
}