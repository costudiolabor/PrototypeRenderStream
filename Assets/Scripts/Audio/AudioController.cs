using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private AudioClip _audioClip;

    void Start()
    {
        // name, lengthSamples, channels, frequency, stream,
        _audioClip = AudioClip.Create("AudioInput", 1, 1, 1000, true);
        audioSource.clip = _audioClip;
        audioSource.Play();
    }
}