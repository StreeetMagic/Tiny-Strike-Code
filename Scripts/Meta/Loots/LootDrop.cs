using System;
using Meta.Currencies;

namespace Meta.Loots
{
  [Serializable]
  public class LootDrop
  {
    public CurrencyId Id;
    public int Level;
  }
}