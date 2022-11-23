using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip[] clips;
    
    [Range(0f, 1f)]
    public float volume;
    [Range(-3, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
    
    public bool loop;
}