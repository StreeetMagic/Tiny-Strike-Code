using System.Collections.Generic;
using Core.Characters.Enemies.States.Bootstrap;
using Core.Spawners.Enemies;
using UnityEngine;
using Zenject.Source.Factories;

namespace Core.Characters.Enemies
{
  [SelectionBase]
  public class Enemy : MonoBehaviour
  {
    public EnemyInstaller Installer { get; set; }

    private void OnEnable()
    {
      if (Installer)
        Installer.FiniteStateMachine.OnEntered(typeof(EnemyBootstrapState));
    }

    public class Factory : PlaceholderFactory<EnemyConfig, List<Transform>, EnemySpawner, bool, Enemy>
    {
    }
  }
}