using System;
using Core.Characters.Players;
using Meta.Stats;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HeadsUpDisplays.HealthBars
{
  [RequireComponent(typeof(Slider))]
  public class HealthBarSlider : MonoBehaviour
  {
    public float VisibleTime = 2f;

    public Slider Slider;
    // ReSharper disable once InconsistentNaming
    public Image fillImage; // Ссылка на Image, который используется для заполнения слайдера
    public Slider WhiteSlider;

    // ReSharper disable once InconsistentNaming
    public float scaleDuration = 1.0f; // Длительность одного цикла изменения масштаба
    // ReSharper disable once InconsistentNaming
    public float maxScale = 1.2f;
    public float WhiteSliderUpdateSpeed;
    public float SliderUpdateSpeed;

    public Color RedColore = Color.red;
    public Color YellowColore = Color.yellow;
    public Color GreenColore = Color.green;

    // ReSharper disable once InconsistentNaming
    public GameObject[] _allObjects;

    [Range(0f, 1f)] public float HealthValueToRedColor = .4f;
    [Range(0f, 1f)] public float HealthValueToYellowColor = .7f;

    //private Slider _slider;

    [Inject] private PlayerStatsProvider _playerStatsProvider;
    [Inject] private PlayerProvider _playerProvider;

    private float _timeLeft;
    private bool _isDamaged;

    // private void Awake()
    // {
    //    _slider = GetComponent<Slider>();
    // }
    //
    // private void OnEnable()
    // {
    //   _playerProvider.Instance.Health.Damaged += StartScaleBounce;
    // }

    private void Start()
    {
      Hide();

      _playerProvider.Instance.Health.Damaged += OnDamaged;

      // Убедимся, что ссылки на компоненты установлены
      if (Slider == null || fillImage == null)
      {
        Debug.LogError("Ссылки на Slider или Image не установлены!");
        return;
      }

      // Подписываемся на событие изменения значения слайдера
      Slider.onValueChanged.AddListener(UpdateFillColor);
      // Обновляем цвет сразу при старте
      UpdateFillColor(Slider.value);
    }

    // private void StartScaleBounce(float obj)
    // {
    //   // Исходный масштаб объекта
    //   Vector3 originalScale = transform.localScale;
    //
    //   // Анимация увеличения масштаба
    //   transform.DOScale(originalScale * maxScale, scaleDuration / 2)
    //     .SetEase(Ease.OutQuad)
    //     .OnComplete(() =>
    //     {
    //       // Анимация уменьшения масштаба
    //       transform.DOScale(originalScale, scaleDuration / 2)
    //         .SetEase(Ease.InQuad)
    //         .OnComplete(() => { transform.localScale = Vector3.one; });
    //     });
    // }

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;

      UpdateSlider();
      VisibleTimer();
    }

    private void UpdateSlider()
    {
      float max = _playerStatsProvider.GetStat(StatId.Health);
      float current = _playerProvider.Instance.Health.Current.Value;
      float value = current / max;

      if (Math.Abs(Slider.value - value) > 0.01f)
        Slider.value = value;

      if (Math.Abs(WhiteSlider.value - value) > 0.01f)
        WhiteSlider.value =
          Mathf.MoveTowards(WhiteSlider.value, value, Time.deltaTime * WhiteSliderUpdateSpeed);
      // float max = _playerStatsProvider.GetStat(StatId.Health);
      // float current = _playerProvider.Instance.Health.Current.Value;
      // _slider.value = Mathf.MoveTowards(_slider.value, current / max, Time.deltaTime * SliderUpdateSpeed);
    }

    private void UpdateFillColor(float value)
    {
      // Устанавливаем цвет в зависимости от значения слайдера
      if (value <= HealthValueToRedColor)
      {
        fillImage.color = RedColore;
      }
      else if (value <= HealthValueToYellowColor)
      {
        fillImage.color = YellowColore;
      }
      else
      {
        fillImage.color = GreenColore;
      }
    }

    private void Show()
    {
      foreach (GameObject obj in _allObjects)
        obj.SetActive(true);
    }

    private void Hide()
    {
      foreach (GameObject obj in _allObjects)
        obj.SetActive(false);
    }

    private void VisibleTimer()
    {
      if (!_isDamaged)
        return;

      _timeLeft -= Time.deltaTime;

      if (_timeLeft <= 0)
      {
        _isDamaged = false;
        Hide();
      }
    }

    private void OnDamaged(float obj)
    {
      _timeLeft = VisibleTime;

      if (_isDamaged)
        return;

      _isDamaged = true;

      Show();
    }
  }
}