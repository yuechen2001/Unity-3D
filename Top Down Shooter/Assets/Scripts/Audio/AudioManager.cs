using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; 

    // Start is called before the first frame update
    void Awake()
    {
        // Allow Audio Manager to be the only AudioSource for all sounds in game 
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }

        PlaySound("BGM");
    }

    public void PlaySound(string name)
    {
        foreach(Sound s in sounds)
        {
            if (s.name == name)
                s.source.Play(); 
        }
    }

    public void PauseSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.Pause(); 
        }
    }

    public void ResumeSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.UnPause(); 
        }
    }
}
