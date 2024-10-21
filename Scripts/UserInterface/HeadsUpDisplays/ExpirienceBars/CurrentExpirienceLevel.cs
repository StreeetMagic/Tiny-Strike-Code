using ConfigProviders;
using Meta.Expirience;
using TMPro;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.ExpirienceBars
{
  public class CurrentExpirienceLevel : MonoBehaviour
  {
    public TextMeshProUGUI Text;

    private ExpierienceStorage _expierienceStorage;

    [Inject]
    public void Construct(ExpierienceStorage expierienceStorage, BalanceConfigProvider balanceConfigProvider)
    {
      _expierienceStorage = expierienceStorage;
    }

    private void OnEnable()
    {
      SetText(_expierienceStorage.CurrentLevel());
      _expierienceStorage.AllPoints.ValueChanged += SetText;
    }

    private void OnDisable()
    {
      _expierienceStorage.AllPoints.ValueChanged -= SetText;
    }

    private void SetText(int value)
    {
      Text.text = _expierienceStorage.CurrentLevel().ToString();
    }
  }
}