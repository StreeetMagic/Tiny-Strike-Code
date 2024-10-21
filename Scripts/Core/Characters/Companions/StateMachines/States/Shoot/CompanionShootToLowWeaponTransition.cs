using Core.Characters.Companions.States.LowWeapon;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;
using DevConfigs;
using UnityEngine;

namespace Core.Characters.Companions.States.Shoot
{
  public class CompanionShootToLowWeaponTransition : Transition
  {
    private readonly CompanionWeaponMagazine _magazine;
    private readonly Transform _transform;
    private readonly PlayerProvider _playerProvider;

    public CompanionShootToLowWeaponTransition(CompanionWeaponMagazine magazine, Transform transform, PlayerProvider playerProvider)
    {
      _magazine = magazine;
      _transform = transform;
      _playerProvider = playerProvider;
    }

    public override void Tick()
    {
      if (_magazine.IsEmpty)
      {
        Enter<CompanionLowWeaponState>();
        return;
      }

      float maxDistance = DevConfig.MaxCompanionFollowDistance;

      if (!_playerProvider.Instance)
        return;

      if (!_playerProvider.Instance.Transform)
        return;

      if (Vector3.Distance(_transform.position, _playerProvider.Instance.Transform.position) > maxDistance)
        Enter<CompanionFollowPlayerState>();
    }
  }
}