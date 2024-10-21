using ConfigProviders;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  public class AttackRadiusListener : MonoBehaviour
  {
    private RectTransform _rectTransform;

    [Inject] private PlayerWeaponIdProvider _playerWeaponIdProvider;
    [Inject] private BalanceConfigProvider _balanceConfigProvider;

    private void Awake()
    {
      _rectTransform = GetComponent<RectTransform>();
    }

    // private void OnEnable()
    // {
    //   OnChanged(_playerWeaponIdProvider.CurrentId.Value);
    //   _playerWeaponIdProvider.CurrentId.ValueChanged += OnChanged;
    // }
    //
    // private void OnDisable()
    // {
    //   _playerWeaponIdProvider.CurrentId.ValueChanged -= OnChanged;
    // }

    private void Update()
    {
      OnChanged();
    }

    private void OnChanged()
    {
      float radius = FireRange() * 2;

      _rectTransform.localScale = new Vector3(radius, radius, radius);
    }

    private float FireRange() =>
      _balanceConfigProvider.Weapons[_playerWeaponIdProvider.CurrentId.Value].Range;
  }
}