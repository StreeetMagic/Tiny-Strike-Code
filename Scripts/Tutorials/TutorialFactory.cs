using SaveLoadServices;
using ZenjectFactories.SceneContext;

namespace Tutorials
{
  public class TutorialFactory
  {
    private readonly HubZenjectFactory _factory;
    private readonly TutorialProvider _tutorialProvider;
    private readonly ISaveLoadService _saveLoadService;

    public TutorialFactory(HubZenjectFactory factory, TutorialProvider tutorialProvider, ISaveLoadService saveLoadService)
    {
      _factory = factory;
      _tutorialProvider = tutorialProvider;
      _saveLoadService = saveLoadService;
    }

    public Tutorial Create()
    {
      Tutorial tutorial = _factory.Instantiate<Tutorial>();
      _tutorialProvider.Instance = tutorial;

      _saveLoadService.ProgressReaders.Add(tutorial);
      return tutorial;
    }
  }
}