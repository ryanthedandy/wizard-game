using System;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    // array of sounds to place sounds in the unity UI
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            // for each sound in the sounds array, give it an audio source on the audio manager object
            // sync the variables from our created "sound" class with the variables inside of unity audio system
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        // play theme on loop
        Play("theme");
    }
    public void Play(string name)
    {
        // make a 'play' function that takes a string, finds the string in our sounds array and then plays the sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip);
    }    
}
