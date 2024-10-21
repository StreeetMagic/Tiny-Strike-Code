using Meta;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Characters.Players.GroundPointers
{
  public class GroundSimpleQuestPointerToTargetRotator : GroundQuestPointerToTargetRotator
  {
    // ReSharper disable once InconsistentNaming
    [FormerlySerializedAs("SimpleQuestPointer")]
    public GroundSimpleQuestPointer groundSimpleQuestPointer;

    [Inject]
    private SimpleQuestTargetsProvider _targetProvider;

    protected override void GetTargetsOrNull()
    {
      Targets = _targetProvider.GetTargetsOrNull(groundSimpleQuestPointer.Config.Id);
    }
  }
}