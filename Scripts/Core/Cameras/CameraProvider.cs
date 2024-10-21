using UnityEngine;

namespace Core.Cameras
{
  public class CameraProvider
  {
    public Camera MainCamera { get; set; }
    public TopDownCamera TopCamera { get; set; }
    public TopDownCamera BotCamera { get; set; }
  }
}