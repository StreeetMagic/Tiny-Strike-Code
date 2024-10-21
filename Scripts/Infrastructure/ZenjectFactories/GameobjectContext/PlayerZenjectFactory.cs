using ConfigProviders;
using Zenject.Source.Main;

namespace ZenjectFactories.GameobjectContext
{
  public class PlayerZenjectFactory : ZenjectFactory, IGameObjectZenjectFactory
  {
    public PlayerZenjectFactory(IInstantiator instantiator, DevConfigProvider devConfigProvider) : base(instantiator, devConfigProvider)
    {
    }
  }
}