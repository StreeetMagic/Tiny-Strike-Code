using Core.Characters;
using UnityEngine;
using UnityEngine.AI;
using Zenject.Source.Install;

namespace Core.AimObstacles
{
  public class AimObstacleInstaller : MonoInstaller
  {
    public IHealth Health { get; private set; }
    
    public override void InstallBindings()
    {
      Container.Bind<AimObstacle>().FromInstance(GetComponent<AimObstacle>()).AsSingle();
      Container.Bind<NavMeshObstacle>().FromInstance(GetComponent<NavMeshObstacle>()).AsSingle();
      Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
      Container.Bind<QuestOutline>().FromInstance(GetComponentInChildren<QuestOutline>()).AsSingle(); 
      Container.Bind<IHealth>().To<AimObstacleHealth>().FromInstance(GetComponent<AimObstacleHealth>()).AsSingle();
      Container.Bind<ITargetTrigger>().To<AimObstacleTargetTrigger>().FromInstance(GetComponent<AimObstacleTargetTrigger>()).AsSingle();

      Container.BindInterfacesAndSelfTo<HitStatus>().AsSingle();
      Container.BindInterfacesAndSelfTo<AimObstacleExpierience>().AsSingle();
      Container.BindInterfacesAndSelfTo<ExpirienceCollector>().AsSingle();
      Container.BindInterfacesAndSelfTo<AimObstacleSelfDestroer>().AsSingle();
      Container.BindInterfacesAndSelfTo<AimObstacleQuestOutlineSwitcher>().AsSingle();
      
      Container.Resolve<AimObstacle>().Installer = this;
      Health = Container.Resolve<IHealth>();
    }
  }
}