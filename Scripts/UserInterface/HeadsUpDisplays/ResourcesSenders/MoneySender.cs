using UnityAssetsTools.ParticleImage.Runtime;
using UnityEngine;

namespace HeadsUpDisplays.ResourcesSenders
{
  public class MoneySender : MonoBehaviour
  {
    public ParticleImage ParticleImage;
  //  public RectTransform StartPosition;
    public RectTransform TargetTransform;

    public void PlayParticle()
    {
      ParticleImage.enabled = true;
      
      //  ParticleImage.main.rateOverTime = amount;
      ParticleImage.main.attractorTarget = TargetTransform;

      ParticleImage.Play();
    }
  }
}