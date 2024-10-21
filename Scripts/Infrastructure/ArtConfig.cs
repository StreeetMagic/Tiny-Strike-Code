using UnityEngine;

public abstract class ArtConfig<T> : ScriptableObject where T : class
{
  protected abstract void Validate();

  private void OnValidate()
  {
    Validate();
  }

  public T[] Setups;
}