using PersistentProgresses;

namespace SaveLoadServices
{
  public interface IProgressReader
  {
    void ReadProgress(ProjectProgress projectProgress);
  }
}