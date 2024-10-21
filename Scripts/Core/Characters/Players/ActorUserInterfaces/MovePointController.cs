using Inputs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Characters.Players
{
  public class MovePointController : MonoBehaviour
  {
    private const float Multiplier = -14f;
    
    public Image MovePoint;
    public RectTransform Transform;

    [Inject] private InputService _inputService;

    private void Update()
    {
      if (_inputService.HasMoveInput)
      {
        if (MovePoint.enabled == false)
        {
          MovePoint.enabled = true;
        }

        float directionLength = _inputService.MoveDirection.magnitude;
        
        Vector3 newPosition = Transform.localPosition;
        newPosition.y = directionLength * Multiplier; 
        Transform.localPosition = newPosition;
      }
      else
      {
        if (MovePoint.enabled)
        {
          MovePoint.enabled = false;
        }
      }
    }
  }
}