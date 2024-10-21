using Windows;
using Meta.Currencies;
using Zenject;

public class FullOpenChestWindow : Window
{
  [Inject] private CurrencyStorage _currencyStorage;

  public override void Initialize()
  {
    _currencyStorage.Get(CurrencyId.Key).Value--;
  //  _currencyStorage.Get(CurrencyId.Chest).Value--;
  }

  protected override void SubscribeUpdates()
  {
  }

  protected override void Cleanup()
  {
  }
}