using Buttons;
using Core.Characters.Players;
using Meta.Currencies;
using Zenject;

public class BuyHenButton : BaseButton
{
  [Inject] private PlayerProvider _playerProvider;
  [Inject] private CurrencyStorage _currencyStorage;

  private void Start()
  {
    Button.onClick.AddListener(Buy);
  }

  private void Buy()
  {
    throw new System.NotImplementedException();
    
    //_playerProvider.Instance.HenSpawner.Count++;
  }
}