using System.Collections.Generic;
using Meta.Currencies;
using UnityEngine;

namespace Meta.Loots
{
  [CreateAssetMenu(fileName = "Loot", menuName = "Configs/Loot")]
  public class LootConfig : Config<CurrencyId>
  {
    public List<Loot> Loots;
  }
}