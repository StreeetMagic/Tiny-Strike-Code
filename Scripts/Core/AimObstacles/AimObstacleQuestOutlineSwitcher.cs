using System;
using Meta;
using Zenject;

namespace Core.AimObstacles
{
  public class AimObstacleQuestOutlineSwitcher : ITickable
  {
    private readonly AimObstacle _aimObstacle;
    private readonly QuestOutline _outline;
    private readonly SimpleQuestStorage _simpleQuestStorage;
    private readonly CompositeQuestStorage _compositeQuestStorage;
    private readonly IHealth _health;

    public AimObstacleQuestOutlineSwitcher(AimObstacle aimObstacle, QuestOutline outline,
      SimpleQuestStorage simpleQuestStorage, IHealth health, CompositeQuestStorage compositeQuestStorage)
    {
      _aimObstacle = aimObstacle;
      _outline = outline;
      _simpleQuestStorage = simpleQuestStorage;
      _health = health;
      _compositeQuestStorage = compositeQuestStorage;
    }

    public void Tick()
    {
      if (!_aimObstacle)
      {
        Hide();
        return;
      }

      if (_health.Current.Value <= 0)
      {
        Hide();
        return;
      }

      switch (_aimObstacle.Id)
      {
        case AimObstacleId.CrushBox:
          CrushBox();
          break;

        case AimObstacleId.Barrel:
        case AimObstacleId.Dummy:
        case AimObstacleId.MailBox:
          Hide();
          return;

        default:
        case AimObstacleId.Unknown:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void Show() =>
      _outline.gameObject.SetActive(true);

    private void Hide() =>
      _outline.gameObject.SetActive(false);

    private void CrushBox()
    {
      bool isDestroyCrushBoxQuestActive = _simpleQuestStorage.Get(SimpleQuestId.DestroyCrushBox).State.Value == QuestState.Activated;
      bool isDestroyBoxQuestActive = _compositeQuestStorage.Get(CompositeQuestId.DestroyCrushBox).State.Value == QuestState.Activated;

      bool hasActiveQuests = isDestroyCrushBoxQuestActive || isDestroyBoxQuestActive;

      if (hasActiveQuests)
        Show();
      else
        Hide();
    }
  }
}