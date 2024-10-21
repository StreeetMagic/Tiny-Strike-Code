using System;
using UnityEngine;

namespace Core.Characters.Players.GroundPointers
{
  public abstract class GroundQuestPointerToTargetRotator : MonoBehaviour
  {
    protected Transform[] Targets = Array.Empty<Transform>();
    
    public Transform CurrentTarget { get; private set; }
    
    private void LateUpdate()
    {
      Clear(); 
      GetTargetsOrNull();
      RotateTo();
    }

    protected abstract void GetTargetsOrNull();

    private void SetClosestTarget()
    {
      Transform closestTarget = null;
      float minDistance = float.MaxValue;

      foreach (Transform target in Targets)
      {
        if (!target)
          continue;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < minDistance)
        {
          minDistance = distance;
          closestTarget = target;
        }
      }

      CurrentTarget = closestTarget;
    }

    private void Clear()
    {
      for (int i = 0; i < Targets.Length; i++)
        Targets[i] = null;
    }

    private void RotateTo()
    {
      SetClosestTarget();

      if (!CurrentTarget)
        return;

      transform.rotation = Quaternion.LookRotation(transform.position - CurrentTarget.transform.position);

      Quaternion cachedRotation = transform.rotation;

      transform.rotation = new Quaternion(0, cachedRotation.y, 0, cachedRotation.w);
    }
  }
}