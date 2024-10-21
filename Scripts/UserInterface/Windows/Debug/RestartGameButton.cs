using Buttons;
using Scenes.Hubs;
using Zenject;

public class RestartGameButton : BaseButton
{
  [Inject] private HubInitializer _hubInitializer;

  private void Start()
  {
    Button.onClick.AddListener(RestartGame);
  }

  private void RestartGame()
  {
    _hubInitializer.Restart();
  }
}