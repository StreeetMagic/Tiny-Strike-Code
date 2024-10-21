using LevelDesign.EnemySpawnMarkers;
using UnityEngine;
using Zenject;

namespace Tutorials.StateChangers
{
  [RequireComponent(typeof(EnemySpawnerMarker))]
  public class EnemySpawnerTutorialFinisher : MonoBehaviour
  {
    public TutorialState State = TutorialState.BombDefused;

    [Inject] private TutorialProvider _tutorialProvider;

    private EnemySpawnerMarker _marker;
    private bool _hasProcessed;

    private void Awake()
    {
      _marker = GetComponent<EnemySpawnerMarker>();

      if (State == TutorialState.Uknown)
        Debug.LogError("EnemySpawnerTutorialFinisher: Unknown state");
    }

    private void Update()
    {
      if (_marker.Spawner == null)
        return;

      if (!_marker.Spawner.Activated)
        return;
      
      if (_hasProcessed)
        return;

      if (_marker.Spawner.Enemies.Count == 0)
      {
        _hasProcessed = true;
        _tutorialProvider.Instance.State.Value = State;
      }
    }
  }
}