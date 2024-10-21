using System.Collections.Generic;

namespace SaveLoadServices
{
  public interface ISaveLoadService
  {
    List<IProgressReader> ProgressReaders { get; }
    void SaveProgress(string saver);
    void LoadProgress();
    void DeleteSaves();
  }
}