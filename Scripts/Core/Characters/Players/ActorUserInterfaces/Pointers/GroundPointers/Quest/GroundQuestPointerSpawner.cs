using ConfigProviders;
using UnityEngine;
using Zenject;
using ZenjectFactories.SceneContext;

namespace Core.Characters.Players.GroundPointers
{
  public abstract class GroundQuestPointerSpawner : MonoBehaviour
  {
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