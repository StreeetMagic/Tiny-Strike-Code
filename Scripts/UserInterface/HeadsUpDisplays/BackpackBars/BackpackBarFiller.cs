using Meta.BackpackStorages;
using Meta.Stats;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HeadsUpDisplays.BackpackBars
{
  public class BackpackBarFiller : MonoBehaviour
  {
    public Slider Slider;
    public float SliderUpdateSpeed;

    [Inject] private PlayerStatsProvider _playerStatsProvider;
    [Inject] private BackpackStorage _backpackStorage;

    private void Start()
    {
      Slider.value = 0;
    }

    private void Update()
    {
      UpdateSlider();
    }

    private void UpdateSlider()
    {
      float max = _playerStatsProvider.GetStat(StatId.BackpackCapacity);
      float current = _backpackStorage.Volume();
      Slider.value = Mathf.MoveTowards(Slider.value, current / max, Time.deltaTime * SliderUpdateSpeed);
    }
  }
}