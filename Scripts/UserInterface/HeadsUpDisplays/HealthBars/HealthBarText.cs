using Core.Characters.Players;
using Meta.Stats;
using TMPro;
using UnityEngine;
using Zenject;

namespace HeadsUpDisplays.HealthBars
{
  public class HealthBarText : MonoBehaviour
  {
    public TextMeshProUGUI Text;

    [Inject] private PlayerProvider _playerProvider;
    [Inject] private PlayerStatsProvider _playerStatsProvider;

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;

      float maxHealth = _playerStatsProvider.GetStat(StatId.Health);
      float currentHealth = _playerProvider.Instance.Health.Current.Value;

      float healthPercentage = currentHealth / maxHealth * 100;

      Text.text = $"HP {healthPercentage}%";
    }
  }
}