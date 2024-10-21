using System.Collections.Generic;
using AudioServices;
using ConfigProviders;
using Core.Characters.FiniteStateMachines;

namespace Core.Characters.Enemies.States.Reload
{
  public class EnemyReloadState : State
  {
    private readonly EnemyWeaponMagazine _magazine;
    private readonly EnemyWeaponMagazineReloaderTimer _timer;
    private readonly EnemyAnimatorProvider _animator;
    private readonly EnemyVisualsProvider _enemyVisualsProvider;
    private readonly AudioService _audioService;
    private readonly EnemyConfig _enemyConfig;

    public EnemyReloadState(List<Transition> transitions,
      EnemyWeaponMagazine magazine,
      EnemyWeaponMagazineReloaderTimer timer, EnemyAnimatorProvider animator,
      EnemyVisualsProvider enemyVisualsProvider, AudioService audioService, EnemyConfig enemyConfig) : base(transitions)
    {
      _magazine = magazine;
      _timer = timer;
      _animator = animator;
      _enemyVisualsProvider = enemyVisualsProvider;
      _audioService = audioService;
      _enemyConfig = enemyConfig;
    }

    public override void Enter()
    {
      _animator.Instance.PlayReload();
      _audioService.Play(_enemyVisualsProvider.ReloadSound(_enemyConfig.Id));
      _timer.Set(_enemyConfig.MagazineReloadTime);
    }

    protected override void OnTick()
    {
      if (_timer.IsCompleted)
      {
        _magazine.Reload();
        return;
      }

      _timer.Tick();
    }

    public override void Exit()
    {
    }
  }
}