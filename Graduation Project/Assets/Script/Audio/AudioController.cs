using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    List<AudioSource> allAudioSource = new List<AudioSource>();

    void Awake()
    {
        instance = this;
    }

    public void AddAudioSource(AudioSource audioSource) { allAudioSource.Add(audioSource); }
    public void SetVolume(float value)
    {
        foreach(AudioSource audioSource in allAudioSource)
            audioSource.volume = value;
    }

}
