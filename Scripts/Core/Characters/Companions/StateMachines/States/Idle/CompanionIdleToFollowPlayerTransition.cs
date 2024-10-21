using Core.Characters.FiniteStateMachines;
using DevConfigs;
using UnityEngine;

namespace Core.Characters.Companions.States
{
  public class CompanionIdleToFollowPlayerTransition : Transition
  {
    private readonly Companion _companion;
    private readonly Transform _transform;

    public CompanionIdleToFollowPlayerTransition(Companion companion, Transform transform)
    {
      _companion = companion;
      _transform = transform;
    }

    public override void Tick()
    {
      float maxDistance = DevConfig.MaxCompanionFollowDistance;

      if (Vector3.Distance(_transform.position, _companion.Installer.TransformContainer.Transform.position) > maxDistance)
      {
        Enter<CompanionFollowPlayerState>();
      }
    }
  }
}