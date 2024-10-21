using Meta;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.GroundPointers
{
  public class GroundCompositeQuestPointer : MonoBehaviour
  {
    public GroundCompositeQuestPointerHider Hider;
    public GroundCompositeQuestPointerToTargetRotator Rotator;
    
    [Inject] public CompositeQuestConfig Config { get; private set; }
  }
}