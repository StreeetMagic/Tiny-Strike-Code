using System.Collections.Generic;
using ConfigProviders;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerDefuseBombState : State
  {
    private readonly BalanceConfigProvider _configProvider;
    private readonly PlayerBombLocator _playerBombLocator;
    private readonly PlayerAnimatorController _playerAnimatorController;

    public PlayerDefuseBombState(List<Transition> transitions, BalanceConfigProvider configProvider,
      PlayerBombLocator playerBombLocator, PlayerAnimatorController playerAnimatorController) : base(transitions)
    {
      _configProvider = configProvider;
      _playerBombLocator = playerBombLocator;
      _playerAnimatorController = playerAnimatorController;
    }

    public override void Enter()
    {
      _playerAnimatorController.PlayDefuseBomb();
    }

    protected override void OnTick()
    {
      _playerBombLocator.ClosestBomb.DefuseTick(_configProvider.Player.BombDefuseDuration);
    }

    public override void Exit()
    {
      _playerAnimatorController.StopDefuseBomb();

      if (!_playerBombLocator.ClosestBomb.IsDefused())
      {
        _playerBombLocator.ClosestBomb.ResetProgress();
      }
    }
  }
}