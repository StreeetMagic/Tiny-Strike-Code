using ConfigProviders;
using Core.Characters.Questers;
using LevelDesign.Maps;
using Meta;
using Meta.Stats;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerHostageHolder : ITickable
  {
    [Inject] private PlayerHostageHolderPoint _hostageHolderPoint;
    [Inject] private MapProvider _mapProvider;
    [Inject] private Transform _playerTransform;
    [Inject] private SimpleQuestStorage _simpleQuestStorage;
    [Inject] private PlayerHealth _playerHealth;
    [Inject] private BalanceConfigProvider _balanceConfigProvider;
    [Inject] private PlayerStatsProvider _playerStatsProvider;

    public Hostage Hostage { get; private set; }
    public bool HasHostage;

    public void Tick()
    {
      if (!Hostage)
        return;

      ReleaseTick();
      ReturnTick();
    }

    public void PickUp(Hostage hostage)
    {
      HasHostage = true;
      Hostage = hostage;

      Hostage.transform.parent = _hostageHolderPoint.transform;
      Hostage.transform.localPosition = Vector3.zero;
      Hostage.transform.localRotation = Quaternion.identity;

      _playerStatsProvider.SetStatusMultiplier(StatId.MoveSpeed, _balanceConfigProvider.Player.HostageMoveSlowMultiplier);
    }

    private void ReleaseTick()
    {
      SimpleQuestId questId = Hostage.SpawnMarker.QuestId;
      SimpleQuester simpleQuester = _mapProvider.Map.GetSimpleQuesterOrNull(questId);
      float distance = Vector3.Distance(simpleQuester.transform.position, _playerTransform.position);
      bool isClose = distance <= simpleQuester.DistanceToPlayer;

      if (simpleQuester & isClose)
        Release(questId);
    }

    private void ReturnTick()
    {
      if (_playerHealth.IsDead && Hostage)
        ReturnToSpawn();
    }

    private void ReturnToSpawn()
    {
      Hostage.ResetProgress();
      Hostage.transform.parent = Hostage.SpawnMarker.transform;
      Hostage.transform.localPosition = Vector3.zero;
      Hostage.transform.localRotation = Quaternion.identity;

      ReleaseHostage();
    }

    private void Release(SimpleQuestId questId)
    {
      Object.Destroy(Hostage.gameObject);
      ReleaseHostage();

      _simpleQuestStorage.Get(questId).CompletedQuantity.Value++;
    }

    private void ReleaseHostage()
    {
      HasHostage = false;
      Hostage = null;
      _playerStatsProvider.ClearStatusMultiplier(StatId.MoveSpeed);
    }
  }
}