using AudioServices;
using AudioServices.Sounds;
using UnityEngine;
using Zenject;

namespace Core.Characters.Mentors
{
  public class GuidPopupSoundCntroller : MonoBehaviour
  {
    [Inject] private AudioService _audioService;
    
    private void OnEnable()
    {
      _audioService.Play(SoundId.TooltipPopup);  
    }
  }
}
