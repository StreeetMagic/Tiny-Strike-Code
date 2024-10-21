using Core.Characters.FiniteStateMachines;
using DevConfigs;
using UnityEngine;

namespace Core.Characters.Companions.States
{
  public class CompanionFollowPlayerToIdleTransition : Transition
  {
    private readonly Companion _companion;
    private readonly Transform _transform;

    public CompanionFollowPlayerToIdleTransition(Companion companion, Transform transform)
    {
      _companion = companion;
      _transform = transform;
    }

    public override void Tick()
    {
      float minDistance = DevConfig.MaxCompanionFollowDistance;

      if (Vector3.Distance(_transform.position, _companion.Installer.TransformContainer.Transform.position) < minDistance)
      {
        Enter<CompanionIdleState>();
      }
    }
  }
}