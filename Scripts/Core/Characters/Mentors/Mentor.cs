using AudioServices;
using AudioServices.Sounds;
using Core.Characters.Players;
using UnityEngine;
using Zenject;

namespace Core.Characters.Mentors
{
  [SelectionBase]
  public class Mentor : MonoBehaviour
  {
    [SerializeField] private GameObject _probs;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _distanceToPlayer;

    [Inject] private AudioService _audioService;
    [Inject] private PlayerProvider _playerProvider;

    private bool _isCloseAnimation;
    private static readonly int s_close = Animator.StringToHash("Close");
    private bool _isPlaying;
    
    private void Start()
    {
      _panel.SetActive(true);
      _panel.SetActive(false);
    }

    private void Update()
    {
      if (!_playerProvider.Instance)
        return;

      if (Vector3.Distance(transform.position, _playerProvider.Instance.transform.position) < _distanceToPlayer)
      {
        _panel.SetActive(true);
        _isCloseAnimation = false;

        if (!_isPlaying)
        {
          _isPlaying = true;
          _audioService.Play(SoundId.Voice);
        }

        if (_probs)
          _probs.SetActive(false);
      }
      else
      {
        ClosePopup();

        if (_isPlaying)
        {
          _isPlaying = false;
        }

        if (_panel.GetComponent<RectTransform>().localScale == Vector3.zero)
          _panel.SetActive(false);

        if (_probs)
          _probs.SetActive(true);
      }
    }

    private void ClosePopup()
    {
      if (_isCloseAnimation)
        return;

      if (_animator.isActiveAndEnabled)
        _animator.SetTrigger(s_close);

      _isCloseAnimation = true;
    }
  }
}