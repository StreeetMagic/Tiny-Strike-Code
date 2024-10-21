using System.Collections.Generic;
using UnityEngine;

namespace Core.Characters.Players
{
  public class PlayerHenContainer : MonoBehaviour
  {
    public List<Transform> SpawnPoints;
  
    public Transform GetRandomSpawnPoint() => 
      SpawnPoints[Random.Range(0, SpawnPoints.Count - 1)];
  }
}