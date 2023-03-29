using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public sealed class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance {
        get {
            return _instance;
        }
    }

    [SerializeField]
    private Sound[] sounds;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // If there is already an instance, then delete self;
        if (_instance != null && _instance != this) {
            Debug.Log("An instance of AudioManager already exists in the scene.");
            Destroy(this.gameObject);
            return;
        }
        else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.GetRandomAudioClip();
            sound.audioSource.outputAudioMixerGroup = sound.audioMixerGroup;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
        }
    }

    // private void Update(){
    //     foreach (Sound sound in sounds)
    //     {
    //         if (!sound.audioSource.isPlaying)
    //         {
    //             sound.audioSource.clip = sound.GetRandomAudioClip();
    //         }
    //     }
    // }

    public void PlaySound(string name)
    {
        Sound sound = SearchForSound(name);
        
        if (sound != null)
        {
            sound.audioSource.clip = sound.GetRandomAudioClip();
            sound.audioSource.Play();
        }else{
            Debug.LogWarning("No audio clip with the name: " + name + " could be found");
        }
    }

    public bool isPlayingSound(string name)
    {
        Sound sound = SearchForSound(name);
        
        if (sound != null)
        {
            return sound.audioSource.isPlaying;
        }else{
            Debug.LogWarning("No audio clip with the name: " + name + " could be found");
        }
        return false;
    }
 
    public Sound SearchForSound(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                return sound;
            }
        }
        return null;
    }
}
