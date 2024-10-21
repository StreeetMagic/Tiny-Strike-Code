using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Doors
{
    [SelectionBase]
    public class DoorAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private const string Close = "Close";
        private const string Open = "Open";

        private static readonly int s_close = Animator.StringToHash(Close);
        private static readonly int s_open = Animator.StringToHash(Open);

        [Button]
        public void PlayOpenDoor()
        {
            _animator.SetTrigger(s_open);
        }
        
        [Button]
        public void PlayCloseDoor()
        {
            _animator.SetTrigger(s_close);
        }
    }
}
