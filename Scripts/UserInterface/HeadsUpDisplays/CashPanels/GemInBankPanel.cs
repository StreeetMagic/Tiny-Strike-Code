using Meta.Currencies;
using TMPro;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.CashPanels
{
  public class GemInBankPanel : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _moneyInBankText;

    [Inject] private CurrencyStorage _currencyStorage;

    private void Start()
    {
      Set();

      _currencyStorage.Get(CurrencyId.Gem).ValueChanged += OnChanged;
    }

    private void OnDestroy()
    {
      _currencyStorage.Get(CurrencyId.Gem).ValueChanged -= OnChanged;
    }

    private void Set() =>
      _moneyInBankText.text = "" + _currencyStorage.Get(CurrencyId.Gem).Value;

    private void OnChanged(int obj) =>
      Set();
  }
}