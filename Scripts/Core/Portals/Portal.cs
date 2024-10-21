using System;
using Core.Characters.Players;
using Projects;
using SceneLoaders;
using Scenes;
using UnityEngine;
using Zenject;

namespace Core.Portals
{
  [SelectionBase]
  public class Portal : MonoBehaviour
  {
    public PortalTypeId TypeId;
    public SceneId ToScene;

    [Tooltip("Время в секудах до активации после завершения арены")]
    public float ActivationDelay = 10f;

    [Tooltip("Время в секудах до активации при входа в триггер")]
    public float EnterDelay = 1f;

    [Inject] private SceneLoader _sceneLoader;
    [Inject] private ProjectData _projectData;

    private ParticleSystem _particleSystem;

    private bool _playerInTrigger;
    private bool _isActive;
    private float _timeLeftForActivation;
    private float _timeLeftForEnter;

    private void Awake()
    {
      _particleSystem = GetComponentInChildren<ParticleSystem>();
      Validate();

      Activate();
    }

    private void Update()
    {
      if (_isActive == false)
      {
        if (_timeLeftForActivation > 0)
          _timeLeftForActivation -= Time.deltaTime;
        else
          Activate();
      }

      if (_playerInTrigger)
      {
        if (_timeLeftForEnter > 0)
          _timeLeftForEnter -= Time.deltaTime;
        else
          LoadScene();
      }
      else
      {
        _timeLeftForEnter = EnterDelay;
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      if (_isActive == false)
        return;

      if (_playerInTrigger)
        return;

      if (other.TryGetComponent(out PlayerTargetTrigger _))
      {
        _playerInTrigger = true;
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (_isActive == false)
        return;

      if (!_playerInTrigger)
        return;

      if (other.TryGetComponent(out PlayerTargetTrigger _))
      {
        _playerInTrigger = false;
      }
    }

    private void Activate()
    {
      _isActive = true;
      _particleSystem.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
      _isActive = false;
      _particleSystem.gameObject.SetActive(false);
      _timeLeftForActivation = ActivationDelay;
    }

    private void LoadScene()
    {
      switch (TypeId)
      {
        case PortalTypeId.Unknown:
          throw new ArgumentOutOfRangeException();

        case PortalTypeId.CoreToArena:
          _sceneLoader.Load(ToScene);
          break;

        case PortalTypeId.ArenaToCore:
          switch (_projectData.ConfigId)
          {
            case ConfigId.Unknown:
              throw new ArgumentOutOfRangeException();

            case ConfigId.Default:
              _sceneLoader.Load(ToScene);
              break;

            case ConfigId.Vlad:
              _sceneLoader.Load(SceneId.VladHubTest);
              break;

            case ConfigId.Vova:
              _sceneLoader.Load(SceneId.VovaHubTest);
              break;

            case ConfigId.Valera:
              _sceneLoader.Load(SceneId.ValeraHubTest);
              break;

            default:
              throw new ArgumentOutOfRangeException();
          }

          break;

        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void Validate()
    {
      if (TypeId == PortalTypeId.Unknown || ToScene == SceneId.Unknown)
        throw new Exception("PortalTypeId or ToScene is unknown");
    }
  }
}