using Core.Characters.Players;
using LevelDesign;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VisualEffects;
using Zenject;

namespace Core.Bombs
{
  public class Bomb : MonoBehaviour
  {
    [field: SerializeField] public TextMeshProUGUI TimerText { get; private set; }
    [field: SerializeField] public Slider Slider { get; private set; }
    [field: SerializeField] public TextMeshProUGUI SliderText { get; private set; }

    public float DefuseProgress { get; private set; }
    public BombSpawnMarker SpawnMarker { get; set; }

    [Inject] private VisualEffectFactory _visualEffectFactory;
    [Inject] private PlayerProvider _playerProvider;

    private void Update()
    {
      UpdateText();
      UpdateSlider();
    }

    public bool IsDefused() =>
      DefuseProgress >= 1;

    public void DefuseTick(float wholeDuration)
    {
      DefuseProgress += Time.deltaTime / wholeDuration;

      if (DefuseProgress > 1)
        DefuseProgress = 1;
    }

    public void ResetProgress()
    {
      DefuseProgress = 0;
    }

    private void UpdateText()
    {
      float timeInSeconds = SpawnMarker.BombTimerLeft;

      var secondsPerMinute = 60;
      int minutes = (int)timeInSeconds / secondsPerMinute;
      int seconds = (int)timeInSeconds % secondsPerMinute;

      TimerText.text = $"{minutes}:{seconds}";
    }

    private void UpdateSlider()
    {
      Slider.value = DefuseProgress;

      SliderText.text = Mathf.RoundToInt(DefuseProgress * 100).ToString();
    }

    public void Explode()
    {
      _visualEffectFactory.CreateAndDestroy(VisualEffectId.BombExplosion, transform.position, Quaternion.identity);

      if (!_playerProvider.Instance)
        return;

      if (Vector3.Distance(transform.position, _playerProvider.Instance.transform.position) < SpawnMarker.ExplosionRadius)
        _playerProvider.Instance.Health.TakeDamage(float.MaxValue);
    }
  }
}