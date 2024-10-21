using DG.Tweening;
using UnityEngine;

namespace Core.Grenades
{
  [RequireComponent(typeof(GrenadeDetonator))]
  public class GrenadeMover : MonoBehaviour
  {
    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private GrenadeDetonator _detonator;
    private GrenadeDetonationRadius _detonationRadius;
    private GrenadeConfig _config;

    private float FlightTime => _config.FlightTime;

    public void Init(GrenadeConfig config, Vector3 startPosition, Vector3 targetPosition)
    {
      _startPosition = startPosition;
      _targetPosition = targetPosition;
      _config = config;

      _detonator = GetComponent<GrenadeDetonator>();
      _detonationRadius = GetComponentInChildren<GrenadeDetonationRadius>();
      _detonationRadius.Init(_config);
      _detonationRadius.gameObject.SetActive(false);
    }

    public void Throw()
    {
      MoveGrenade();
    }

    private void MoveGrenade()
    { 
      Vector3 midpoint = (_startPosition + _targetPosition) / 2;
      midpoint.y += Vector3.Distance(_startPosition, _targetPosition) / 2;

      Vector3[] path = new Vector3[] { _startPosition, midpoint, _targetPosition };
      
      transform.DOPath(path, FlightTime, PathType.CatmullRom)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
          Vector3 bounceTarget = transform.position + new Vector3(0, 2, 0);
          transform.DOMove(bounceTarget, 0.5f).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
              transform.DOMove(_targetPosition, 0.5f).SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                  _detonator.Detonate();
                  _detonationRadius.gameObject.SetActive(true);
                });
            });
        });
    }
  }
}