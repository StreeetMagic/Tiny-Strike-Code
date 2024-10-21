using ConfigProviders;
using Meta.Expirience;
using TMPro;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.ExpirienceBars
{
  public class ExpirienceText : MonoBehaviour
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
      SetText(_expierienceStorage.CurrentExpierience());
      _expierienceStorage.AllPoints.ValueChanged += SetText;
    }

    private void OnDisable()
    {
      _expierienceStorage.AllPoints.ValueChanged -= SetText;
    }

    private void SetText(int value)
    {
      int currentExpierience = _expierienceStorage.CurrentExpierience();
      int expierienceToNextLevel = _expierienceStorage.ExpierienceToNextLevel();

      Text.text = $"{currentExpierience}/{expierienceToNextLevel}";
    }
  }
}