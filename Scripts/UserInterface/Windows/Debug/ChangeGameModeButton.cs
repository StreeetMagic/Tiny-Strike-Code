using AudioServices;
using AudioServices.Sounds;
using Buttons;
using SceneLoaders;
using Scenes;
using UnityEngine;
using Zenject;

public class ChangeGameModeButton : BaseButton
{
  [Inject] private SceneLoader _sceneLoader;
  [Inject] private AudioService _audioService;

  [SerializeField] private SoundId _soundId;
    
  private void Start()
  {
    Button.onClick.AddListener(ChangeGameMode);
  }

  private void ChangeGameMode()
  {
    _audioService.Play(SoundId.ButtonTap);
    _sceneLoader.Load(SceneId.ChooseGameMode);
  }
}