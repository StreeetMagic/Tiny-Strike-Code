using TMPro;
using UnityEngine;
using Zenject;

namespace Core.AimObstacles
{
  [RequireComponent(typeof(TextMeshProUGUI))]
  public class AimObstacleHealthPercentText : MonoBehaviour
  {
    private TextMeshProUGUI _text;

    [Inject] private IHealth _enemyHealth;
    [Inject] private AimObstacle _aimObstacle;

    private float MaxHealth => _aimObstacle.InitialHealth;

    private void Awake()
    {
      _text = GetComponent<TextMeshProUGUI>(); 
    }

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
      _text.text = Mathf.RoundToInt(current / MaxHealth * 100) + "%";
    }
  }
}