using ConfigProviders;
using Zenject.Source.Main;

namespace ZenjectFactories.SceneContext
{
  public class HubZenjectFactory : ZenjectFactory
  {
    public HubZenjectFactory(IInstantiator instantiator, DevConfigProvider devConfigProvider) : base(instantiator, devConfigProvider)
    {
    }
  }
}