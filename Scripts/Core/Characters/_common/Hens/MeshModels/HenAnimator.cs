using UnityEngine;

namespace Core.Characters.Hens.MeshModels
{
  public class HenAnimator : MonoBehaviour
  {
    [SerializeField] private float _delayToBoom;
    [SerializeField] private float _delayToFollowTarget;
    [SerializeField] private Animator _animator;

    private static readonly int s_isMove = Animator.StringToHash("IsMove");
    private static readonly int s_isFastSpeed = Animator.StringToHash("IsFastSpeed");
    private static readonly int s_alarm = Animator.StringToHash("Alarm");
    private static readonly int s_boom = Animator.StringToHash("Boom");

    public void PlayWalkAnimation()
    {
      _animator.SetBool(s_isMove, true);
    }

    public void StopMovingAnimation()
    {
      _animator.SetBool(s_isMove, false);
      _animator.SetBool(s_isFastSpeed, false);
    }

    public void PlayFastRunAnimation()
    {
      _animator.SetBool(s_isFastSpeed, true);
    }

    public float PlayAlarmAnimation()
    {
      _animator.SetTrigger(s_alarm);
      return _delayToFollowTarget;
    }

    public float PlayBoomAnimation()
    {
      _animator.SetTrigger(s_boom);
      return _delayToBoom;
    }
  }
}