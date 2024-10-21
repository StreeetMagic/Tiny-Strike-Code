using System.Collections.Generic;
using Core.Characters;
using Core.Weapons;
using UnityEngine;

namespace Core
{
  public interface ITargetTrigger
  {
    IHealth Health { get; }
    bool IsTargeted { get; set; }
    bool IsTargetable { get; set; }
    // ReSharper disable once InconsistentNaming
    Transform transform { get; }
    TargetPriority TargetPriority { get; }
    Transform TargetPoint { get; }
    List<WeaponId> WeaponWhiteList { get; }
    
    void TakeDamage(float damage);
    void Hit();
  }
}