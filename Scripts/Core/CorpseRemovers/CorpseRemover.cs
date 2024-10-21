// using System.Collections;
// using System.Collections.Generic;
// using Core.Characters.Enemies;
// using CoroutineRunners;
// using UnityEngine;
// using Utilities;
// using Object = UnityEngine.Object;

namespace Core.CorpseRemovers
{
  public class CorpseRemover
  {
    // public List<IHealth> Enemies { get; } = new();
    //
    // private readonly ICoroutineRunner _coroutineRunner;
    //
    // public CorpseRemover(ICoroutineRunner coroutineRunner)
    // {
    //   _coroutineRunner = coroutineRunner;
    // }
    //
    // public void Add(IHealth enemyHealth)
    // {
    //   Enemies.Add(enemyHealth);
    //   enemyHealth.Died += OnDied;
    // }
    //
    // private void OnDied(IHealth health, int expirience, float corpseRemoveDelay)
    // {
    //   health.Died -= OnDied;
    //
    //   var coroutineDecorator = new CoroutineDecorator(_coroutineRunner, () => RemoveCorpse(health, corpseRemoveDelay));
    //   coroutineDecorator.Start();
    // }
    //
    // private IEnumerator RemoveCorpse(IHealth enemyHealth, float corpseRemoveDelay)
    // {
    //   yield return new WaitForSeconds(corpseRemoveDelay);
    //   
    //   if (enemyHealth == null)
    //     yield break;
    //
    //   if (!enemyHealth.transform)
    //     yield break;
    //
    //   enemyHealth.transform.gameObject.SetActive(false);
    //   
    //   Enemies.Remove(enemyHealth);
    // }
  }
}