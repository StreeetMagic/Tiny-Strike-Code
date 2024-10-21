using ConfigProviders;
using DG.Tweening;
using RandomServices;
using UnityEngine;
using ZenjectFactories.SceneContext;

namespace Core.PickUpTreasures
{
  public class PickUpTreasureSpawner
  {
    private readonly HubZenjectFactory _zenjectFactory;
    private readonly RandomService _randomService;
    private readonly ArtConfigProvider _artConfigProvider;

    public PickUpTreasureSpawner(HubZenjectFactory zenjectFactory, RandomService randomService,
      ArtConfigProvider artConfigProvider)
    {
      _zenjectFactory = zenjectFactory;
      _randomService = randomService;
      _artConfigProvider = artConfigProvider;
    }

    public void Spawn(PickUpTreasureId id, Vector3 position, bool destroyAfterTime, float destroyTimer)
    {
      GameObject go = _artConfigProvider.PickUpTreasures[id].Prefab.gameObject;

      GameObject pickUpTreasure = _zenjectFactory.InstantiatePrefab(go, position, Quaternion.identity, null);
      pickUpTreasure.SetActive(true);
      pickUpTreasure.GetComponent<PickUpTreasureView>().Init(destroyAfterTime, destroyTimer);

      Vector3 randomDirection = Random.insideUnitSphere; // Генерируем случайное направление
      randomDirection.y = go.transform.position.y; // Убираем компоненту Y, чтобы объект оставался на той же высоте

      float distance = _randomService.GetRandomFloat(2f, 4f); // Расстояние, на которое объект будет отброшен
      float duration = _randomService.GetRandomFloat(0.3f, 0.5f); // Время, за которое объект достигнет конечной точки
      float jumpPower = 2f; // Высота прыжка
      int numJumps = 1; // Количество прыжков

      Vector3 targetPosition = position + randomDirection.normalized * distance; // Конечная точка

      pickUpTreasure.transform
        .DOJump(targetPosition, jumpPower, numJumps, duration)
        .SetEase(Ease.OutQuad);
    }
  }
}