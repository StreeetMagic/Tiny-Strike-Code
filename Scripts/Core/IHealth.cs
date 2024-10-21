using System;
using UnityEngine;
using Utilities;

namespace Core
{
  public interface IHealth
  {
    /// <summary>
    ///  int - expirience 
    ///  float - corpse remove delay 
    /// </summary>
    event Action<IHealth, int, float> Died;

    event Action<float> Damaged;

    ReactiveProperty<float> Current { get; }
    float Initial { get; }
    bool IsFull { get; }
    bool IsDead { get; }

    // ReSharper disable once InconsistentNaming
    Transform transform { get; }

    void NotifyOtherEnemies();
    void TakeDamage(float damage);
    void Hit();
    T GetComponent<T>();
  }
}