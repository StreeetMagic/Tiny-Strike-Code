using System.Collections.Generic;
using UnityEngine;

namespace Meta.Expirience
{
  [CreateAssetMenu(menuName = "Configs/Expirience", fileName = "Expirience")]
  public class ExpirienceConfig : ScriptableObject
  {
    [Header("Это настройки количества опыта для получения следующего уровня")]
    public List<ExpirienceSetup> Levels;
  }
}