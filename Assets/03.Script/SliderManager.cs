using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider soundSlider;

    private void Start()
    {
        // Set the slider's value to the current volume
        soundSlider.value = audioSource.volume;
    }

    public void SetVolume(float volume)
    {
        // Set the volume of the audio source to the slider's value
        audioSource.volume = volume;
    }

    public void ToggleSound()
    {
        // Toggle the audio source's mute property
        audioSource.mute = !audioSource.mute;
    }
}
