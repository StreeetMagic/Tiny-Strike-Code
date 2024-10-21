using UnityEngine;

namespace Core.Characters.Players
{
  public class PlayerCompanionContainer : MonoBehaviour
  {
    [field: SerializeField] public Transform[] SpawnPoints { get; private set; }
  }
}