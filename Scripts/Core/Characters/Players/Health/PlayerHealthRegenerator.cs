using Meta.Stats;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerHealthRegenerator : ITickable
  {
    private readonly PlayerStatsProvider _playerStatsProvider;
    private readonly PlayerHealth _playerHealth;

    public PlayerHealthRegenerator(PlayerStatsProvider playerStatsProvider, PlayerHealth playerHealth)
    {
      _playerStatsProvider = playerStatsProvider;
      _playerHealth = playerHealth;
    }

    public void Tick()
    {
      if (_playerHealth.Current.Value >= _playerStatsProvider.GetStat(StatId.Health))
        return;

      if (_playerHealth.Current.Value == 0)
        return;

      _playerHealth.HealTick();
    }
  }
}