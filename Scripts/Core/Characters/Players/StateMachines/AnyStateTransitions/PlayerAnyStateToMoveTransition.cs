using Core.Characters.FiniteStateMachines;
using Core.Characters.Players.States;
using DevConfigs;
using Inputs;

namespace Core.Characters.Players.AnyStateTransitions
{
  public class PlayerAnyStateToMoveTransition : Transition
  {
    private readonly PlayerInputHandler _inputHandler;
    private readonly InputService _inputService;
    private readonly PlayerAnimatorController _playerAnimator;
    private readonly PlayerHealth _playerHealth;

    public PlayerAnyStateToMoveTransition(InputService inputService, PlayerAnimatorController playerAnimator, PlayerHealth playerHealth, PlayerInputHandler inputHandler)
    {
      _inputService = inputService;
      _playerAnimator = playerAnimator;
      _playerHealth = playerHealth;
      _inputHandler = inputHandler;
    }

    public override void Tick()
    {
      if (_playerHealth.IsDead)
        return;

      if (!_inputService.HasMoveInput)
        return;
      
      if (_inputHandler.GetDirection().magnitude < DevConfig.MinMoveDirectionLength)
        return;

      _playerAnimator.OffStateShooting();
      Enter<PlayerMoveState>();
    }
  }
}