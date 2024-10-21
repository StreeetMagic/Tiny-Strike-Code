using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Characters.Enemies
{
  public class SliderValueUpdater : MonoBehaviour
  {
    public Slider Slider;
    public Slider WhiteSlider;
    public float SliderUpdateSpeed;
    public float WhiteSliderUpdateSpeed;

    [Inject] private IHealth _enemyHealth;
    
    private float _targetValue;
    private bool _isUpdatingWhiteSlider;

    private void Start()
    {
      // Подписываемся на событие изменения здоровья
      _enemyHealth.Damaged += OnHealthChanged;
      _enemyHealth.Died += OnDied;

      // Инициализируем значение слайдеров
      float initialValue = _enemyHealth.Current.Value / _enemyHealth.Initial;
      Slider.value = initialValue;
      WhiteSlider.value = initialValue;
      _targetValue = initialValue;
    }

    private void OnDestroy()
    {
      // Отписываемся от событий при уничтожении объекта
      _enemyHealth.Damaged -= OnHealthChanged;
      _enemyHealth.Died -= OnDied;
    }

    private void OnHealthChanged(float damageAmount)
    {
      // Вычисляем новое значение здоровья
      float newHealthValue = _enemyHealth.Current.Value / _enemyHealth.Initial;
      _targetValue = newHealthValue;

      // Обновляем основной слайдер мгновенно
      Slider.value = _targetValue;

      // Запускаем корутину для плавного обновления белого слайдера
      if (!_isUpdatingWhiteSlider)
      {
        StartCoroutine(UpdateWhiteSlider());
      }
    }

    private System.Collections.IEnumerator UpdateWhiteSlider()
    {
      _isUpdatingWhiteSlider = true;

      while (Mathf.Abs(WhiteSlider.value - _targetValue) > 0.01f)
      {
        WhiteSlider.value = Mathf.MoveTowards(WhiteSlider.value, _targetValue, Time.deltaTime * WhiteSliderUpdateSpeed);
        yield return null;
      }

      _isUpdatingWhiteSlider = false;
    }

    private void OnDied(IHealth health, int experience, float corpseRemoveDelay)
    {
      // Обработка смерти врага, если необходимо
      // Например, отключение слайдеров или запуск анимации
    }
  }
}