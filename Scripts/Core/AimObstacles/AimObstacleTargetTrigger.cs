using System.Collections.Generic;
using Core.Characters;
using Core.Weapons;
using UnityEngine;
using Zenject;

namespace Core.AimObstacles
{
  public class AimObstacleTargetTrigger : MonoBehaviour, ITargetTrigger
  {
    [SerializeField] private Transform _targetPoint;

    private Collider _collider;

    [Inject] private AimObstacle _aimObstacle;

    [Inject] public IHealth Health { get; private set; }
    [Inject] public HitStatus HitStatus { get; private set; }

    public TargetPriority TargetPriority => _aimObstacle.TargetPriority;
    public bool IsTargeted { get; set; }
    public bool IsTargetable { get; set; } = true;
    public Transform TargetPoint => _targetPoint;
    public List<WeaponId> WeaponWhiteList => _aimObstacle.WeaponWhiteList;

    private void Awake()
    {
      _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
      Health.Died += OnDied;
    }

    private void OnDisable()
    {
      Health.Died -= OnDied;
    }

    private void OnDied(IHealth enemyHealth, int expirity, float corpseRemoveDelay)
    {
      _collider.enabled = false;
    }

    public void TakeDamage(float damage)
    {
      Health.TakeDamage(damage);
    }

    public void Hit()
    {
      Health.Hit();
    }
  }
}