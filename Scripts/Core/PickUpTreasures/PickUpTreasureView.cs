using System;
using Windows;
using AudioServices;
using ConfigProviders;
using Core.Characters.Players;
using Loggers;
using UnityEngine;
using Zenject;

namespace Core.PickUpTreasures
{
  [SelectionBase]
  public class PickUpTreasureView : MonoBehaviour
  {
    public PickUpTreasureId TreasureId;
    public float PickUpRange = 4f;
    public bool ShowWindow = true;
    public bool IsDestroyed = true;

    [Inject] private PlayerProvider _playerProvider;
    [Inject] private WindowService _windowService;
    [Inject] private AudioService _audioService;
    [Inject] private PickUpTreasureCollector _pickUpTreasureService;
    [Inject] private ArtConfigProvider _artConfigProvider;

    private bool _isCollected;

    public event Action PickedUp;

    private void Awake()
    {
      if (TreasureId == PickUpTreasureId.Unknown)
        new DebugLogger().LogError("У pickUpTresuareId = PickUpTresuareId.Uknown");
    }

    private void Update()
    {
      if (_isCollected)
        return;

      if (!_playerProvider.Instance)
        return;

      if (Vector3.Distance(transform.position, _playerProvider.Instance.transform.position) > PickUpRange)
        return;

      _isCollected = true;

      if (ShowWindow)
        _windowService.Open(WindowId.PickUp, TreasureId);

      _pickUpTreasureService.Collect(TreasureId);

      PickedUp?.Invoke();

      _audioService.Play(_artConfigProvider.PickUpTreasures[TreasureId].PickUpSound);

      foreach (Transform child in transform)
        child.gameObject.SetActive(false);

      Destroy(gameObject);
    }

    public void Init(bool destroyAfterTime, float destroyTimer)
    {
      if (IsDestroyed == false)
        return;
      
      if (destroyAfterTime)
        Destroy(gameObject, destroyTimer);
    }
  }
}