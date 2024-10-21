using System;
using Meta;
using UnityEngine;

namespace HeadsUpDisplays.ScreenPointers
{
  public abstract class ScreenQuestPointer : ScreenPointer
  {
    public GameObject Exclamation;
    public GameObject Question;
    
    protected Transform[] Targets = Array.Empty<Transform>();

    public Transform CurrentTarget { get; private set; }

    protected void SwitchMarks(QuestState questState)
    {
      bool rewardReady = questState == QuestState.RewardReady;

      if (rewardReady)
      {
        if (!Exclamation.activeSelf)
          Exclamation.SetActive(true);

        if (Question.activeSelf)
          Question.SetActive(false);
      }
      else
      {
        if (Question.activeSelf)
          Exclamation.SetActive(false);

        if (!Exclamation.activeSelf)
          Question.SetActive(true);
      }
    }

    protected void SetClosestTarget()
    {
      Transform closestTarget = null;
      float minDistance = float.MaxValue;

      foreach (Transform target in Targets)
      {
        if (!target)
          continue;

        float distance = Vector3.Distance(PlayerProvider.Instance.Transform.position, target.position);

        if (distance < minDistance)
        {
          minDistance = distance;
          closestTarget = target;
        }
      }

      CurrentTarget = closestTarget;
    }

    protected void Clear()
    {
      for (int i = 0; i < Targets.Length; i++)
        Targets[i] = null;
    }
  }
}