using Inputs;
using UnityEngine;

namespace Core.Characters.Players
{
  public class PlayerInputHandler
  {
    private readonly InputService _inputService;

    public PlayerInputHandler(InputService inputService)
    {
      _inputService = inputService;
    }

    public Vector3 GetDirection()
    {
      Vector2 directionXY = _inputService.MoveDirection;

      return new Vector3(directionXY.x, 0, directionXY.y);
    }
  }
}