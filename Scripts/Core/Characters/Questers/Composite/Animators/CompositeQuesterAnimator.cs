using UnityEngine;

namespace Core.Characters.Questers.Animators
{
  public class CompositeQuesterAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;

    private static readonly int s_talk = Animator.StringToHash(IsTalk);

    private const string IsTalk = nameof(IsTalk);

    public void PlayTalkAnimation()
    {
      _animator.SetBool(s_talk, true);
    }

    public void StopTalkAnimation()
    {
      _animator.SetBool(s_talk, false);
    }
  }
}