using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }

        if (gameObject.CompareTag("Seamless"))
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Play(string name)
    {
        Sound playSound = Array.Find(sounds, sound => sound.name == name);
        playSound.source.Play();
    }

    public void Stop(string name)
    {
        Sound stopSound =Array.Find(sounds, sound => sound.name == name);
        stopSound.source.Stop();
    }
}
