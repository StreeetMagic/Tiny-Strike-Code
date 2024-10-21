using System;
using UnityEngine;

namespace Core.Weapons
{
  [Serializable]
  public class WeaponSetup
  {
    public WeaponId WeaponTypeId;
    public GameObject GameObject;
    public Transform ShootingPoint;
  }
}