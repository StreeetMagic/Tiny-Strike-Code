using System;
using Meta.Currencies;

namespace Meta.Rewards
{
  [Serializable]
  public class CurrencyReward
  {
    public CurrencyId Id;
    public int Quantity;

    public CurrencyReward(CurrencyId id, int quantity)
    {
      Id = id;
      Quantity = quantity;
    }
  }
}