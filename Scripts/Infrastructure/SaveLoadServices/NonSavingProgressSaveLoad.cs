using System.Collections.Generic;
using System.Linq;
using PersistentProgresses;
using Projects;
using UnityEngine;

namespace SaveLoadServices
{
  public class NonSavingProgressSaveLoad : ISaveLoadService
  {
    private readonly PersistentProgressService _progressService;
    private readonly ProjectData _projectData;

    public NonSavingProgressSaveLoad(PersistentProgressService progressService, ProjectData projectData)
    {
      _progressService = progressService;
      _projectData = projectData;
    }

    public List<IProgressReader> ProgressReaders { get; } = new();

    public void SaveProgress(string saver)
    {
      UpdateProgressWriters();
    }

    public void LoadProgress()
    {
      ReadPlayerPrefs();
      UpdateProgressReaders();
    }

    public void DeleteSaves()
    {
      PlayerPrefs.DeleteAll();
    }

    private void UpdateProgressReaders()
    {
      foreach (IProgressReader progressReader in ProgressReaders)
        progressReader.ReadProgress(_progressService.ProjectProgress);
    }

    private void UpdateProgressWriters() =>
      ProgressReaders
        .OfType<IProgressWriter>()
        .ToList()
        .ForEach(progressWriter => progressWriter.WriteProgress(_progressService.ProjectProgress));

    private void ReadPlayerPrefs()
    {
      if (PlayerPrefs.HasKey(ProgressKey()))
        _progressService.LoadProgress(PlayerPrefs.GetString(ProgressKey()));
      else
        _progressService.SetDefault();
    }

    private string ProgressKey() =>
      $"{_projectData.ConfigId}_progress";
  }
}