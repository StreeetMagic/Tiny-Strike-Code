using System;
using UnityEngine;

namespace Meta.Currencies
{
  [Serializable]
  public class CurrencyArtSetup : ArtSetup<CurrencyId>
  {
    public Sprite Sprite;
  }
}