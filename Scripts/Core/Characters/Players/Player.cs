using Core.Spawners.Companions;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  [SelectionBase]
  public class Player : MonoBehaviour
  {
    [Inject] private CompanionSpawner _companionSpawner;
    [Inject] private PlayerCompanionContainer _playerCompanionContainer;

    // private void Start()
    // {
    //   _companionSpawner.Spawn(CompanionId.Companion1, new TransformContainer(_playerCompanionContainer.SpawnPoints[0]));
    //   _companionSpawner.Spawn(CompanionId.Companion1, new TransformContainer(_playerCompanionContainer.SpawnPoints[1]));
    //   _companionSpawner.Spawn(CompanionId.Companion1, new TransformContainer(_playerCompanionContainer.SpawnPoints[2]));
    //   _companionSpawner.Spawn(CompanionId.Companion1, new TransformContainer(_playerCompanionContainer.SpawnPoints[3]));
    // }
  }
}