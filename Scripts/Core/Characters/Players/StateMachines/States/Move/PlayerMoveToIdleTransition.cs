using Core.Characters.FiniteStateMachines;
using DevConfigs;

namespace Core.Characters.Players.States
{
  public class PlayerMoveToIdleTransition : Transition
  {
    private readonly PlayerInputHandler _inputHandler;

    public PlayerMoveToIdleTransition(PlayerInputHandler inputHandler)
    {
      _inputHandler = inputHandler;
    }

    public override void Tick()
    {
      if (_inputHandler.GetDirection().magnitude > DevConfig.MinMoveDirectionLength)
        return;

      Enter<PlayerIdleState>();
    }
  }
}