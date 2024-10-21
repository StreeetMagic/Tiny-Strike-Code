using System;
using System.Collections.Generic;
using Core.Characters;
using Core.LootSlots;
using Core.PickUpTreasures;
using Core.Weapons;
using Meta;
using Meta.Loots;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Core.AimObstacles
{
  [SelectionBase]
  public class AimObstacle : MonoBehaviour
  {
    public const string Quest = nameof(Quest);

    public AimObstacleId Id;
    public TargetPriority TargetPriority;
    public float InitialHealth;
    public int Expirience;
    public float CorpseRemoveDelay;
    public List<LootDrop> LootDrops;
    public bool ShowLoot = true;

    [Tooltip("ЕСЛИ ПУСТО - разрешено ВСЕ. Список оружия, из которого можно уничтожить этот AimObstacle")]
    public List<WeaponId> WeaponWhiteList;

    [Inject] private LootSlotFactory _lootSlotFactory;
    [Inject] private SimpleQuestStorage _simpleQuestStorage;
    [Inject] private CompositeQuestStorage _compositeQuestStorage;
    [Inject] private ITargetTrigger _targetTrigger;

    [field: FoldoutGroup(Quest)]
    [field: SerializeField]
    public bool IsQuestTarget { get; private set; } = true;

    [field: FoldoutGroup(Quest)]
    [field: ShowIf(nameof(IsQuestTarget))]
    [field: SerializeField]
    public QuestId QuestId { get; private set; }

    [field: FoldoutGroup(Quest)]
    [field: ShowIf(nameof(IsSimpleQuestType))]
    [field: SerializeField]
    [Tooltip("Активируйте симпл квест для возможности уничтожения. Unknown если доступно всегда")]
    public SimpleQuestId SimpleQuestToDestroy { get; private set; } = SimpleQuestId.Unknown;

    [field: FoldoutGroup(Quest)]
    [field: ShowIf(nameof(IsCompositeQuestType))]
    [field: SerializeField]
    [field: Tooltip("Активируйте композит квест для возможности уничтожения. Unknown если доступно всегда")]
    public CompositeQuestId CompositeQuestToDestroy { get; private set; } = CompositeQuestId.Unknown;

    [field: Space]
    [field: SerializeField]
    [field: Tooltip("Активируйте симпл квест для возможности уничтожения. Unknown если доступно всегда")]
    public PickUpTreasureId PickUpTreasure { get; private set; } = PickUpTreasureId.Unknown;
    
    [Tooltip("Уничтожаем после дропа pickUpTreasure")] 
    public bool PickUpTreasureDestroyAfterTime = true;
    
    [Tooltip("Время уничтожения pickUpTreasure")] 
    public float PickUpTreasureDestroyTimer = 10f;

    public AimObstacleInstaller Installer { get; set; }

    private void Awake()
    {
      if (InitialHealth <= 0)
      {
        throw new ArgumentException("InitialHealth must be greater than zero");
      }

      if (Id == AimObstacleId.Unknown)
      {
        throw new ArgumentException("Id must be set");
      }
    }

    private void Start()
    {
      if (ShowLoot)
      {
        Transform lootSlotContainer = GetComponentInChildren<LootSlotsContainer>().transform;
        _lootSlotFactory.Create(lootSlotContainer, LootDrops);
      }
    }

    private void Update()
    {
      if (IsQuestTarget == false)
      {
        _targetTrigger.IsTargetable = true;
      }
      else
      {
        if (QuestId == QuestId.Unknown)
        {
          _targetTrigger.IsTargetable = true;
        }
        else
        {
          if (QuestId == QuestId.Simple)
          {
            if (SimpleQuestToDestroy == SimpleQuestId.Unknown)
            {
              _targetTrigger.IsTargetable = true;
            }
            else
            {
              if (_simpleQuestStorage.Get(SimpleQuestToDestroy).State.Value != QuestState.UnActivated)
                _targetTrigger.IsTargetable = true;
              else
                _targetTrigger.IsTargetable = false;
            }
          }
          else if (QuestId == QuestId.Composite)
          {
            if (CompositeQuestToDestroy == CompositeQuestId.Unknown)
            {
              _targetTrigger.IsTargetable = true;
            }
            else
            {
              if (_compositeQuestStorage.Get(CompositeQuestToDestroy).State.Value != QuestState.UnActivated)
                _targetTrigger.IsTargetable = true;
              else
                _targetTrigger.IsTargetable = false;
            }
          }
        }
      }
    }

    private void OnValidate()
    {
      if (SimpleQuestToDestroy != SimpleQuestId.Unknown && CompositeQuestToDestroy != CompositeQuestId.Unknown)
      {
        SimpleQuestToDestroy = SimpleQuestId.Unknown;
        CompositeQuestToDestroy = CompositeQuestId.Unknown;
      }

      if (QuestId == QuestId.Unknown)
      {
        SimpleQuestToDestroy = SimpleQuestId.Unknown;
        CompositeQuestToDestroy = CompositeQuestId.Unknown;
      }
    }

    private bool IsSimpleQuestType()
      => QuestId == QuestId.Simple;

    private bool IsCompositeQuestType()
      => QuestId == QuestId.Composite;
  }
}