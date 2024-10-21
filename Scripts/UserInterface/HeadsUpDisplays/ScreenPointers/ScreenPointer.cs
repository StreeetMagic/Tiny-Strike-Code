using Core.Characters.Players;
using LevelDesign.Maps;
using Tutorials;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.ScreenPointers
{
  public abstract class ScreenPointer : MonoBehaviour
  {
    private const float ScreenEdgeOffset = 50f;

    public RectTransform Pointer;

    [Inject] protected MapProvider MapProvider;
    [Inject] protected TutorialProvider TutorialProvider;
    [Inject] protected PlayerProvider PlayerProvider;

    [field: SerializeField] public RectTransform ParentCanvasRectTransform { get; set; }
    public bool IsClosest { get; set; } = true;
    protected Camera MainCamera { get; private set; }

    private void Awake()
    {
      MainCamera = Camera.main;
    }

    protected void UpdatePointer(Vector3 worldToScreenPoint)
    {
      if (!IsClosest)
      {
        Hide();
        return;
      }

      if (worldToScreenPoint is { z: > 0, x: > 0 } && worldToScreenPoint.x < Screen.width && worldToScreenPoint.y > 0 && worldToScreenPoint.y < Screen.height)
      {
        Hide();
      }
      else
      {
        if (!ParentCanvasRectTransform)
          return;

        Show();
        Position(worldToScreenPoint);
      }
    }

    private void Position(Vector3 screenPoint)
    {
      Vector2 canvasSize = ParentCanvasRectTransform.sizeDelta;
      Vector2 halfCanvasSize = canvasSize / 2;
      
      RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentCanvasRectTransform, screenPoint, MainCamera, out Vector2 _);
      
      Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
      Vector2 direction = (new Vector2(screenPoint.x, screenPoint.y) - screenCenter).normalized;
      
      Vector2 maxOffset = new Vector2(
        halfCanvasSize.x - ScreenEdgeOffset,
        halfCanvasSize.y - ScreenEdgeOffset
      );
      
      Vector2 boundary = direction * Mathf.Min(maxOffset.x / Mathf.Abs(direction.x), maxOffset.y / Mathf.Abs(direction.y));
      
      Pointer.anchoredPosition = boundary;
    }

    public void Hide() =>
      Pointer.gameObject.SetActive(false);

    public void Show() =>
      Pointer.gameObject.SetActive(true);
  }
}