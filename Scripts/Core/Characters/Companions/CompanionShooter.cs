using Core.Characters.Companions.Configs;
using Core.Projectiles.Scripts;
using UnityEngine;
using Utilities;

namespace Core.Characters.Companions
{
  public class CompanionShooter
  {
    private readonly ProjectileFactory _projectileFactory;

    public CompanionShooter(ProjectileFactory projectileFactory)
    {
      _projectileFactory = projectileFactory;
    }

    public void Shoot(Transform parent, Vector3 startPosition, Vector3 directionToTarget, CompanionConfig companionConfig)
    {
      for (int i = 0; i < companionConfig.BulletsPerShot; i++)
        _projectileFactory.CreateCompanionProjectile(parent, startPosition, directionToTarget.AddAngle(companionConfig.BulletSpreadAngle), companionConfig);
    }
  }
}