using System;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;
using ZenjectFactories.SceneContext;

namespace Cheats.MapTeleports
{
  public class CheatTeleportButtonSpawner : MonoBehaviour
  {
    public GameObject Prefab;

    [Inject] private HubZenjectFactory _gameLoopZenjectFactory;

    private Vector3[] _teleports;

    private void Start()
    {
      int count = CollectTeleports();

      Spawn(count);
    }

    private void Spawn(int count)
    {
      for (int i = 1; i <= count; i++)
      {
        GameObject gameObj = _gameLoopZenjectFactory.InstantiatePrefab(Prefab, transform);

        gameObj.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
        gameObj.GetComponent<CheatTelepornButton>().Init(i, _teleports);
      }
    }

    private int CollectTeleports()
    {
      CheatTeleportPointMarker[] markers = FindObjectsOfType<CheatTeleportPointMarker>();

      if (ValidateSameNumbers(markers) == false)
      {
        Time.timeScale = 0;
        throw new Exception("НА КАРТЕ ЕСТЬ ЧИТ ТЕЛЕПОРТЫ С ОДИНАКОВЫМИ НОМЕРАМИ");
      }
      
      Array
        .Sort(markers, (x, y) => x.Number.CompareTo(y.Number));

      _teleports = new Vector3[markers.Length];

      for (int i = 0; i < markers.Length; i++)
      {
        _teleports[i] = markers[i].transform.position;
      }

      return markers.Length;
    }

    private bool ValidateSameNumbers(CheatTeleportPointMarker[] tps)
    {
      int[] numbers = new int[tps.Length];

      for (int i = 0; i < numbers.Length; i++)
      {
        numbers[i] = tps[i].Number;
      }

      return numbers.Distinct().Count() == numbers.Length;
    }
  }
}