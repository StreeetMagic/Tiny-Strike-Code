using System;
using DevConfigs;

namespace RandomServices
{
  public class RandomService
  {
    private static readonly Random s_random = new Random();
    
    public int GetRandomInt(int min, int max)
    {
      return s_random.Next(min, max);
    }
    
    public int GetRandomInt(int max)
    {
      return s_random.Next(max);
    }

    public float GetRandomFloat(float max)
    {
      return (float)s_random.NextDouble() * max;
    }

    public float GetRandomFloat(float min, float max)
    {
      return (float)s_random.NextDouble() * (max - min) + min;
    }

    public string GetRandomUniqueId()
    {
      return Guid
        .NewGuid()
        .ToString();
    }

    public float Modify(float value)
    {
      float percent = DevConfig.RandomValuesPercentDelta;
      
      float randomPercent = GetRandomFloat(1f - percent, 1f + percent);
      
      return value * randomPercent;
    }
  }
}