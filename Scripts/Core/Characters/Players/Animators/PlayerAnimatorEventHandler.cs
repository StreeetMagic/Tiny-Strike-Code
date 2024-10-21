using System;
using UnityEngine;

namespace Core.Characters.Players
{
  public class PlayerAnimatorEventHandler : MonoBehaviour
  {
    public event Action Shot;

    public void Shoot()
    {
      Shot?.Invoke();
    }
  }
}