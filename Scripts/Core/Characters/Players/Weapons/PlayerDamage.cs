using ConfigProviders;
using Core.Weapons;
using Meta.Stats;
using RandomServices;

namespace Core.Characters.Players
{
  public class PlayerDamage
  {
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly PlayerStatsProvider _playerStatsProvider;
    private readonly RandomService _randomService;

    public PlayerDamage(BalanceConfigProvider balanceConfigProvider, PlayerStatsProvider playerStatsProvider, RandomService randomService)
    {
      _balanceConfigProvider = balanceConfigProvider;
      _playerStatsProvider = playerStatsProvider;
      _randomService = randomService;
    }

    public float Get(WeaponId id)
    {
      float baseWeaponDamage = _balanceConfigProvider.Weapons[id].Damage;
      float additionalDamageFromPlayerStat = _playerStatsProvider.GetStat(StatId.AdditionalDamage);
      float playerCritDamage = _playerStatsProvider.GetStat(StatId.AttackCritMultiplier);
      float playerCritChance = _playerStatsProvider.GetStat(StatId.AttackCritPercentChance);

      playerCritChance /= 100f;

      bool isCrit = false;

      float chance = _randomService.GetRandomFloat(1f);

      if (chance < playerCritChance)
        isCrit = true;

      float totalBaseDamage = baseWeaponDamage + additionalDamageFromPlayerStat;
      
      if (isCrit)
        return totalBaseDamage * playerCritDamage;

      return totalBaseDamage;
    }
  }
}