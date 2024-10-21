using System;
using AudioServices;
using AudioServices.Sounds;
using Core.Characters.Movers;
using Meta.Stats;
using UnityEngine;
using Utilities;

namespace Core.Characters.Players
{
  public class PlayerHealth
  {
    private readonly PlayerStatsProvider _playerStatsProvider;
    private readonly AudioService _audioService;
    private readonly PlayerRespawnPosition _playerRespawnPosition;
    private readonly IMover _navMeshMover;

    public PlayerHealth(PlayerStatsProvider playerStatsProvider, AudioService audioService, PlayerRespawnPosition playerRespawnPosition,
      IMover navMeshMover)
    {
      _playerStatsProvider = playerStatsProvider;
      _audioService = audioService;
      _playerRespawnPosition = playerRespawnPosition;
      _navMeshMover = navMeshMover;
      Current.Value = playerStatsProvider.GetStat(StatId.Health);
    }

    public event Action Died;
    public event Action<float> Damaged;

    public ReactiveProperty<float> Current { get; } = new();
    public bool IsDead { get; private set; }
    public bool IsImmortal { get; set; }

    public void TakeDamage(float damage)
    {
      if (damage <= 0)
        throw new ArgumentOutOfRangeException(nameof(damage));

      _audioService.Play(SoundId.HitMarker);

      // GameObject go = _hubZenjectFactory.InstantiatePrefab(_devConfigProvider.GetPrefab(PrefabId.HitPopupNumber), _transform.position, Quaternion.identity, null);
      // go.GetComponent<DamageNumberMesh>().number = damage;

      Current.Value -= damage;

      Damaged?.Invoke(damage);
    }

    public void HealMax()
    {
      Current.Value = _playerStatsProvider.GetStat(StatId.Health);
    }

    public void HealTick()
    {
      float regenPerSecond = _playerStatsProvider.GetStat(StatId.HealthRegeneration);

      Current.Value += regenPerSecond * Time.deltaTime;

      if (Current.Value > _playerStatsProvider.GetStat(StatId.Health))
        Current.Value = _playerStatsProvider.GetStat(StatId.Health);
    }

    public void Die()
    {
      if (IsDead)
        return;

      Current.Value = 0;

      IsDead = true;
      Died?.Invoke();
    }

    public void Revive()
    {
      IsDead = false;

      Warp();
    }

    private void Warp()
    {
      _navMeshMover.Warp(_playerRespawnPosition.Position());
    }
  }
}