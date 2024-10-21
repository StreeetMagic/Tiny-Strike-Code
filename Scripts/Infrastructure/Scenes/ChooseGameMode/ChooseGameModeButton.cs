using Projects;
using SceneLoaders;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scenes.ChooseGameMode
{
  public class ChooseGameModeButton : MonoBehaviour
  {
    // ReSharper disable once InconsistentNaming
    public ConfigId configId;
    public SceneId SceneId;

    [Inject] private SceneLoader _sceneLoader;
    [Inject] private ProjectData _projectData;
    
    private Button _button;

    private void Start()
    {
      _button = GetComponent<Button>();
      
      _button.onClick.AddListener(() =>
      {
        _projectData.ConfigId = configId; 
        _projectData.InitialSceneId = SceneId; 
        
        _sceneLoader.Load(SceneId.LoadConfigs);
      });
    }
  }
}