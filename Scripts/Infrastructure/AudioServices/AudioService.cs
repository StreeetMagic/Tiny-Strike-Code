using System;
using System.Collections.Generic;
using System.Linq;
using AudioServices.AudioMixers;
using AudioServices.Sounds;
using AudioServices.Sounds.Configs;
using ConfigProviders;
using PersistentProgresses;
using Prefabs;
using SaveLoadServices;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using ZenjectFactories.ProjectContext;
using Random = UnityEngine.Random;

namespace AudioServices
{
  public class AudioService : IProgressWriter, ITickable
  {
    public const float SliderMutedLoudness = -40;
    private const float MutedLoudness = -80;

    private const string Music = nameof(Music);
    private const string SoundEffects = nameof(SoundEffects);
    private const string MasterVolume = nameof(MasterVolume);

    private readonly ArtConfigProvider _artConfigProvider;
    private readonly DevConfigProvider _devConfigProvider;
    private readonly SoundPlayer _soundPlayer;
    private readonly ProjectZenjectFactory _factory;
    private readonly AudioMixer _audioMixer;
    private readonly Dictionary<AudioMixerGroupId, AudioMixerGroupWrapper> _audioMixerGroupWrappers = new();

    private Transform _container;

    public AudioService(ArtConfigProvider artConfigProvider,
      ProjectZenjectFactory factory, DevConfigProvider devConfigProvider, AudioMixer audioMixer)
    {
      _artConfigProvider = artConfigProvider;
      _devConfigProvider = devConfigProvider;
      _audioMixer = audioMixer;
      _factory = factory;

      _soundPlayer = new SoundPlayer();
    }

    public bool IsWorking { get; private set; } = true;
    public float MusicLoudness { get; private set; }
    public float SoundEffectsLoudness { get; private set; }

    public void Init()
    {
      CreateContainer();
      CreateAudioMixerGroupWrappers();
    }

    public void ReadProgress(ProjectProgress projectProgress)
    {
      MusicLoudness = projectProgress.MusicLoudness;
      SoundEffectsLoudness = projectProgress.SoundEffectsLoudness;

      _audioMixer.SetFloat(Music, MusicLoudness);
      _audioMixer.SetFloat(SoundEffects, SoundEffectsLoudness);
    }

    public void WriteProgress(ProjectProgress projectProgress)
    {
      projectProgress.MusicLoudness = MusicLoudness;
      projectProgress.SoundEffectsLoudness = SoundEffectsLoudness;
    }

    public void Tick()
    {
      foreach (AudioMixerGroupWrapper audioMixerGroupWrapper in _audioMixerGroupWrappers.Values)
        audioMixerGroupWrapper.Tick();
    }

    public void Play(SoundId id, Vector3 at = default)
    {
      if (IsWorking == false)
        return;

      if (id == SoundId.Unknown)
        throw new Exception("Unknown sound id");

      SoundArtSetup soundSetup = _artConfigProvider.Sounds[id];

      AudioMixerGroupId groupId = _artConfigProvider.AudioMixerGroups[soundSetup.AudioMixerGroupId].Id;
      AudioMixerGroupArtSetup groupSetup = _artConfigProvider.AudioMixerGroups[groupId];

      AudioSourceWrapper audioSourceWrapper = _audioMixerGroupWrappers[groupId].GetOrNull();

      if (audioSourceWrapper == null)
        return;

      audioSourceWrapper.IsActive = true;

      AudioSource source = audioSourceWrapper.AudioSource;

      AudioClip clip = soundSetup.AudioClips[Random.Range(0, soundSetup.AudioClips.Count)];

      _soundPlayer.Play(clip, source, soundSetup.Volume, at, groupSetup.Loop);
    }

    public void StopAll()
    {
      foreach (AudioMixerGroupWrapper audioMixerGroupWrapper in _audioMixerGroupWrappers.Values)
        audioMixerGroupWrapper.StopAll();
    }

    public void SetMusicLoudness(float value)
    {
      if (value <= SliderMutedLoudness)
        value = MutedLoudness;

      MusicLoudness = value;

      _audioMixer.SetFloat(Music, MusicLoudness);
    }

    public void SetSoundEffectsLoudness(float value)
    {
      if (value <= SliderMutedLoudness)
        value = MutedLoudness;

      SoundEffectsLoudness = value;

      _audioMixer.SetFloat(SoundEffects, SoundEffectsLoudness);
    }

    public void MuteMasterMixer()
    {
      _audioMixer.SetFloat(MasterVolume, MutedLoudness);
    }
    
    public void UnmuteMasterMixer()
    {
      _audioMixer.SetFloat(MasterVolume, 0);
    }

    public void Mute()
    {
      foreach (AudioMixerGroupWrapper audioMixerGroupWrapper in _audioMixerGroupWrappers.Values)
        audioMixerGroupWrapper.Mute();
    }

    public void UnMute()
    {
      foreach (AudioMixerGroupWrapper audioMixerGroupWrapper in _audioMixerGroupWrappers.Values)
        audioMixerGroupWrapper.UnMute();
    }

    private void CreateContainer()
    {
      AudioSourceContainer prefab = _devConfigProvider.GetPrefabForComponent<AudioSourceContainer>(PrefabId.AudioSourceContainer);
      AudioSourceContainer container = _factory.InstantiatePrefabForComponent(prefab);
      _container = container.transform;
    }

    private void CreateAudioMixerGroupWrappers()
    {
      AudioMixerGroupId[] groupIds = _artConfigProvider.AudioMixerGroups.Keys.ToArray();

      foreach (AudioMixerGroupId id in groupIds)
      {
        AudioMixerGroupArtSetup setup = _artConfigProvider.AudioMixerGroups[id];

        float cooldown = setup.Cooldown;
        int count = setup.Count;

        GameObject gameObject = CreateAudioMixerGroudWrapper(id, cooldown);

        for (int i = 0; i < count; i++)
          CreateAudioSourceWrapper(gameObject, i, id);
      }
    }

    private GameObject CreateAudioMixerGroudWrapper(AudioMixerGroupId id, float cooldown)
    {
      AudioMixerGroupArtSetup setup = _artConfigProvider.AudioMixerGroups[id];
      AudioMixerGroup group = setup.AudioMixerGroup;
      var gameObject = new GameObject(group.name + "Group");
      gameObject.transform.SetParent(_container.transform);
      var audioMixerGroupWrapper = new AudioMixerGroupWrapper(group, cooldown);
      _audioMixerGroupWrappers.Add(id, audioMixerGroupWrapper);
      return gameObject;
    }

    private void CreateAudioSourceWrapper(GameObject groupGameObject, int count, AudioMixerGroupId id)
    {
      AudioMixerGroup group = _artConfigProvider.AudioMixerGroups[id].AudioMixerGroup;
      GameObject gameObject = _factory.InstantiatePrefab(_devConfigProvider.GetPrefab(PrefabId.AudioSource));
      gameObject.transform.SetParent(groupGameObject.transform);
      gameObject.name = groupGameObject.name + count;
      var audioSource = gameObject.GetComponent<AudioSource>();
      audioSource.outputAudioMixerGroup = group;

      _audioMixerGroupWrappers[id].Add(new AudioSourceWrapper(audioSource));
    }
  }
}