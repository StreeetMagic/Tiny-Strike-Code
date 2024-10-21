using Meta.Currencies;
using TMPro;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.CashPanels
{
  public class KeysInBankPanel : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _keysInBankText;

    [Inject]
    private CurrencyStorage _currencyStorage;

    private void Start()
    {
      SetKeysInBank();

      _currencyStorage.Get(CurrencyId.Key).ValueChanged += OnKeyInBankValueChanged;
    }

    private void OnDestroy()
    {
      _currencyStorage.Get(CurrencyId.Key).ValueChanged -= OnKeyInBankValueChanged;
    }

    private void SetKeysInBank() =>
      _keysInBankText.text = "" + _currencyStorage.Get(CurrencyId.Key).Value;

    private void OnKeyInBankValueChanged(int obj) =>
      SetKeysInBank();
  }
}