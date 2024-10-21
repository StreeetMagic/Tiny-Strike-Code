using System;

namespace Meta.Currencies
{
  [Serializable]
  public class CurrencyProgress
  {
    public CurrencyId Id;
    public int Count;
    
    public CurrencyProgress(CurrencyId id, int count)
    {
      Id = id;
      Count = count;
    }
  }
}