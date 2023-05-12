using UnityEngine;
[System.Serializable]
public class Sound 
{
    // create sound class to use in our audio manager. 
    public string name;
    public AudioClip clip;

    // adjust volume and pitch in unity UI
    [Range(0f,1f)]
    public float volume;
    [Range(0f,1f)]
    public float pitch;

    // adjust loop in Unity UI
    public bool loop;

    // variable to hold audio source
    [HideInInspector]
    public AudioSource source;
}
