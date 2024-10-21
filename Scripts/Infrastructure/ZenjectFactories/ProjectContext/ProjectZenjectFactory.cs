using ConfigProviders;
using Zenject.Source.Main;

namespace ZenjectFactories.ProjectContext
{
  public class ProjectZenjectFactory : ZenjectFactory
  {
    public ProjectZenjectFactory(IInstantiator instantiator, DevConfigProvider devConfigProvider) : base(instantiator, devConfigProvider)
    {
    }
  }
}