using UnityEngine;
using VisualEffects;
using Zenject;

namespace Core.Characters.Hens
{
  public class HenVisualEffector : MonoBehaviour
  {
    [Inject] private VisualEffectFactory _visualEffectFactory;

    public void PlayExplosion()
    {
      _visualEffectFactory.CreateAndDestroy(VisualEffectId.HenExplosion, transform.position, Quaternion.identity);
    }
  }
}