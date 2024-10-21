using System;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class EnemyColliderDisabler : IInitializable, IDisposable
  {
    private readonly IHealth _health;
    private readonly Enemy _enemy;

    public EnemyColliderDisabler(IHealth health, Enemy enemy)
    {
      _health = health;
      _enemy = enemy;
    }

    public void Initialize()
    {
      _health.Died += DisableColliders;
    }

    public void Dispose()
    {
      _health.Died -= DisableColliders;
    }

    private void DisableColliders(IHealth enemyHealth, int expirity, float corpseRemoveDelay)
    {
      Collider[] colliders = _enemy.GetComponentsInChildren<Collider>();

      foreach (Collider thisCollider in colliders)
        thisCollider.enabled = false;
    }
  }
}