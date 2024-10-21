using System.Collections.Generic;
using Windows;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerDieState : State
  {
    private readonly PlayerHealth _health;
    private readonly PlayerAnimatorController _animatorController;
    private readonly WindowService _windowService;
    
    public PlayerDieState(List<Transition> transitions, PlayerHealth playerHealth, 
      PlayerAnimatorController animatorController, WindowService windowService) : base(transitions)
    {
      _health = playerHealth;
      _animatorController = animatorController;
      _windowService = windowService;
    }

    public override void Enter()
    {
      _health.Die();
      _animatorController.PlayDeathAnimation();
      _windowService.Open(WindowId.DeadScreenWindow);
    }

    protected override void OnTick()
    {
    }

    public override void Exit()
    {
      _health.Revive();
      _animatorController.PLayIdle();
    }
  }
}