using Core.Characters.Players;
using Loggers;
using UnityEngine;
using Zenject;

namespace Core.Shortcuts
{
  public class Shortcut : MonoBehaviour
  {
    [Inject] private PlayerProvider _playerProvider;

    public float DistanceToPlayer = 1f;
    public float ActivationDelay = 1f;
    public Transform WarpPosition;

    private float _timeLeft;

    private void Awake()
    {
      if (!WarpPosition)
        new DebugLogger().LogError("Shortcut: WarpPosition is null. Не назначена точка перемещения в скрипте Shortcut");
    }

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;

      if (Vector3.Distance(transform.position, _playerProvider.Instance.transform.position) > DistanceToPlayer)
      {
        _timeLeft = ActivationDelay;
      }
      else
      {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0)
        {
          _playerProvider.Instance.Mover.Warp(WarpPosition.position);
        }
      }
    }
  }
}