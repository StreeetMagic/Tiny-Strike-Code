using ConfigProviders;
using UnityEngine;
using ZenjectFactories.GameobjectContext;

namespace Core.Characters.Enemies
{
  public class EnemyMeshModelSpawner
  {
    private EnemyMeshModelSpawner
    (
      EnemyId enemyId,
      EnemyShootingPointProvider shootingPointProvider,
      IGameObjectZenjectFactory factory,
      EnemyAnimatorProvider animatorProvider,
      Transform transform,
      ArtConfigProvider artConfigProvider,
      EnemyAlertPointProvider alertPointProvider, 
      EnemyInstaller enemyInstaller)
    {
      EnemyMeshModel prefab = artConfigProvider.Enemies[enemyId].EnemyMeshModelPrefab;
      EnemyMeshModel meshModel = factory.InstantiatePrefabForComponent(prefab, transform.position, transform);
      shootingPointProvider.PointTransform = meshModel.GetComponent<EnemyShootingPoint>().PointTransform;
      alertPointProvider.PointTransform = meshModel.GetComponent<EnemyAlertPoint>().PanicTransform;
      animatorProvider.Instance = meshModel.GetComponent<EnemyAnimatorController>();
      MeshModel = meshModel;
      enemyInstaller.MeshModel = meshModel;
    }

    public EnemyMeshModel MeshModel { get; private set; }
  }
}