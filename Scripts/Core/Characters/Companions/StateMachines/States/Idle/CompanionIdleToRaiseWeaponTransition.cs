using Core.Characters.Companions.States.RaiseWeapon;
using Core.Characters.FiniteStateMachines;
using Core.Characters.Players;

namespace Core.Characters.Companions.States
{
  public class CompanionIdleToRaiseWeaponTransition : Transition
  {
    private readonly PlayerProvider _playerProvider;

    public CompanionIdleToRaiseWeaponTransition(PlayerProvider playerProvider)
    {
      _playerProvider = playerProvider;
    }

    public override void Tick()
    {
      if (_playerProvider.Instance.WeaponAttacker.IsAttacking)
      {
        Enter<CompanionRaiseWeaponState>();
      }
    }
  }
}