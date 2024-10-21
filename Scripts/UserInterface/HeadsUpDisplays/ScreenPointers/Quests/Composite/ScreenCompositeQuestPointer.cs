using Meta;
using Zenject;

namespace HeadsUpDisplays.ScreenPointers
{
	public class ScreenCompositeQuestPointer : ScreenQuestPointer
	{
		[Inject] public CompositeQuestId QuestId { get; private set; }

		[Inject] private CompositeQuestStorage _questStorage;
		[Inject] private CompositeQuestTargetsProvider _targetsProvider;

		private void Update()
		{
			if (!MapProvider.Map)
			{
				Hide();
				return;
			}

			if (!MapProvider.Map.GetClosestBaseTrigger())
			{
				Hide();
				return;
			}

			Targets = _targetsProvider.GetTargetsOrNull(QuestId);
			
			if (Targets == null)
			{
				return;
			}

			if (Targets.Length == 0)
			{
				return;
			}

			if (Targets[0] == null)
			{
				return;
			}

			SwitchMarks(_questStorage.Get(QuestId).State.Value);
			UpdatePointer(MainCamera.WorldToScreenPoint(Targets[0].position));

			Clear();
			Targets = _targetsProvider.GetTargetsOrNull(QuestId);
			SetClosestTarget();
		}
	}
}