using UnityEngine;

namespace Core
{
  public class TransformContainer
  {
    public TransformContainer(Transform transform)
    {
      Transform = transform;
    }

    public Transform Transform { get; set; }
  }
}