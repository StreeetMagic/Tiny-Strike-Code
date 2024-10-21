using System;
using AudioServices;
using ConfigProviders;
using UnityEngine;

namespace Core.Characters.Players
{
  public class PlayerWeaponMagazineReloader
  {
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly PlayerProvider _playerProvider;
    private readonly AudioService _audioService;
    private readonly ArtConfigProvider _artConfigProvider;
    private readonly PlayerWeaponIdProvider _playerWeaponIdProvider;
    
    private float _timeLeft;

    public PlayerWeaponMagazineReloader(BalanceConfigProvider balanceConfigProvider, 
      PlayerProvider playerProvider, AudioService audioService, ArtConfigProvider artConfigProvider,
      PlayerWeaponIdProvider playerWeaponIdProvider)
    {
      _balanceConfigProvider = balanceConfigProvider;
      _playerProvider = playerProvider;
      _audioService = audioService;
      _artConfigProvider = artConfigProvider;
      _playerWeaponIdProvider = playerWeaponIdProvider;
    }

    public bool IsActive { get; private set; }
    public float FullTime => _balanceConfigProvider.Weapons[_playerWeaponIdProvider.CurrentId.Value].ReloadTime;
    public float TimePassed => FullTime - _timeLeft;

    public void Activate()
    {
      if (IsActive)
        return;

      IsActive = true;
      _timeLeft = _balanceConfigProvider.Weapons[_playerWeaponIdProvider.CurrentId.Value].ReloadTime;
      
      _audioService.Play(_artConfigProvider.Weapons[_playerWeaponIdProvider.CurrentId.Value].ReloadSound);
    }

    public void Tick()
    {
      if (IsActive == false)
        throw new Exception("Reloader is not active");

      if (_timeLeft > 0)
      {
        _timeLeft -= Time.deltaTime;
        return;
      }

      if (_timeLeft <= 0)
      {
        _timeLeft = 0;
      }

      _playerProvider.Instance.WeaponAmmo.Reload(_playerWeaponIdProvider.CurrentId.Value);
      IsActive = false;
     // _playerWeaponIdProvider.CurrentId.Value = WeaponId.Unknown;
    }
  }
}