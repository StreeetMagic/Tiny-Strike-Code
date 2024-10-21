using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class HealthPercentText : MonoBehaviour
  {
    public TextMeshProUGUI Text;

    [Inject] private IHealth _enemyHealth;
    [Inject] private EnemyConfig _config;

    private float MaxHealth => _config.InitialHealth;

    private void Start()
    {
      UpdateText(_enemyHealth.Current.Value);
      _enemyHealth.Current.ValueChanged += UpdateText;
    }

    private void OnDestroy()
    {
      _enemyHealth.Current.ValueChanged -= UpdateText;
    }

    private void UpdateText(float current)
    {
      Text.text = Mathf.RoundToInt(current / MaxHealth * 100) + "%";
    }
  }
}