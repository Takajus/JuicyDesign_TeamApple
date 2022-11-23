using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }
    
    public Sound[] sounds;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            if(s.clips.Length > 1)
            {
               int index = UnityEngine.Random.Range(0, s.clips.Length);
               s.source.clip = s.clips[index];
            }
            else if (s.clips.Length != 0)
            {
                s.source.clip = s.clips[0];
            }
            else
            {
                Debug.LogError("has no clip!");
            }
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.Play();
    }
}
