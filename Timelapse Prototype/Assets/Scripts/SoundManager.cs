using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Joue le son
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();
    }

    // Change le pitch du son au hasard
    public void ChangePitch(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.pitch = UnityEngine.Random.Range(s.pitch - 0.1f, s.pitch + 0.1f);
    }

    // Stoppe le son joué en loop
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Stop();
    }

    // Trouve le son si on a besoin de paramètres associés dans un autre script
    public AudioClip FindSound(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        return s.source.clip;
    }
}
