using System.Collections.Generic;
using UnityEngine;

namespace Core.Projectiles.Scripts
{
  public class ProjectileStorage
  {
    private Dictionary<string, PlayerProjectile> _projectiles = new();

    public void Add(string guid, PlayerProjectile playerProjectile)
    {
      _projectiles.Add(guid, playerProjectile);
    }

    public void Remove(string guid)
    {
      if (!_projectiles.ContainsKey(guid))
      {
        Debug.LogError($"Projectile with guid {guid} not found");
        return;
      }

      _projectiles.Remove(guid);
    }

    public PlayerProjectile Get(string guid)
    {
      if (_projectiles.TryGetValue(guid, out PlayerProjectile projectile))
        return projectile;

      Debug.LogError($"Projectile with guid {guid} not found");
      return null;
    }
  }
}