using Core.Characters.Players;
using UnityEngine;
using Zenject;

namespace Core.Characters.Hens
{
  public class HenBehaviourController : MonoBehaviour
  {
    [Inject] private PlayerProvider _playerProvider;
    
    [Inject] private HenToPlayerFollower _henToPlayerFollower;
    [Inject] private HenToTargetFollower _henToTargetFollower;
    [Inject] private HenDamageDealer _henDamageDealer;
    [Inject] private HenIdle _henIdle;

    private bool HasTarget => _playerProvider.Instance.TargetHolder.HasTarget;

    private void Update()
    {
      if (HasTarget)
      {
        _henToTargetFollower.enabled = true;
        _henDamageDealer.enabled = true;
        
        _henToPlayerFollower.enabled = false;
        _henIdle.enabled = false;
      }
      else
      {
        _henDamageDealer.enabled = false;
        
        float distanceToPlayer = Vector3.Distance(_playerProvider.Instance.transform.position, transform.position); 
        
        if (distanceToPlayer > 4f)
        {
          _henToPlayerFollower.enabled = true;
          
          _henToTargetFollower.enabled = false;
          _henIdle.enabled = false;
        }
        else
        {
          _henIdle.enabled = true;
          
          _henToTargetFollower.enabled = false;
          _henToPlayerFollower.enabled = false;
        }
      }
    }
  }
}