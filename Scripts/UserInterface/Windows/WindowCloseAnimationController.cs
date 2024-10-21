using Windows;
using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class WindowCloseAnimationController : MonoBehaviour
{
  // ReSharper disable once InconsistentNaming
  public float animationDuration = 0.5f;
  // ReSharper disable once InconsistentNaming
  public float fadeOutDuration = 0.5f;
  // ReSharper disable once InconsistentNaming
  public Vector3 endScale = new Vector3(0.5f, 0.5f, 0.5f);

  // ReSharper disable once InconsistentNaming
  public GameObject _dimmed;
  // ReSharper disable once InconsistentNaming
  public GameObject _closeButton;

  private CanvasGroup _canvasGroup;
  
  [Inject] private WindowService _windowService;

  private void Awake()
  {
    _canvasGroup = GetComponent<CanvasGroup>();
  }

  public void OnEnable()
  {
    _dimmed.SetActive(true);
  }

  public void CloseWindow()
  {
    _dimmed.SetActive(false);
    _closeButton.SetActive(false);

    transform
      .DOScale(endScale, animationDuration)
      .SetEase(Ease.InBack);

    _canvasGroup
      .DOFade(0, fadeOutDuration)
      .SetEase(Ease.Linear)
      .OnComplete(() =>
      {
        _windowService.ClearActiveWindow();
        _dimmed.SetActive(true);
        _windowService.ClearActiveWindow();
         gameObject.SetActive(false);
      });
  }
}