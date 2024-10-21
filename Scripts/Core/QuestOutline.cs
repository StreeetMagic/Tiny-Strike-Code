using UnityEngine;

namespace Core
{
  public class QuestOutline : MonoBehaviour
  {
    // ReSharper disable once InconsistentNaming
    public GameObject _arrow;

    private void OnEnable()
    {
      _arrow.SetActive(true);
    }

    private void OnDisable()
    {
      _arrow.SetActive(false);
    }
  }
}