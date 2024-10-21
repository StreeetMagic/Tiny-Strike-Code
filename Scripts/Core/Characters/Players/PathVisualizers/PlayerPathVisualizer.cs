using System;
using System.Collections.Generic;
using System.Linq;
using Meta;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerPathVisualizer : MonoBehaviour
  {
    [Inject]
    private SimpleQuestTargetsProvider _simpleQuestTargetsProvider;

    [Inject]
    private SimpleQuestStorage _simpleQuestStorage;

    [Inject]
    private CompositeQuestStorage _compositeQuestStorage;

    [Inject]
    private CompositeQuestTargetsProvider _compositeQuestTargetsProvider;

    private NavMeshAgent _agent;
    private NavMeshPath _path;

    private Transform _arrowContainer;

    // ReSharper disable once InconsistentNaming
    public GameObject pathMarkerPrefab;
    private readonly List<GameObject> _activeMarkers = new();
    private readonly Queue<GameObject> _markerPool = new();

    // ReSharper disable once InconsistentNaming
    public float markerSpacing = 2.0f;

    // ReSharper disable once InconsistentNaming
    public float minDistanceFromPlayer = 2.0f;

    // ReSharper disable once InconsistentNaming
    public float minDistanceFromTarget = 2.0f;

    private List<SimpleQuestId> _simpleQuestIds;
    private List<CompositeQuestId> _compositeQuestIds;

    private float _timeLeft;
    private readonly float _cooldown = 1f;

    private Transform _target;

    void Start()
    {
      _arrowContainer = new GameObject("ArrowContainer").transform;

      _agent = GetComponent<NavMeshAgent>();
      _path = new NavMeshPath();

      _simpleQuestIds = Enum.GetValues(typeof(SimpleQuestId))
        .Cast<SimpleQuestId>()
        .Where(id => id != SimpleQuestId.Unknown)
        .ToList();

      _compositeQuestIds = Enum.GetValues(typeof(CompositeQuestId))
        .Cast<CompositeQuestId>()
        .Where(id => id != CompositeQuestId.Unknown)
        .ToList();
    }

    void Update()
    {
      if (_timeLeft < 0)
      {
        _target = GetClosestTarget();

        _timeLeft = _cooldown;
      }

      _timeLeft -= Time.deltaTime;

      if (_target)
      {
        _agent.CalculatePath(_target.position, _path);

        if (_path.status == NavMeshPathStatus.PathComplete)
        {
          UpdatePathMarkers(_path.corners, _target);
        }
      }
    }

    private Transform GetClosestTarget()
    {
      float minDistanceSimple = float.MaxValue;
      float minDistanceComposite = float.MaxValue;

      Transform closestSimpleTarget = FindClosestSimpleQuestTarget(_simpleQuestTargetsProvider, _simpleQuestIds, ref minDistanceSimple);

      Transform closestCompositeTarget = FindClosestCompositeQuestTarget(_compositeQuestTargetsProvider, _compositeQuestIds, ref minDistanceComposite);

      return minDistanceSimple <= minDistanceComposite
        ? closestSimpleTarget
        : closestCompositeTarget;
    }

    private Transform FindClosestSimpleQuestTarget(SimpleQuestTargetsProvider provider, List<SimpleQuestId> questIds, ref float minDistance)
    {
      Transform closestTarget = null;
      Vector3 agentPosition = _agent.transform.position;
      float minDistanceSquared = minDistance * minDistance;

      foreach (SimpleQuestId questId in questIds)
      {
        Transform[] targets = provider.GetTargetsOrNull(questId);

        if (targets == null) continue;

        foreach (Transform target in targets)
        {
          if (!target)
            break;

          float distanceSquared = (agentPosition - target.position).sqrMagnitude;

          if (distanceSquared < minDistanceSquared)
          {
            minDistanceSquared = distanceSquared;
            closestTarget = target;
          }
        }
      }

      minDistance = Mathf.Sqrt(minDistanceSquared);
      return closestTarget;
    }

    private Transform FindClosestCompositeQuestTarget(CompositeQuestTargetsProvider provider, List<CompositeQuestId> questIds, ref float minDistance)
    {
      if (provider == null || questIds == null || questIds.Count == 0)
        return null;

      Transform closestTarget = null;
      Vector3 agentPosition = _agent.transform.position;
      float minDistanceSquared = minDistance * minDistance;

      foreach (CompositeQuestId questId in questIds)
      {
        Transform[] targets = provider.GetTargetsOrNull(questId);

        if (targets == null || targets.Length == 0)
          continue;

        foreach (Transform target in targets)
        {
          if (!target)
            break;

          Vector3 targetPosition = target.position;
          float distanceSquared = (agentPosition - targetPosition).sqrMagnitude;

          if (distanceSquared < minDistanceSquared)
          {
            minDistanceSquared = distanceSquared;
            closestTarget = target;
          }
        }
      }

      minDistance = Mathf.Sqrt(minDistanceSquared);
      return closestTarget;
    }

    private void UpdatePathMarkers(Vector3[] corners, Transform target)
    {
      int markerIndex = 0;

      for (int i = corners.Length - 1; i > 0; i--)
      {
        Vector3 startPoint = corners[i];
        Vector3 endPoint = corners[i - 1];
        float segmentDistance = Vector3.Distance(startPoint, endPoint);
        Vector3 direction = (endPoint - startPoint).normalized;

        int markerCount = Mathf.FloorToInt(segmentDistance / markerSpacing);

        for (int j = 0; j < markerCount; j++)
        {
          Vector3 position = startPoint + direction * (j * markerSpacing);

          if (ShouldSkipMarker(position, corners, i, target))
            continue;

          GameObject marker;

          if (markerIndex < _activeMarkers.Count)
          {
            marker = _activeMarkers[markerIndex];
            marker.transform.position = position;
            marker.transform.rotation = Quaternion.LookRotation(direction);

            if (!marker.activeSelf)
            {
              marker.SetActive(true);
            }
          }
          else
          {
            marker = GetOrCreateMarker(position, direction);
            _activeMarkers.Add(marker);
          }

          markerIndex++;
        }
      }

      for (int i = markerIndex; i < _activeMarkers.Count; i++)
      {
        GameObject marker = _activeMarkers[i];

        if (marker.activeSelf)
        {
          marker.SetActive(false);
        }

        _markerPool.Enqueue(marker);
      }

      _activeMarkers.RemoveRange(markerIndex, _activeMarkers.Count - markerIndex);
    }

    private bool ShouldSkipMarker(Vector3 position, Vector3[] corners, int index, Transform target)
    {
      float distanceToPlayer = Vector3.Distance(_agent.transform.position, position);
      float distanceToTarget = Vector3.Distance(target.position, position);

      bool isNearCorner = (index > 0 && Vector3.Distance(position, corners[index]) < minDistanceFromPlayer) ||
                          (index < corners.Length - 2 && Vector3.Distance(position, corners[index - 1]) < minDistanceFromPlayer);

      return distanceToPlayer < minDistanceFromPlayer || distanceToTarget < minDistanceFromTarget || isNearCorner;
    }

    private GameObject GetOrCreateMarker(Vector3 position, Vector3 direction)
    {
      GameObject marker;

      if (_markerPool.Count > 0)
      {
        marker = _markerPool.Dequeue();
        marker.transform.position = position;
        marker.transform.rotation = Quaternion.LookRotation(direction);
        marker.SetActive(true);
      }
      else
      {
        marker = Instantiate(pathMarkerPrefab, position, Quaternion.LookRotation(direction), _arrowContainer);
      }

      return marker;
    }
  }
}