using Windows;

public class FullChestWindow : Window
{
  // public GameObject AlertCounter;
  //
  // [Inject] private CurrencyStorage _currencyStorage;

  public override void Initialize()
  {
    // bool canOpen = _currencyStorage.Get(CurrencyId.Key).Value >= 1 && _currencyStorage.Get(CurrencyId.Chest).Value >= 1;
    //
    // AlertCounter.SetActive(canOpen);
  }

  protected override void SubscribeUpdates()
  {
  }

  protected override void Cleanup()
  {
  }
}