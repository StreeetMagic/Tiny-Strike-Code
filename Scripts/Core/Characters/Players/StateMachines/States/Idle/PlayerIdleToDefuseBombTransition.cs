using ConfigProviders;
using Core.Characters.FiniteStateMachines;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.States
{
  public class PlayerIdleToDefuseBombTransition : Transition
  {
    [Inject] private PlayerBombLocator _playerBombLocator;
    [Inject] private BalanceConfigProvider _configProvider;
    [Inject] private Transform _transform;

    public override void Tick()
    {
      if (!_playerBombLocator.ClosestBomb)
        return;

      if (Vector3.Distance(_transform.position, _playerBombLocator.ClosestBomb.transform.position) <= _configProvider.Player.BombDefuseRadius)
        Enter<PlayerDefuseBombState>();
    }
  }
}