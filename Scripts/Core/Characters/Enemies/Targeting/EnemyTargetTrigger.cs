using System;
using System.Collections.Generic;
using Core.Weapons;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class EnemyTargetTrigger : MonoBehaviour, ITargetTrigger
  {
    [SerializeField] private Transform _targetPoint;
    
    private Collider _collider;
    
    [Inject] private EnemyConfig _config;

    public event Action<EnemyTargetTrigger> TargetDied;

    [Inject] public IHealth Health { get; private set; }
    [Inject] public HitStatus HitStatus { get; set; }
    
    public TargetPriority TargetPriority => _config.TargetPriority;
    public bool IsTargeted { get; set; }
    public bool IsTargetable { get; set; } = true;
    public float AggroRadius => _config.AggroRadius;
    public Transform TargetPoint => _targetPoint;
    public List<WeaponId> WeaponWhiteList => new();

    private void Awake()
    {
      _collider = GetComponent<Collider>(); 
    }

    private void OnEnable()
    {
      _collider.enabled = true;
      Health.Died += OnDied;
    }

    private void OnDisable()
    {
      Health.Died -= OnDied;
    }
    
    private void OnDied(IHealth enemyHealth, int expirience, float corpseRemoveDelay)
    {
      _collider.enabled = false;

      TargetDied?.Invoke(this);
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