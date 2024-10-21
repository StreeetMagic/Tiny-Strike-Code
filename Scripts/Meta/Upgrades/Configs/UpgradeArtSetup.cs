using System;
using Meta.Stats;
using UnityEngine;

namespace Meta.Upgrades.Configs
{
  [Serializable]
  public class UpgradeArtSetup : ArtSetup<StatId>
  {
    [Tooltip("Название апгрейда")] 
    public string Title;

    [Tooltip("Описание апгрейда")] 
    public string Description;

    [Tooltip("Иконка")] 
    public Sprite Icon;
  }
}