using UnityAssetsTools.ParticleImage.Runtime;
using UnityEngine;

namespace HeadsUpDisplays.ResourcesSenders
{
  public class EggSender : MonoBehaviour
  {
    public ParticleImage ParticleImage;
    public RectTransform StartPosition;
    public RectTransform TargetTransform;

    public void PlayParticle(int amount, Vector3 targetPosition)
    {
      TargetTransform.position = targetPosition;
    
      amount /= 2;

      ParticleImage.main.rateOverTime = amount;
      ParticleImage.main.attractorTarget = TargetTransform;

      ParticleImage.Play();
    }
  }
}