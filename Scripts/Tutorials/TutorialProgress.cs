using System;

namespace Tutorials
{
  [Serializable]
  public class TutorialProgress
  {
    public TutorialState State;

    public TutorialProgress(TutorialState state)
    {
      State = state;
    }
  }
}