using Zenject;

namespace Tutorials
{
  public class TutorialProvider : ITickable
  {
    public Tutorial Instance { get; set; }

    public void Tick() =>
      Instance?.Tick();
  }
}