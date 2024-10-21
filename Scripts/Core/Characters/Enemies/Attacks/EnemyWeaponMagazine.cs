namespace Core.Characters.Enemies
{
  public class EnemyWeaponMagazine
  {
    private readonly EnemyConfig _config;
    
    private int _bulletsInMagazine;

    public EnemyWeaponMagazine(EnemyConfig config)
    {
      _config = config;
      Reload();
    }
    
    public bool IsEmpty => _bulletsInMagazine == 0;

    public void Reload()
    {
      _bulletsInMagazine = _config.MagazineCapacity;
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