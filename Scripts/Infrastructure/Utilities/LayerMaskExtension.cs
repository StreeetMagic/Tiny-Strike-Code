using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
  public static class LayerMaskExtension
  {
    public static int Inverse(this int mask) =>
      ~mask;

    public static int Combined(this int mask, int other) =>
      mask | other;

    public static bool Includes(this int mask, int value) =>
      (1 << value & mask) != 0;

    public static bool Includes(this int mask, string layerName) =>
      mask.Includes(LayerMask.NameToLayer(layerName));

    public static bool IncludesAll(this int mask, IEnumerable<int> values)
    {
      foreach (int layer in values)
      {
        if (!mask.Includes(layer)) return false;
      }

      return true;
    }

    public static bool IncludesAny(this int mask, IEnumerable<int> values)
    {
      foreach (int layer in values)
      {
        if (mask.Includes(layer)) return true;
      }

      return false;
    }

    public static LayerMask Inverse(this LayerMask layerMask) =>
      ~layerMask;

    public static LayerMask Combined(this LayerMask layerMask, LayerMask other) =>
      layerMask | other;

    public static bool Includes(this LayerMask layerMask, int value) =>
      layerMask.value.Includes(value);

    public static bool Includes(this LayerMask layerMask, string layerName) =>
      layerMask.value.Includes(layerName);

    public static bool IncludesAll(this LayerMask layerMask, IEnumerable<int> values) =>
      layerMask.value.IncludesAll(values);

    public static bool IncludesAny(this LayerMask layerMask, IEnumerable<int> values) =>
      layerMask.value.IncludesAny(values);
  }
}