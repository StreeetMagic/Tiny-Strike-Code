using Core.Characters.Hens.MeshModels;
using Core.Characters.Players;
using Meta.Stats;
using UnityEngine;
using Zenject;

namespace Core.Characters.Hens
{
  public class HenToTargetFollower : MonoBehaviour
  {
    [Inject] private PlayerProvider _playerProvider;
    [Inject] private HenMover _henMover;
    [Inject] private PlayerStatsProvider _playerStatsProvider;
    [Inject] private HenRotator _henRotator;
    [Inject] private HenAnimator _henAnimator;

    private float _timeLeft;

    private Transform Target => _playerProvider.Instance.TargetHolder.CurrentTarget.transform;
    private float MoveSpeed => _playerStatsProvider.GetStat(StatId.MoveSpeed);

    private void Awake()
    {
      enabled = false;
    }

    private void OnEnable()
    {
      var delay = _henAnimator.PlayAlarmAnimation();

      _timeLeft = delay;
    }

    private void Update()
    {
      _timeLeft -= Time.deltaTime;

      if (_timeLeft <= 0)
      {
        if (_playerProvider.Instance.TargetHolder.CurrentTarget != null)
        {
          _henMover.Move(Target.position, MoveSpeed);
          _henRotator.Rotate(Target.position);
        }
      }
    }
  }
}