using UnityEngine;

namespace Core.Grenades
{
  [CreateAssetMenu(fileName = "Grenade", menuName = "Configs/Grenade")]
  public class GrenadeConfig : Config<GrenadeTypeId>
  {
    [Tooltip("Время полета от запуска до приземления")]
    public float FlightTime = 1f;
    
    [Tooltip("Время детонации после приземления")]
    public float DetonationTime = 1f;
    
    [Tooltip("Радиус детонации")]
    public float DetonationRadius = 1f;
    
    [Tooltip("Урон")]
    public float Damage = 50;
  }
}