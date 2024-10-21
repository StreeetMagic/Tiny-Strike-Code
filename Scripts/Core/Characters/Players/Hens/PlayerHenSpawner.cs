using Core.Characters.Hens;
using UnityEngine;
using Zenject;

namespace Core.Characters.Players
{
  public class PlayerHenSpawner : ITickable
  {
    private const float CoolDown = 2f;

    private readonly HenSpawner _henSpawner;

    private float _coolDownTimer;
    private int _count;

    public PlayerHenSpawner(HenSpawner henSpawner)
    {
      _henSpawner = henSpawner;

      _coolDownTimer = CoolDown;
      _count = 0;
    }

    public void Tick()
    {
      if (_henSpawner.Count != 0)
        return;

      if (_coolDownTimer > 0)
      {
        _coolDownTimer -= Time.deltaTime;
      }
      else
      {
        if (_count <= 0)
          return;

        _count--;
        _coolDownTimer = CoolDown;

        _henSpawner.Spawn();
      }
    }
  }
}