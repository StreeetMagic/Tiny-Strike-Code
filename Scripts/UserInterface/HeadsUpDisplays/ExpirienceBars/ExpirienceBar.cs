using ConfigProviders;
using Meta.Expirience;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HeadsUpDisplays.ExpirienceBars
{
  public class ExpirienceBar : MonoBehaviour
  {
    public Slider Slider;

    [Inject] private ExpierienceStorage _expierienceStorage;
    [Inject] private BalanceConfigProvider _balanceConfigProvider;

    private void Update()
    {
      float expierienceToNextLevel = (float)_expierienceStorage.CurrentExpierience() / _expierienceStorage.ExpierienceToNextLevel();

      Slider.value =
        Slider.value > expierienceToNextLevel
          ? expierienceToNextLevel
          : Mathf.MoveTowards(Slider.value, expierienceToNextLevel, Time.deltaTime);
    }
  }
}