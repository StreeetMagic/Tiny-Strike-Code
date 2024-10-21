using Core.Weapons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Characters.Players
{
  public class ReloadWeaponSliderController : MonoBehaviour
  {
    public Image Slider;
    public Image Shape;

    [Inject] private PlayerProvider _playerProvider;

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;
      
      PlayerWeaponMagazineReloader reloader = _playerProvider.Instance.PlayerWeaponMagazineReloader;

      if (!reloader.IsActive)
      {
        Slider.fillAmount = 0;
        Shape.enabled = false;
        return;
      }

      if (_playerProvider.Instance.WeaponIdProvider.CurrentId.Value is WeaponId.Unknown or WeaponId.Knife or WeaponId.Unarmed)
        return;

      Shape.enabled = true;
      Slider.fillAmount = reloader.TimePassed / reloader.FullTime;
    }
  }
}