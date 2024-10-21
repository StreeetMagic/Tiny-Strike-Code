using ConfigProviders;
using UnityEngine;
using Zenject;
using ZenjectFactories.SceneContext;

namespace HeadsUpDisplays.ScreenPointers
{
  public abstract class ScreenQuestPointerSpawner : MonoBehaviour
  {
    public RectTransform ParentCanvasRectTransform;
    
    [Inject] protected HubZenjectFactory Factory;
    [Inject] protected DevConfigProvider DevConfigProvider;

    private void Start()
    {
      CreateAll();
    }

    private void Update()
    {
      ShowClosestAndHideOthers(); 
    }

    protected abstract void CreateAll();
    protected abstract void ShowClosestAndHideOthers();
  }
}