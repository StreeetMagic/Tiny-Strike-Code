using Meta.Configs;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players.GroundPointers
{
  public class GroundSimpleQuestPointer : MonoBehaviour
  {
    public GroundSimpleQuestPointerHider Hider;
    public GroundSimpleQuestPointerToTargetRotator Rotator;
    
    [Inject] public SimpleQuestConfig Config { get; private set; }
  }
}