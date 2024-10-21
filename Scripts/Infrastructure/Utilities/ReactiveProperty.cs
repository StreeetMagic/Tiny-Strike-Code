using System;

namespace Utilities
{
  [Serializable]
  public class ReactiveProperty<T>
  {
    private T _value;

    public ReactiveProperty()
    {
      Value = default;
    }

    public ReactiveProperty(T value)
    {
      Value = value;
    }

    public event Action<T> ValueChanged;

    public T Value
    {
      get => _value;

      set
      {
        _value = value;

        ValueChanged?.Invoke(_value);
      }
    }
  }
}