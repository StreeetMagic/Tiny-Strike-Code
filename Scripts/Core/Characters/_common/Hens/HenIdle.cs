using Core.Characters.Hens.MeshModels;
using UnityEngine;
using Zenject;

namespace Core.Characters.Hens
{
  public class HenIdle : MonoBehaviour
  {
    [Inject] private HenAnimator _henAnimator;

    private void Awake()
    {
      enabled = false;
    }

    private void OnEnable()
    {
      _henAnimator.StopMovingAnimation();
    }
  }
}