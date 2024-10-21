using System;

namespace Utilities
{
  public static class FluentBuilder
  {
    public static T With<T>(this T self, Action<T> set)
    {
      set.Invoke(self);
      return self;
    }

    public static T With<T>(this T self, Action<T> apply, Func<bool> when)
    {
      if (when())
      {
        apply(self);
      }

      return self;
    }

    public static T With<T>(this T self, Action<T> apply, bool when)
    {
      if (when)
      {
        apply(self);
      }

      return self;
    }
  }
}