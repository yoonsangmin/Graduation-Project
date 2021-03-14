using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController audioController;
    public static AudioController instance
    {
        get
        {
            if (audioController == null)
                audioController = FindObjectOfType<AudioController>();
            return audioController;
        }
    }

    private float volume;
    public float _volume { get { return volume; } }

    private List<AudioSource> allAudioSource = new List<AudioSource>();

    public void AddAudioSource(AudioSource audioSource) { allAudioSource.Add(audioSource); }
    public void SetVolume(float value)
    {
        volume = value;
        foreach (AudioSource audioSource in allAudioSource)
            audioSource.volume = volume;
    }
}
