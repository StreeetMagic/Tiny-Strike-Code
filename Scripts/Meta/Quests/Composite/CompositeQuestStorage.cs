using System.Collections.Generic;
using System.Linq;
using ConfigProviders;
using Meta.Sub;
using PersistentProgresses;
using SaveLoadServices;
using ZenjectFactories.ProjectContext;

namespace Meta
{
  public class CompositeQuestStorage : IProgressWriter
  {
    private Dictionary<CompositeQuestId, CompositeQuest> _quests;

    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly ProjectZenjectFactory _gameLoopZenjectFactory;

    public CompositeQuestStorage(BalanceConfigProvider balanceConfigProvider,
      ProjectZenjectFactory gameLoopZenjectFactory)
    {
      _balanceConfigProvider = balanceConfigProvider;
      _gameLoopZenjectFactory = gameLoopZenjectFactory;
    }

    public CompositeQuest Get(CompositeQuestId compositeQuestId)
      => _quests[compositeQuestId];

    public List<CompositeQuest> GetAll()
      => _quests.Values.ToList();

    public void ReadProgress(ProjectProgress projectProgress)
    {
      Dictionary<CompositeQuestId, CompositeQuestConfig> configs = _balanceConfigProvider.CompositeQuests;

      _quests = new Dictionary<CompositeQuestId, CompositeQuest>();

      for (int i = 0; i < projectProgress.Quests.Count; i++)
      {
        List<SubQuest> subQuests = SubQuest(configs[projectProgress.Quests[i].Id], projectProgress);
        QuestState questState = GetQuestState(projectProgress, projectProgress.Quests[i].Id);
        var quest = _gameLoopZenjectFactory.Instantiate<CompositeQuest>(questState, configs[projectProgress.Quests[i].Id], subQuests, projectProgress.Quests[i].RewardGained);
        _quests.Add(projectProgress.Quests[i].Id, quest);
      }
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      projectProgress.Quests.Clear();

      foreach (KeyValuePair<CompositeQuestId, CompositeQuest> quest in _quests)
      {
        List<SubQuestProgress> subQuests = new List<SubQuestProgress>();

        foreach (SubQuest subQuest in quest.Value.SubQuests)
          subQuests.Add(new SubQuestProgress(subQuest.Id, subQuest.CompletedQuantity.Value, subQuest.State.Value));

        projectProgress.Quests.Add(new CompositeQuestProgress(quest.Key, quest.Value.State.Value, subQuests, quest.Value.RewardGained));
      }
    }

    private static QuestState GetQuestState(ProjectProgress projectProgress, CompositeQuestId compositeQuestId)
    {
      return projectProgress.Quests.Find(x => x.Id == compositeQuestId).State;
    }

    private List<SubQuest> SubQuest(CompositeQuestConfig compositeQuestConfig, ProjectProgress projectProgress)
    {
      List<SubQuest> subQuests = new List<SubQuest>();

      for (var i = 0; i < compositeQuestConfig.SubQuests.Count; i++)
      {
        SubQuestProgress progressSubQuest = projectProgress.Quests.Find(x => x.Id == compositeQuestConfig.Id).SubQuests[i];

        SubQuestSetup subQuestSetup = compositeQuestConfig.SubQuests[i];

        var subQuest = _gameLoopZenjectFactory.Instantiate<SubQuest>(subQuestSetup, progressSubQuest.CompletedQuantity, progressSubQuest.State, compositeQuestConfig.Id);

        subQuests.Add(subQuest);
      }

      return subQuests;
    }
  }
}