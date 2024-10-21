using ConfigProviders;
using Core.Characters;
using Meta;
using Prefabs;
using UnityEngine;
using Zenject;
using ZenjectFactories.SceneContext;

namespace LevelDesign
{
  public class HostageSpawnMarker : MonoBehaviour
  {
    [Inject] private SimpleQuestStorage _simpleQuestStorage;
    [Inject] private DevConfigProvider _configProvider;
    [Inject] private HubZenjectFactory _hubZenjectFactory;

    [field: SerializeField] public SimpleQuestId QuestId { get; private set; }

    public bool Spawned { get; private set; }
    public Hostage Hostage { get; private set; }

    private void Awake()
    {
      if (QuestId == SimpleQuestId.Unknown)
        Debug.LogWarning("HostageSpawnMarker has unknown quest id");
    }

    private void Update()
    {
      if (Spawned)
        return;

      if (_simpleQuestStorage.Get(QuestId).State.Value == QuestState.Activated)
      {
        Spawned = true;
        Hostage = _hubZenjectFactory.InstantiatePrefabForComponent(_configProvider.GetPrefabForComponent<Hostage>(PrefabId.Hostage), transform);
        Hostage.SpawnMarker = this;
        Hostage.gameObject.name = name;
      }
    }
  }
}