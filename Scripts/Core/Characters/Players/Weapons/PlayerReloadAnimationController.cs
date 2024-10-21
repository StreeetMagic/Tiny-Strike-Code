using Zenject;

namespace Core.Characters.Players
{
  public class PlayerReloadAnimationController : ITickable
  {
    private readonly PlayerWeaponMagazineReloader _reloader;
    private readonly PlayerAnimatorController _playerAnimator;

    private bool _isReloading;

    public PlayerReloadAnimationController(PlayerWeaponMagazineReloader reloader, PlayerAnimatorController playerAnimator)
    {
      _reloader = reloader;
      _playerAnimator = playerAnimator;
    }

    public void Tick()
    {
      if (_reloader.IsActive && _isReloading == false)
      {
        _playerAnimator.OffStateShooting();
        _playerAnimator.PlayReload();
        _isReloading = true;
      }
      else if (_reloader.IsActive == false && _isReloading)
      {
        _playerAnimator.StopReload();
        _isReloading = false;
      }
    }
  }
}