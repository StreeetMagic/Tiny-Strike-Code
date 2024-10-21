using System.Collections.Generic;
using Meta.Currencies;
using Meta.Stats;
using UnityEngine;

namespace Meta.Upgrades
{
  [CreateAssetMenu(fileName = "Upgrade", menuName = "Configs/Upgrade")]
  public class UpgradeConfig : Config<StatId>
  {
    [Space] 
    [Tooltip("Валюта покупки")] 
    public CurrencyId CurrencyId;
    
    [Space] 
    [Tooltip("Значения апгрейда")] 
    public List<UpgradeSetup> Setups;
  }
}