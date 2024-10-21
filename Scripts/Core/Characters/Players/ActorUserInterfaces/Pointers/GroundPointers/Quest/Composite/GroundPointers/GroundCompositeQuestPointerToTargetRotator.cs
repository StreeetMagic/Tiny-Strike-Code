using Meta;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Characters.Players.GroundPointers
{
  public class GroundCompositeQuestPointerToTargetRotator : GroundQuestPointerToTargetRotator
  {
    // ReSharper disable once InconsistentNaming
    [FormerlySerializedAs("CompositeQuestPointer")]
    public GroundCompositeQuestPointer groundCompositeQuestPointer;

    [Inject]
    private CompositeQuestTargetsProvider _targetProvider;

    protected override void GetTargetsOrNull()
    {
      Targets = _targetProvider.GetTargetsOrNull(groundCompositeQuestPointer.Config.Id);
    }
  }
}