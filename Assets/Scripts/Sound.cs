using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [field: SerializeField]
    public string name { get; private set; }
    [field: SerializeField]
    public List<AudioClip> audioClips { get; private set; }
    [field: SerializeField]
    public AudioMixerGroup audioMixerGroup { get; private set; }

    [field: SerializeField]
    public bool loop { get; private set; }

    [field: SerializeField, Range(0f, 1f)]
    public float volume { get; private set; }
    [field: SerializeField, Range(-3f, 3f)]
    public float pitch { get; private set; }

    [HideInInspector]
    public AudioSource audioSource;

    public AudioClip GetRandomAudioClip()
    {
        if (audioClips == null || audioClips.Count == 0)
        {
            Debug.LogWarning("No audio clips found for sound: " + name);
            return null;
        }

        int randomIndex = Random.Range(0, audioClips.Count);
        return audioClips[randomIndex];
    }
}
