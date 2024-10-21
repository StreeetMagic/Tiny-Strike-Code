namespace Core.Characters.Companions
{
  public class CompanionWeaponMagazine
  {
    private readonly Companion _companion;

    private int _bulletsInMagazine;

    public CompanionWeaponMagazine(Companion companion)
    {
      _companion = companion;
    }

    public bool IsEmpty => _bulletsInMagazine == 0;

    public void Reload()
    {
      _bulletsInMagazine = _companion.Installer.Config.MagazineCapacity;
    }

    public bool TryGetBullet()
    {
      if (_bulletsInMagazine > 0)
      {
        _bulletsInMagazine--;
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}