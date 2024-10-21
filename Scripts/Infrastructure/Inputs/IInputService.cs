using TimeServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Inputs
{
  public class InputService : ITickable
  {
    private readonly TimeService _timeService;

    private readonly InputAction _move;
    private readonly InputAction _restart;
    private readonly InputAction _deleteSaves;
    private readonly InputAction _openQuestWindow;

    public InputService(TimeService timeService)
    {
      _timeService = timeService;

      var controls = new Controls();

      _move = controls.Player.Move;
      _move.Enable();
    }

    public bool HasMoveInput =>
      _move.ReadValue<Vector2>() != Vector2.zero
      || MoveDirectionFloatingJoystick != Vector2.zero;

    public Vector2 MoveDirection
    {
      get
      {
        if (MoveDirectionFloatingJoystick != Vector2.zero)
          return MoveDirectionFloatingJoystick;
        else
          return _move.ReadValue<Vector2>();
      }
    }

    public Vector2 MoveDirectionFloatingJoystick { private get; set; }

    public void Tick()
    {
      if (_timeService.IsPaused)
        return;

      const string JoystickName = "Movement1.5";
      const string JoystickName2 = "Movement10and3/4";

      float axisX1 = UltimateJoystick.GetHorizontalAxis(joystickName: JoystickName);
      float axisY1 = UltimateJoystick.GetVerticalAxis(joystickName: JoystickName);

      float axisX2 = UltimateJoystick.GetHorizontalAxis(joystickName: JoystickName2);
      float axisY2 = UltimateJoystick.GetVerticalAxis(joystickName: JoystickName2);

      float axisX = axisX1 + axisX2;
      float axisY = axisY1 + axisY2;

      MoveDirectionFloatingJoystick = new Vector2(x: axisX, y: axisY);
    }
  }
}