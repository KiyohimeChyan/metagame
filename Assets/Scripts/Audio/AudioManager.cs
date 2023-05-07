using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("“Ù∆µº‡Ã˝")]
    public PlayAudioEventSO BGMEvents;
    public PlayAudioEventSO FXEvents;

    public AudioSource BGMSource;
    public AudioSource FXSource;

    private void OnEnable()
    {
        FXEvents.OnEventRaised += OnFXEvent;
        BGMEvents.OnEventRaised += OnBGMEvent;
    }

    private void OnDisable()
    {
        FXEvents.OnEventRaised -= OnFXEvent;
        BGMEvents.OnEventRaised -= OnBGMEvent;


    }

    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }
}
