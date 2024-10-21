using System;
using System.Collections.Generic;
using ConfigProviders;
using PersistentProgresses;
using SaveLoadServices;
using Utilities;

namespace Meta.Expirience
{
  public class ExpierienceStorage : IProgressWriter
  {
    private readonly BalanceConfigProvider _balanceConfigProvider;
    private readonly ISaveLoadService _saveLoadService;
    private readonly PersistentProgressService _persistentProgressService;

    private int _cachedLevel;

    public ExpierienceStorage(BalanceConfigProvider balanceConfigProvider, ISaveLoadService saveLoadService, 
      PersistentProgressService persistentProgressService)
    {
      _balanceConfigProvider = balanceConfigProvider;
      _saveLoadService = saveLoadService;
      _persistentProgressService = persistentProgressService;

      AllPoints.ValueChanged += OnValueChanged;
    }

    public ReactiveProperty<int> AllPoints { get; } = new();
    public event Action<int> LevelChanged;

    public void ReadProgress(ProjectProgress projectProgress)
    {
      AllPoints.Value = projectProgress.Expierience;
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      projectProgress.Expierience = AllPoints.Value;
    }

    public int CurrentLevel()
    {
      CalculateLevelAndExperience(out int currentLevel, out _);
      return currentLevel;
    }

    public int CurrentExpierience()
    {
      CalculateLevelAndExperience(out _, out int expirienceLeft);
      return expirienceLeft;
    }

    public int ExpierienceToNextLevel()
    {
      CalculateLevelAndExperience(out int currentLevel, out int _);
      List<ExpirienceSetup> setups = Config().Levels;

      if (currentLevel >= setups.Count)
      {
        // Если достигнут максимальный уровень, возвращаем 0 или любое другое значение, которое имеет смысл в вашей логике
        return 999999;
      }

      return setups[currentLevel].Expierience;
    }

    private void OnValueChanged(int value)
    {
      if (_cachedLevel != CurrentLevel())
      {
        _cachedLevel = CurrentLevel();

        if (_cachedLevel != 1)
          LevelChanged?.Invoke(_cachedLevel);
      }

      if (AllPoints.Value != 0)
        if (_persistentProgressService.ProjectProgress.Expierience != AllPoints.Value)
          _saveLoadService.SaveProgress(ToString());
    }

    private ExpirienceConfig Config() =>
      _balanceConfigProvider.Expirience;

    private void CalculateLevelAndExperience(out int currentLevel, out int expirienceLeft)
    {
      List<ExpirienceSetup> setups = Config().Levels;

      currentLevel = 1;
      expirienceLeft = AllPoints.Value;

      while (currentLevel < setups.Count && expirienceLeft >= setups[currentLevel].Expierience)
      {
        expirienceLeft -= setups[currentLevel].Expierience;
        currentLevel++;
      }
    }
  }
}