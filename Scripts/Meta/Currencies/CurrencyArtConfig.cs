using Loggers;
using UnityEngine;

namespace Meta.Currencies
{
  [CreateAssetMenu(fileName = "Currency", menuName = "ArtConfigs/Currency")]
  public class CurrencyArtConfig : ArtConfig<CurrencyArtSetup>
  {
    protected override void Validate()
    {
      foreach (CurrencyArtSetup art in Setups)
      {
        if (!art.Sprite)
          new DebugLogger().LogError("Icon in " + nameof(CurrencyArtConfig) + " with ID " + art.Id + " is null");
      }
    }
  }
}