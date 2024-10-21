using Meta.Currencies;
using TMPro;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.CashPanels
{
  public class MoneyInBankPanel : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _moneyInBankText;

    [Inject] private CurrencyStorage _currencyStorage;

    private void Start()
    {
      SetMoneyInBank();

      _currencyStorage.Get(CurrencyId.Money).ValueChanged += OnMoneyInBankValueChanged;
    }

    private void OnDestroy()
    {
      _currencyStorage.Get(CurrencyId.Money).ValueChanged -= OnMoneyInBankValueChanged;
    }

    private void SetMoneyInBank() =>
      _moneyInBankText.text = "" + _currencyStorage.Get(CurrencyId.Money).Value;

    private void OnMoneyInBankValueChanged(int obj) =>
      SetMoneyInBank();
  }
}