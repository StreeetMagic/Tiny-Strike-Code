using UnityEngine;

namespace CoroutineRunners
{
  public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
  {
    public void Dispose()
    {
      StopAllCoroutines();
    }
  }
}