using ConfigProviders;
using Zenject.Source.Main;

namespace ZenjectFactories.GameobjectContext
{
  public class EnemyZenjectFactory : ZenjectFactory, IGameObjectZenjectFactory
  {
    public EnemyZenjectFactory(IInstantiator instantiator, DevConfigProvider devConfigProvider) : base(instantiator, devConfigProvider)
    {
    }
  }
}