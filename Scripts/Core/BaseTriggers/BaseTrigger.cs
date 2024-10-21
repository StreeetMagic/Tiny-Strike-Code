using Core.Characters.Players;
using HeadsUpDisplays;
using LevelDesign.Maps;
using Meta.BackpackStorages;
using Meta.Currencies;
using SaveLoadServices;
using Tutorials;
using UnityEngine;
using Zenject;

namespace Core
{
  public class BaseTrigger : MonoBehaviour
  {
    public bool IsTutolial;

    [Inject] private ISaveLoadService _saveLoadService;
    [Inject] private CurrencyStorage _currencyStorage;
    [Inject] private PlayerRespawnPosition _playerRespawnPosition;
    [Inject] private BackpackStorage _backpackStorage;
    [Inject] private TutorialProvider _tutorialProvider;
    [Inject] private MapProvider _mapProvider;
    [Inject] private HeadsUpDisplayProvider _headsUpDisplayProvider;

    private bool _isDestroed;

    private void OnTriggerEnter(Collider other)
    {
      if (!other.TryGetComponent(out PlayerTargetTrigger playerTrigger))
        return;

      if (!playerTrigger.transform.parent.TryGetComponent(out Player _))
        return;

      if (_backpackStorage.LootDrops.Count > 0)
      {
        _currencyStorage.ApplyBackpackLoot(_backpackStorage.ReadLoot());
        _backpackStorage.LootDrops.Clear();
        _saveLoadService.SaveProgress(ToString());
        _headsUpDisplayProvider.MoneySender.PlayParticle();
      }

      if (!IsTutolial)
      {
        _playerRespawnPosition.SetPosition(transform.position);
        _saveLoadService.SaveProgress(ToString());
      }
    }

    private void Update()
    {
      if (_isDestroed)
        return;

      if (_tutorialProvider.Instance == null)
        return;

      if (_tutorialProvider.Instance.State.Value == TutorialState.BombDefused && IsTutolial)
      {
        _isDestroed = true;

        if (_mapProvider.Map.BaseTrigger.Contains(this))
          _mapProvider.Map.BaseTrigger.Remove(this);

        Destroy(transform.parent.gameObject);
      }
    }
  }
}