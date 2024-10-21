using UnityAssetsTools.ParticleImage.Runtime;
using UnityEngine;

namespace HeadsUpDisplays.MoneyAttractions
{
  public class ParticleEndChecker : MonoBehaviour
  {
    [SerializeField] private ParticleImage _particleImage;

    private void OnEnable()
    {
      _particleImage.onParticleFinish.AddListener(Test);
    }

    void Test()
    {
    }
  }
}