using AudioServices;
using AudioServices.Sounds;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Buttons
{
  public class BaseButton : MonoBehaviour
  {
    [SerializeField]
    protected SoundId SoundId = SoundId.ButtonTap;

    [Inject]
    protected AudioService AudioService;

    protected Button Button { get; private set; }

    private void Awake()
    {
      OnAwake();
    }

    protected virtual void OnAwake()
    {
      Button = GetComponent<Button>();
      Button.onClick.AddListener(() => AudioService.Play(SoundId));
    }
  }
}