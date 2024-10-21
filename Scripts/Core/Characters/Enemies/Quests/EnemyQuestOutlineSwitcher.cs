using Meta;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class EnemyQuestOutlineSwitcher : MonoBehaviour
  {
    [Inject] private SimpleQuestStorage _simpleQuestStorage;
    [Inject] private CompositeQuestStorage _compositeStorage;
    [Inject] private EnemyConfig _config;
    [Inject] private IHealth _health;
    
    private QuestOutline _outline;
    private SimpleQuestId _simpleQuestId;
    private CompositeQuestId _compositeQuestId;

    private void Awake()
    {
      _outline = transform.GetComponentInChildren<QuestOutline>();

      switch (_config.Id)
      {
        case EnemyId.TerTutorialBoss:
          _simpleQuestId = SimpleQuestId.KillTutorialBoss;
          break;

        case EnemyId.Hen:
          _compositeQuestId = CompositeQuestId.KillHens;
          break;

        case EnemyId.ZombieFast:
          _compositeQuestId = CompositeQuestId.KillZombieFast;
          break;

        case EnemyId.TerKnife:
          _compositeQuestId = CompositeQuestId.KillTerKnife;
          break;

        case EnemyId.TerKnifeStrong:
          _compositeQuestId = CompositeQuestId.KillTerKnifeStrong;
          break;

        case EnemyId.TerGun:
          _compositeQuestId = CompositeQuestId.KillTerGun;
          break;

        case EnemyId.TerSniper:
          _compositeQuestId = CompositeQuestId.KillTerSniper;
          break;

        case EnemyId.TerGrenade:
          _compositeQuestId = CompositeQuestId.KillTerGrenade;
          break;

        case EnemyId.TerBatMelee:
          _compositeQuestId = CompositeQuestId.KillTerBatMelee;
          break;

        case EnemyId.TerAk47:
          _compositeQuestId = CompositeQuestId.KillTerAk47;
          break;

        case EnemyId.Rooster:
          _compositeQuestId = CompositeQuestId.KillRoosters;
          break;

        case EnemyId.TerShotgun:
          _compositeQuestId = CompositeQuestId.KillTerShotgun;
          break;

        case EnemyId.ZombieTank:
          _compositeQuestId = CompositeQuestId.KillZombieTank;
          break;

        case EnemyId.ZombiePolice:
          _compositeQuestId = CompositeQuestId.KillZombiePolice;
          break;

        default:
        case EnemyId.Unknown:
          _simpleQuestId = SimpleQuestId.Unknown;
          break;
      }
    }

    public void Update()
    {
      if (_health.IsDead)
      {
        _outline.gameObject.SetActive(false);
        return;
      }

      bool hasActiveQuests = false;

      if (_simpleQuestId != SimpleQuestId.Unknown)
        hasActiveQuests = _simpleQuestStorage.Get(_simpleQuestId).State.Value == QuestState.Activated;

      if (_compositeQuestId != CompositeQuestId.Unknown)
        hasActiveQuests |= _compositeStorage.Get(_compositeQuestId).State.Value == QuestState.Activated;

      _outline.gameObject.SetActive(hasActiveQuests);
    }
  }
}