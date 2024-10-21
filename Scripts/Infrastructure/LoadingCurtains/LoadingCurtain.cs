using System.Collections;
using UnityEngine;

namespace LoadingCurtains
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide()
    {
      StartCoroutine(DoFadeIn());
    }

    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
      
      gameObject.SetActive(false);
    }
  }
}