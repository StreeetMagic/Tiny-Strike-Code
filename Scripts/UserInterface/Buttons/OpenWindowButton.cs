using Windows;
using UnityEngine;
using Zenject;

namespace Buttons
{
  public class OpenWindowButton : BaseButton
  {
    [SerializeField]
    protected WindowId WindowId;

    [Inject]
    protected WindowService WindowService;

    private void Start()
    {
      Button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
      WindowService.Open(WindowId);
    }
  }
}