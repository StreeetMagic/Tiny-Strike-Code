using Core.AimObstacles;
using Core.Characters.Players;
using Core.Weapons;
using UnityEngine;
using Zenject;

namespace Core.Characters.Mentors
{
  public class AimObstaclePopUp : MonoBehaviour
  {
    public AimObstacle AimObstacle;
    public WeaponId Weapon = WeaponId.Ak47;

    [SerializeField] private GameObject _panel;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _distanceToPlayer;

    [Inject] private PlayerProvider _playerProvider;

    private bool _isCloseAnimation;
    private static readonly int s_close = Animator.StringToHash("Close");

    private void Update()
    {
      if (AimObstacle is null)
        return;

      if (AimObstacle.Installer.Health.Current.Value <= 0)
      {
        Destroy(gameObject);
        return;
      }

      if (_playerProvider.Instance.WeaponIdProvider.CurrentId.Value == Weapon)
        return;

      if (!_playerProvider.Instance)
        return;

      if (Vector3.Distance(transform.position, _playerProvider.Instance.transform.position) < _distanceToPlayer)
      {
        _panel.SetActive(true);
        _isCloseAnimation = false;
      }
      else
      {
        ClosePopup();

        if (_panel.GetComponent<RectTransform>().localScale == Vector3.zero)
          _panel.SetActive(false);
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