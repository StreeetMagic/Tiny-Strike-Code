using ConfigProviders;
using UnityEngine;

namespace Core.Characters.Players
{
  public class PlayerRotator
  {
    private readonly BalanceConfigProvider _static;
    private readonly PlayerProvider _playerProvider;

    public PlayerRotator(BalanceConfigProvider balanceConfigProvider, PlayerProvider playerProvider
    )
    {
      _static = balanceConfigProvider;
      _playerProvider = playerProvider;
    }

    public void RotateTowardsDirection(Vector3 direction)
    {
      const float MinLength = 0.01f;

      if (direction.sqrMagnitude < MinLength)
        return;

      if (direction == Vector3.zero)
        return;

      Quaternion targetRotation = Quaternion.LookRotation(direction);
      _playerProvider.Instance.transform.rotation = Quaternion.Slerp(_playerProvider.Instance.transform.rotation, targetRotation, Time.deltaTime * _static.Player.RotationSpeed);
    }
    
    public void RotateImminately(Vector3 direction)
    {
      _playerProvider.Instance.transform.rotation = Quaternion.LookRotation(direction);
    }
  }
}