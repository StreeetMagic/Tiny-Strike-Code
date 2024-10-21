using System.Collections.Generic;
using ConfigProviders;
using DevConfigs;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerTargetHolder : ITickable
  {
    private readonly Transform _transform;
    private readonly PlayerWeaponIdProvider _playerWeaponIdProvider;
    private readonly BalanceConfigProvider _balanceConfigProvider;

    private readonly ITargetTrigger[] _targets = new ITargetTrigger[DevConfig.TargetOverlapColliders];
    private readonly ITargetTrigger[] _deadTargetsBuffer = new ITargetTrigger[DevConfig.TargetOverlapColliders];
    private readonly ITargetTrigger[] _farTargetsBuffer = new ITargetTrigger[DevConfig.TargetOverlapColliders];
    private readonly ITargetTrigger[] _targetsBuffer = new ITargetTrigger[DevConfig.TargetOverlapColliders];
    
    private int _targetCount;

    public PlayerTargetHolder(Transform transform, PlayerWeaponIdProvider playerWeaponIdProvider, BalanceConfigProvider balanceConfigProvider)
    {
      _transform = transform;
      _playerWeaponIdProvider = playerWeaponIdProvider;
      _balanceConfigProvider = balanceConfigProvider;
    }

    public bool HasTarget { get; private set; }
    public ITargetTrigger CurrentTarget { get; private set; }

    public void Tick()
    {
      ManageCurrentTarget();
      RemoveFarTargets();
      RemoveDeadTargets();

      if (CurrentTarget != null)
        CurrentTarget.IsTargeted = true;
    }

    public void AddTargets(ITargetTrigger target)
    {
      if (_targetCount < DevConfig.TargetOverlapColliders && !ContainsTarget(target))
        _targets[_targetCount++] = target;
    }

    public Vector3 LookDirectionToTarget()
      => new Vector3(CurrentTarget.transform.position.x, _transform.position.y, CurrentTarget.transform.position.z) - _transform.position;

    private void RemoveDeadTargets()
    {
      int deadTargetCount = 0;

      for (int i = 0; i < _targetCount; i++)
      {
        ITargetTrigger target = _targets[i];
        
        if (!target.Health.IsDead)
          continue;

        _deadTargetsBuffer[deadTargetCount++] = target;

        if (CurrentTarget == target)
          target.IsTargeted = false;
      }

      for (int i = 0; i < deadTargetCount; i++)
        RemoveTarget(_deadTargetsBuffer[i]);
    }

    private void RemoveFarTargets()
    {
      int farTargetCount = 0;
      float range = _balanceConfigProvider.Weapons[_playerWeaponIdProvider.CurrentId.Value].Range;

      for (int i = 0; i < _targetCount; i++)
      {
        ITargetTrigger target = _targets[i];
        if (!(Vector3.Distance(_transform.position, target.transform.position) > range))
          continue;

        _farTargetsBuffer[farTargetCount++] = target;

        if (CurrentTarget == target)
          target.IsTargeted = false;
      }

      for (int i = 0; i < farTargetCount; i++)
        RemoveTarget(_farTargetsBuffer[i]);
    }

    private void ManageCurrentTarget()
    {
      if (_targetCount == 0)
      {
        if (CurrentTarget != null)
          CurrentTarget.IsTargeted = false;

        HasTarget = false;
        CurrentTarget = null;
      }
      else
      {
        SetNearestCurrentTarget();
      }
    }

    private void SetNearestCurrentTarget()
    {
      int targetCount = GetTargets(_targetsBuffer);

      ITargetTrigger nearestTarget = null;
      float nearestDistance = float.MaxValue;

      for (int i = 0; i < targetCount; i++)
      {
        ITargetTrigger target = _targetsBuffer[i];
        float distance = Vector3.Distance(_transform.position, target.transform.position);

        if (distance > nearestDistance)
          continue;

        nearestDistance = distance;
        nearestTarget = target;
      }

      if (CurrentTarget != null)
      {
        if (CurrentTarget != nearestTarget)
          CurrentTarget.IsTargeted = false;
      }

      CurrentTarget = nearestTarget;

      if (CurrentTarget != null)
        CurrentTarget.IsTargeted = true;

      HasTarget = true;
    }

    private int GetTargets(ITargetTrigger[] buffer)
    {
      int targetCount = 0;
      TargetPriority priority = TargetPriority.Low;

      Dictionary<TargetPriority, int> map = new Dictionary<TargetPriority, int>
      {
        { TargetPriority.High, 100 },
        { TargetPriority.Medium, 10 },
        { TargetPriority.Low, 0 }
      };

      for (int i = 0; i < _targetCount; i++)
      {
        ITargetTrigger target = _targets[i];
        
        if (target.TargetPriority == priority)
          buffer[targetCount++] = target;

        if (map[target.TargetPriority] <= map[priority])
          continue;

        targetCount = 0;
        buffer[targetCount++] = target;
        priority = target.TargetPriority;
      }

      return targetCount;
    }

    private bool ContainsTarget(ITargetTrigger target)
    {
      for (int i = 0; i < _targetCount; i++)
        if (_targets[i] == target)
          return true;
      
      return false;
    }

    private void RemoveTarget(ITargetTrigger target)
    {
      int index = -1;
      
      for (int i = 0; i < _targetCount; i++)
      {
        if (_targets[i] != target)
          continue;

        index = i;
        break;
      }

      if (index < 0)
        return;

      _targetCount--;
      _targets[index] = _targets[_targetCount];
      _targets[_targetCount] = null;
    }
  }
}
