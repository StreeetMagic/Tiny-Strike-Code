using System.Collections.Generic;
using System.Linq;
using ConfigProviders;
using Meta.Configs;
using PersistentProgresses;
using SaveLoadServices;
using ZenjectFactories.ProjectContext;

namespace Meta
{
  public class SimpleQuestStorage : IProgressWriter
  {
    private Dictionary<SimpleQuestId, SimpleQuest> _simpleQuests;

    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly ProjectZenjectFactory _gameLoopZenjectFactory;

    public SimpleQuestStorage(BalanceConfigProvider balanceConfigProvider,
      ProjectZenjectFactory gameLoopZenjectFactory)
    {
      _balanceConfigProvider = balanceConfigProvider;
      _gameLoopZenjectFactory = gameLoopZenjectFactory;
    }

    public SimpleQuest Get(SimpleQuestId simpleQuestId)
      => _simpleQuests[simpleQuestId];

    public List<SimpleQuest> GetAll()
      => _simpleQuests.Values.ToList();

    public void ReadProgress(ProjectProgress projectProgress)
    {
      Dictionary<SimpleQuestId, SimpleQuestConfig> configs = _balanceConfigProvider.SimpleQuests;

      _simpleQuests = new Dictionary<SimpleQuestId, SimpleQuest>();

      for (int i = 0; i < projectProgress.SimpleQuests.Count; i++)
      {
        QuestState questState = GetQuestState(projectProgress, projectProgress.SimpleQuests[i].Id);
        var simpleQuest = _gameLoopZenjectFactory.Instantiate<SimpleQuest>(questState, configs[projectProgress.SimpleQuests[i].Id], projectProgress.SimpleQuests[i].CompletedQuantity);
        _simpleQuests.Add(projectProgress.SimpleQuests[i].Id, simpleQuest);
      }
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      projectProgress.SimpleQuests.Clear();

      foreach (KeyValuePair<SimpleQuestId, SimpleQuest> simpleQuest in _simpleQuests)
      {
        projectProgress.SimpleQuests.Add(new SimpleQuestProgress(simpleQuest.Key, simpleQuest.Value.State.Value, simpleQuest.Value.CompletedQuantity.Value));
      }
    }

    private QuestState GetQuestState(ProjectProgress projectProgress, SimpleQuestId simpleQuestId)
    {
      return projectProgress.SimpleQuests.Find(x => x.Id == simpleQuestId).State;
    }
  }
}