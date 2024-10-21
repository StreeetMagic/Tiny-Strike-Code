using System.Collections.Generic;
using Meta.Currencies;
using UnityEngine;

namespace PersistentProgresses
{
  [CreateAssetMenu(fileName = "Project", menuName = "Configs/Project")]
  public class ProjectConfig : ScriptableObject
  {
    public List<CurrencyProgress> Currencies;

    [Tooltip("Начальное количество опыта")] 
    [Space]
    public int Expierience;
  }
}