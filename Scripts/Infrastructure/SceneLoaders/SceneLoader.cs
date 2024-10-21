using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoroutineRunners;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoaders
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;

    private readonly List<SceneId> _loadedScenes = new();

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
    }

    public event Action<SceneId> SceneLoaded;
    public List<SceneId> LoadedScenes => _loadedScenes.ToList();
    public SceneId CurrentScene { get; private set; }

    public void Load(SceneId name, Action onLoaded = null)
    {
      if (name == SceneId.Unknown)
        throw new ArgumentException(nameof(name));

      _coroutineRunner.StartCoroutine(LoadSceneAsync(name, onLoaded));
    }

    private IEnumerator LoadSceneAsync(SceneId nextScene, Action onLoaded)
    {
      AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene.ToString());
    
      if (asyncOperation != null)
      {
        asyncOperation.allowSceneActivation = true;
    
        while (asyncOperation.isDone == false)
        {
          yield return null;
        }
      }
      
      _loadedScenes.Add(nextScene);
      
      onLoaded?.Invoke();
      SceneLoaded?.Invoke(nextScene);
      CurrentScene = nextScene;
    }

    // private void LoadScene(SceneId nextScene, Action onLoaded = null)
    // {
    //   SceneManager.LoadScene(nextScene.ToString());
    //   _loadedScenes.Add(nextScene);
    //
    //   onLoaded?.Invoke();
    //   SceneLoaded?.Invoke(nextScene);
    //   CurrentScene = nextScene;
    // }
  }
}