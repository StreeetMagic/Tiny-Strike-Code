using Windows;
using AudioServices;
using Core.Characters.Players;
using UnityEngine;
using Zenject;

namespace Core.Characters.Questers
{
  public abstract class Quester : MonoBehaviour
  {
    public float DistanceToPlayer = 3f;
    public float TimeToOpenWindow = 1f;
    public GameObject Counter;
    public GameObject ExclamationMark;
    public GameObject QuestionMark;
    public GameObject[] All;

    protected float TimeLeft;
    protected bool Opened;
    protected bool IsPlaying;

    [Inject] protected PlayerProvider PlayerProvider;
    [Inject] protected WindowService WindowService;
    [Inject] protected AudioService AudioService;

    public bool IsActive;

    private void Update()
    {
      ShowIfActive();
      OpenWindow();
      TryDestroy();
      ToggleCounter();
      ToggleMarks();
      Activate();
    }

    protected abstract void ToggleMarks();
    
    protected abstract void OpenWindow();
    protected abstract void TryDestroy();
    protected abstract void ToggleCounter();
    protected abstract void Activate();

    private void ShowIfActive()
    {
      if (IsActive)
      {
        foreach (GameObject prob in All)
        {
          if (!prob.activeSelf)
            prob.SetActive(true);
        }
      }
      else
      {
        {
          foreach (GameObject prob in All)
          {
            if (prob.activeSelf)
              prob.SetActive(false);
          }
        }
      }
    }
  }
}