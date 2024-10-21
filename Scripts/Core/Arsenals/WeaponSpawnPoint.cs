using System.Collections.Generic;
using ConfigProviders;
using Core.Characters.Players;
using Core.PickUpTreasures;
using Core.Weapons;
using UnityEngine;
using Zenject;
using ZenjectFactories.SceneContext;

namespace Core.Arsenals
{
  [SelectionBase]
  public class WeaponSpawnPoint : MonoBehaviour
  {
    public const float SpawnTimer = 1;

    public WeaponId WeaponId;
    public Transform TreasureContainer;

    private Dictionary<WeaponId, PickUpTreasureId> _idMap;

    [Inject] private ArtConfigProvider _artConfigProvider;
    [Inject] private HubZenjectFactory _gameLoopZenjectFactory;
    [Inject] private WeaponStorage _weaponStorage;
    [Inject] private PlayerProvider _playerProvider;

    private bool _treasureSpawned;
    private float _timeLeft;
    private PickUpTreasureView _pickUpTreasureView;

    private void Awake()
    {
      _idMap = new()
      {
        { WeaponId.Knife, PickUpTreasureId.Knife },
        { WeaponId.Xm1014, PickUpTreasureId.Xm1014 },
        { WeaponId.Ak47, PickUpTreasureId.Ak47 },
        { WeaponId.DesertEagle, PickUpTreasureId.DesertEagle },
      };
    }

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;

      if (_playerProvider.Instance.WeaponIdProvider.CurrentId.Value == WeaponId)
      {
        Despawn();
      }
      else if (_weaponStorage.Weapons.Contains(WeaponId))
      {
        Tick();
      }
    }

    private void Despawn()
    {
      if (_treasureSpawned == false)
        return;

      if (!_pickUpTreasureView)
        return;

      Destroy(_pickUpTreasureView.gameObject);
      _pickUpTreasureView = null;
      _treasureSpawned = false;
    }

    private void Tick()
    {
      if (_treasureSpawned)
      {
        _timeLeft = SpawnTimer;
      }
      else
      {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0)
        {
          Spawn();
        }
      }
    }

    private void Spawn()
    {
      PickUpTreasureView prefab = _artConfigProvider.PickUpTreasures[_idMap[WeaponId]].Prefab;
      _pickUpTreasureView = _gameLoopZenjectFactory.InstantiatePrefabForComponent(prefab, TreasureContainer.transform.position, Quaternion.identity, TreasureContainer);
      _pickUpTreasureView.ShowWindow = false;
      _treasureSpawned = true;
      _pickUpTreasureView.PickedUp += OnPickedUp;
    }

    private void OnPickedUp()
    {
      _pickUpTreasureView.PickedUp -= OnPickedUp;
      _treasureSpawned = false;
      _timeLeft = SpawnTimer;
      _pickUpTreasureView = null;
    }
  }
}