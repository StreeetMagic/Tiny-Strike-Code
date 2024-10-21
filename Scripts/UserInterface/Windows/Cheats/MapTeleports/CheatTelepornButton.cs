using Core.Characters.Players;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cheats.MapTeleports
{
  public class CheatTelepornButton : MonoBehaviour
  {
    private int _index;
    private Vector3[] _teleports;

    [Inject] private PlayerProvider _playerProvider;

    public void Init(int index, Vector3[] teleports)
    {
      _index = index;
      _teleports = teleports;
    }

    private void Start()
    {
      GetComponent<Button>().onClick.AddListener(() => TeleportTo(_index));
    }

    private void TeleportTo(int number)
    {
      _playerProvider.Instance.NavMeshAgent.Warp(_teleports[number - 1]);
    }
  }
}