using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public int audioToggle;
    public AudioClip currentClip;
    
    private static AudioManager _instance;
    private double _musicDuration;
    private double _goalTime;
    private double _delayBetweenPlays = 1;
    
    
    public void SetCurrentClip(AudioClip clip)
    {
        currentClip = clip;
    }
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Add an initial delay so speaker/eardrums won't get shocked
        _goalTime = AudioSettings.dspTime + 1;
        PlayScheduledClip();
    }

    private void Update()
    {
        if (AudioSettings.dspTime > _goalTime)
        {
            PlayScheduledClip();
        }
    }
    
    private void PlayScheduledClip()
    {
        audioSources[audioToggle].clip = currentClip;
        audioSources[audioToggle].PlayScheduled(_goalTime);

        _musicDuration = (double) currentClip.samples / currentClip.frequency;
        _goalTime = _goalTime + _musicDuration;
    }

}
