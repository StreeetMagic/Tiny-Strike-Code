using AudioServices;
using AudioServices.Sounds;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class WindowOpenAnimationController : MonoBehaviour
{
  // ReSharper disable once InconsistentNaming
  public CanvasGroup canvasGroup;
  // ReSharper disable once InconsistentNaming
  public float animationDuration = 0.5f;
  // ReSharper disable once InconsistentNaming
  public Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);

  [Inject] private AudioService _audioService;

  private Vector3 _originalScale;

  void Awake()
  {
    if (!canvasGroup)
      canvasGroup = GetComponent<CanvasGroup>();

    _originalScale = transform.localScale;
  }

  void OnEnable()
  {
    // Останавливаем все текущие анимации на transform и canvasGroup
    transform.DOKill();
    canvasGroup.DOKill();

    ResetWindowState();
    ShowWindow();
  }

  private void ResetWindowState()
  {
    transform.localScale = startScale;
    canvasGroup.alpha = 0f; // Устанавливаем альфа-канал в 0
  }

  private void ShowWindow()
  {
    _audioService.Play(SoundId.TooltipPopup);

    // Запуск новых анимаций без сохранения ссылок на Tween
    transform
      .DOScale(_originalScale, animationDuration)
      .SetEase(Ease.OutBack);

    canvasGroup
      .DOFade(1f, animationDuration)
      .SetEase(Ease.Linear);
  }
}