using ConfigProviders;
using Core.Characters.FiniteStateMachines;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.States
{
  public class PlayerIdleToRescueHostageTransition : Transition
  {
    [Inject] private PlayerHostageLocator _playerHostageLocator;
    [Inject] private PlayerHostageHolder _playerHostageHolder;
    [Inject] private BalanceConfigProvider _configProvider;
    [Inject] private Transform _transform;

    public override void Tick()
    {
      if (!_playerHostageLocator.ClosestHostage)
        return;
      
      if (_playerHostageHolder.Hostage)
        return;

      if (Vector3.Distance(_transform.position, _playerHostageLocator.ClosestHostage.transform.position) <= _configProvider.Player.HostageReleaseRadius)
        Enter<PlayerRescueHostageState>();
    }
  }
}