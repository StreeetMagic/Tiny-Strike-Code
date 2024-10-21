using UnityEngine;
using Zenject;

namespace Core.Characters.Hens
{
  public class HenMover : MonoBehaviour
  {
    [Inject] private CharacterController _characterController;

    public void Move(Vector3 target, float moveSpeed)
    {
      Vector3 direction = (target - transform.position).normalized;
      _characterController.Move(direction * (moveSpeed * Time.deltaTime));
    }
  }
}