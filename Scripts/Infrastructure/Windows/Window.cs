using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
  public abstract class Window : MonoBehaviour
  {
    [SerializeField]
    protected Button CloseButton;

    [SerializeField]
    protected WindowCloseAnimationController WindowCloseAnimationController;

    private void OnEnable()
    {
      SubscribeUpdates();

      CloseButton.onClick.AddListener(Close);
      CloseButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
      CloseButton.onClick.RemoveAllListeners();
      Cleanup();
    }

    public void Close()
    {
      if (WindowCloseAnimationController)
      {
        WindowCloseAnimationController.CloseWindow();
      }
      else
      {
        gameObject.SetActive(false);
      }
    }

    public abstract void Initialize();

    protected abstract void SubscribeUpdates();
    protected abstract void Cleanup();
  }
}