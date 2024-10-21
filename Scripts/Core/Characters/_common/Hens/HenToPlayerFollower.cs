using Core.Characters.Hens.MeshModels;
using Core.Characters.Players;
using Meta.Stats;
using UnityEngine;
using Zenject;

namespace Core.Characters.Hens
{
  public class HenToPlayerFollower : MonoBehaviour
  {
    [Inject] private HenMover _henMover;
    [Inject] private PlayerStatsProvider _playerStatsProvider;
    [Inject] private PlayerProvider _playerProvider;
    [Inject] private HenRotator _henRotator;
    [Inject] private HenAnimator _henAnimator;

    private float MoveSpeed => _playerStatsProvider.GetStat(StatId.MoveSpeed);
    private Transform Player => _playerProvider.Instance.transform;

    private void Awake()
    {
      enabled = false;
    }

    private void OnEnable()
    {
      _henAnimator.PlayWalkAnimation();
    }

    private void Update()
    {
      _henMover.Move(Player.position, MoveSpeed);
      _henRotator.Rotate(Player.position);
    }
  }
}