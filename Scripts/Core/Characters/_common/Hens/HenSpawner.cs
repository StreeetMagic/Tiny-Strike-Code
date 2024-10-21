using System.Collections.Generic;
using UnityEngine;

namespace Core.Characters.Hens
{
  public class HenSpawner
  {
    private readonly List<Hen> _hens = new();
    private readonly HenFactory _henFactory;

    public HenSpawner(HenFactory henFactory)
    {
      _henFactory = henFactory;
    }
    
    public int Count => _hens.Count;

    public void Spawn()
    {
      _hens.Add(_henFactory.Create());
    }

    public void DeSpawn(Hen hen)
    {
      _hens.Remove(hen);
      Object.Destroy(hen.gameObject);
    }

    public void DeSpawnAll()
    {
      foreach (Hen hen in _hens)
      {
        Object.Destroy(hen.gameObject);
      }

      _hens.Clear();
    }
  }
}