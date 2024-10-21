using System;
using UnityEngine;
using WindowBackgrounds;

[Serializable]
public class PickUpWindowArtSetup : ArtSetup<PickUpWindowId>
{
  public WindowBackground WindowBackground;
  public Sprite TitleSprite;
}