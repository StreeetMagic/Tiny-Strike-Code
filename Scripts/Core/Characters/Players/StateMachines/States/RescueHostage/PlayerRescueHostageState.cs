using System.Collections.Generic;
using ConfigProviders;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerRescueHostageState : State
  {
    private readonly BalanceConfigProvider _configProvider;
    private readonly PlayerHostageLocator _playerHostageLocator;
    private readonly PlayerHostageHolder _playerHostageHolder;

    public PlayerRescueHostageState(List<Transition> transitions, PlayerHostageLocator playerHostageLocator,
      BalanceConfigProvider configProvider, PlayerHostageHolder playerHostageHolder) : base(transitions)
    {
      _playerHostageLocator = playerHostageLocator;
      _configProvider = configProvider;
      _playerHostageHolder = playerHostageHolder;
    }

    public override void Enter()
    {
    }

    protected override void OnTick()
    {
      _playerHostageLocator.ClosestHostage.ResqueTick(_configProvider.Player.HostageResqueDuration);

      if (_playerHostageLocator.ClosestHostage.IsResqued() & !_playerHostageHolder.Hostage)
        _playerHostageHolder.PickUp(_playerHostageLocator.ClosestHostage);
    }

    public override void Exit()
    {
      if (!_playerHostageLocator.ClosestHostage.IsResqued())
      {
        _playerHostageLocator.ClosestHostage.ResetProgress();
      }
    }
  }
}