using AudioServices;
using ConfigProviders;
using Core.Characters.Players;

namespace Core.Characters.Enemies
{
  public class EnemyMeleeAttacker
  {
    private readonly PlayerProvider _playerProvider;
    private readonly EnemyConfig _config;
    private readonly EnemyVisualsProvider _enemyVisualsProvider;
    private readonly AudioService _audioService;
    private readonly EnemyConfig _enemyConfig;

    public EnemyMeleeAttacker(PlayerProvider playerProvider, EnemyConfig config,
      EnemyVisualsProvider enemyVisualsProvider, AudioService audioService, EnemyConfig enemyConfig)
    {
      _playerProvider = playerProvider;
      _config = config;
      _enemyVisualsProvider = enemyVisualsProvider;
      _audioService = audioService;
      _enemyConfig = enemyConfig;
    }

    public void Attack()
    {
      _playerProvider.Instance.Health.TakeDamage(_config.MeeleAttackDamage);
      _audioService.Play(_enemyVisualsProvider.AttackSound(_enemyConfig.Id));
    }
  }
}