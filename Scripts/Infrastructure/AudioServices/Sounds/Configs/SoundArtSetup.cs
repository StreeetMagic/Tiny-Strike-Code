using System;
using System.Collections.Generic;
using AudioServices.AudioMixers;
using UnityEngine;

namespace AudioServices.Sounds.Configs
{
  [Serializable]
  public class SoundArtSetup : ArtSetup<SoundId>
  {
    public AudioMixerGroupId AudioMixerGroupId;

    [Range(0f, 1f)] public float Volume;
    public List<AudioClip> AudioClips;
  }
}