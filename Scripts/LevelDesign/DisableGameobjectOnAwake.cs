using UnityEngine;

namespace LevelDesign
{
  public class DisableGameobjectOnAwake : MonoBehaviour
  {
    private void Awake()
    {
      gameObject.SetActive(false);
    }
  }
}