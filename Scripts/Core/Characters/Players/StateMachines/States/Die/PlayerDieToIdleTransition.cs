using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Players.States
{
  public class PlayerDieToIdleTransition : Transition
  {
    private readonly PlayerHealth _health;

    public PlayerDieToIdleTransition(PlayerHealth health)
    {
      _health = health;
    }

    public override void Tick()
    {
      if (_health.Current.Value > 0)
        Enter<PlayerIdleState>();
    }
  }
}