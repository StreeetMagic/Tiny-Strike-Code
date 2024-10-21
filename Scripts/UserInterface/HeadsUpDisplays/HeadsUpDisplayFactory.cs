using ConfigProviders;
using HeadsUpDisplays.BackpackBars;
using HeadsUpDisplays.ResourcesSenders;
using Prefabs;
using SaveLoadServices;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace HeadsUpDisplays
{
  public class HeadsUpDisplayFactory
  {
    private HeadsUpDisplay _instance;

    private readonly HubZenjectFactory _factory;
    private readonly HeadsUpDisplayProvider _provider;
    private readonly DevConfigProvider _devConfigProvider;
    private readonly ISaveLoadService _saveLoadService;

    public HeadsUpDisplayFactory(HubZenjectFactory factory,
      HeadsUpDisplayProvider provider, DevConfigProvider devConfigProvider, ISaveLoadService saveLoadService)
    {
      _factory = factory;
      _provider = provider;

      _devConfigProvider = devConfigProvider;
      _saveLoadService = saveLoadService;
    }

    public void Create(Transform parent)
    {
      HeadsUpDisplay prefab = _devConfigProvider.GetPrefabForComponent<HeadsUpDisplay>(PrefabId.HeadsUpDisplay);

      _instance = _factory.InstantiatePrefabForComponent(prefab, parent);

      _provider.HeadsUpDisplay = _instance;
      _saveLoadService.ProgressReaders.Add(_provider.HeadsUpDisplay);

      _provider.CanvasTransform = _instance.GetComponentInChildren<RectTransform>();
      _provider.Borders = _instance.GetComponentInChildren<Borders>();
     // _provider.FloatingJoystick = _instance.GetComponentInChildren<FloatingJoystick>();
      _provider.LootSlotsUpdater = _instance.GetComponentInChildren<LootSlotsUpdater>();
      _provider.BackpackBarFiller = _instance.GetComponentInChildren<BackpackBarFiller>();
      _provider.BaseTriggerTarget = _instance.GetComponentInChildren<BaseTriggerTarget>();
      _provider.ResourcesSendersContainer = _instance.GetComponentInChildren<ResourcesSendersContainer>();
      _provider.BackpackBar = _instance.GetComponentInChildren<BackPackBar>();
      _provider.MoneySender = _instance.GetComponentInChildren<MoneySender>();

      _instance.transform.parent = null;
    }

    public void Destroy()
    {
      _saveLoadService.ProgressReaders.Remove(_provider.HeadsUpDisplay);
      _provider.HeadsUpDisplay = null;

      _provider.Borders = null;
      //_provider.FloatingJoystick = null;
      _provider.LootSlotsUpdater = null;
      _provider.CanvasTransform = null;
      _provider.BackpackBarFiller = null;
      _provider.BaseTriggerTarget = null;
      _provider.ResourcesSendersContainer = null;
      _provider.BackpackBar = null;
      _provider.MoneySender = null;

//      Object.Destroy(_instance.gameObject);
      _instance = null;
    }
  }
}