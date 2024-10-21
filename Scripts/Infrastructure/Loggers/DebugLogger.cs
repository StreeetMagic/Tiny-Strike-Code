using Core.Weapons;
using UnityEngine;

namespace Loggers
{
  public class DebugLogger
  {
    private const string Enemy = nameof(Enemy);
    private const string Player = nameof(Player);

    private readonly bool _player = false;
    private readonly bool _enemy = false;

    public void Log(string message)
    {
      Debug.Log(message);
    }

    public void LogWarning(string text)
    {
      Debug.LogWarning(text);
    }

    public void LogError(string lostConfigs)
    {
      Debug.LogError(lostConfigs);
    }

    public void LogShopWeapons(WeaponShop weaponShop)
    {
      string weapons = string.Empty;

      foreach (WeaponId weapon in weaponShop.Weapons.Value)
      {
        weapons += weapon.ToString();
        weapons += ",";
      }

      Log("Shop weapons: " + weapons);
    }

    public void LogPlayerWeapons(WeaponStorage weaponStorage)
    {
      string weapons = string.Empty;

      foreach (WeaponId weapon in weaponStorage.Weapons.Value)
      {
        weapons += weapon.ToString();
        weapons += ",";
      }

      Log("Player weapons: " + weapons);
    }

    private void StateMachine(string message)
    {
      if (message.Contains(Player) && _player)
        Log(message);

      if (message.Contains(Enemy) && _enemy)
        Log(message);
    }

    public void LogTransition(string message)
    {
      StateMachine(message);
    }

    public void LogStateEnter(string message)
    {
      StateMachine(message);
    }

    public void LogStateExit(string message)
    {
      StateMachine(message);
    }
  }
}