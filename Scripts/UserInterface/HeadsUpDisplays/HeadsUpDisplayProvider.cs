using HeadsUpDisplays.BackpackBars;
using HeadsUpDisplays.ResourcesSenders;
using UnityEngine;

namespace HeadsUpDisplays
{
  public class HeadsUpDisplayProvider
  {
    public RectTransform CanvasTransform { get; set; }
    public HeadsUpDisplay HeadsUpDisplay { get; set; }
    
    public Borders Borders { get; set; }
    //public UltimateJoystick FloatingJoystick { get; set; }
    public LootSlotsUpdater LootSlotsUpdater { get; set; }
    public BackpackBarFiller BackpackBarFiller { get; set; }
    public BaseTriggerTarget BaseTriggerTarget { get; set; }
    public ResourcesSendersContainer ResourcesSendersContainer { get; set; }
    public BackPackBar BackpackBar { get; set; }
    public MoneySender MoneySender { get; set; }
  }
}