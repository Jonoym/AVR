using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        // if (instance == null) {
        //     instance = this;
        // } else {
        //     Destroy(gameObject);
        //     return;
        // }

        // DontDestroyOnLoad(gameObject);

        foreach(Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void Start() {
        Play("BackgroundMusic");
    }

    public void Play(string name) {

        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null) {
            sound.source.Play();
        }
    }
}
