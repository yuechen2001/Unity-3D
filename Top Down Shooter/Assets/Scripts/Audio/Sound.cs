using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioSource source;
    public AudioClip clip;

    public string name; 
    public float volume;    
    public bool loop;
}
