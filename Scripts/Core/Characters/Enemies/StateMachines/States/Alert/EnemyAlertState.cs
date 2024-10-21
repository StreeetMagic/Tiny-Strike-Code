using System.Collections.Generic;
using AudioServices;
using AudioServices.Sounds;
using ConfigProviders;
using Core.Characters.FiniteStateMachines;
using UnityEngine;
using VisualEffects;

namespace Core.Characters.Enemies.States.Alert
{
  public class EnemyAlertState : State
  {
    private readonly EnemyAlertTimer _alertTimer;
    private readonly EnemyAnimatorProvider _animatorProvider;
    private readonly EnemyConfig _config;
    private readonly EnemyVisualsProvider _configProvider;
    private readonly VisualEffectFactory _visualEffectFactory;
    private readonly Transform _transform;
    private readonly EnemyAlertPointProvider _alertPointProvider;
    private readonly AudioService _audioService;

    public EnemyAlertState(List<Transition> transitions, EnemyAlertTimer alertTimer,
      EnemyAnimatorProvider animatorProvider, EnemyConfig config, EnemyVisualsProvider configProvider,
      Transform transform, VisualEffectFactory visualEffectFactory, EnemyAlertPointProvider alertPointProvider,
      AudioService audioService)
      : base(transitions)
    {
      _alertTimer = alertTimer;
      _animatorProvider = animatorProvider;
      _config = config;
      _configProvider = configProvider;
      _transform = transform;
      _visualEffectFactory = visualEffectFactory;
      _alertPointProvider = alertPointProvider;
      _audioService = audioService;
    }

    public override void Enter()
    {
      _animatorProvider.Instance.PlayPanic(_config.AlertDuration);
      _audioService.Play(SoundId.AgroVoice);
      Vector3 panicPosition = _alertPointProvider.PointTransform.position;
      GameObject go = _visualEffectFactory.CreateAndDestroy(_configProvider.Panic(_config.Id), panicPosition, Quaternion.identity);
      go.transform.SetParent(_transform, true);

      _alertTimer.Set(_config.AlertDuration);
    }

    protected override void OnTick()
    {
      _alertTimer.Tick();
    }

    public override void Exit()
    {
    }
  }
}