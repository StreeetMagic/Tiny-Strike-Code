using Core.Weapons;
using PersistentProgresses;
using SaveLoadServices;
using Utilities;

namespace Core.Characters.Players
{
  public class PlayerWeaponIdProvider : IProgressWriter
  {
    public PlayerWeaponIdProvider()
    {
      CurrentId = new ReactiveProperty<WeaponId>
      {
        Value = WeaponId.Knife
      };

      OnCurrentIdChanged(CurrentId.Value);

      CurrentId.ValueChanged += OnCurrentIdChanged;
    }

    public ReactiveProperty<WeaponId> CurrentId { get; }

    public void ReadProgress(ProjectProgress projectProgress)
    {
      CurrentId.Value = projectProgress.CurrentPlayerWeaponId;
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      if (CurrentId.Value == WeaponId.Knife)
        return;

      projectProgress.CurrentPlayerWeaponId = CurrentId.Value;
    }

    private void OnCurrentIdChanged(WeaponId currentId)
    {
    }
  }
}