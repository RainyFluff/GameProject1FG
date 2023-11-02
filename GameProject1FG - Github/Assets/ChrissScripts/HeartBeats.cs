using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeats : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip slowHeartBeat;
    [SerializeField] private AudioClip fastHeartBeat;

    public AudioSource _audio;

    [SerializeField] private EnemyProximityLight _enemyProximityLight;

    private bool isFar;
    private bool isClose;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isFar && !isClose)
        {
            _audio.clip = slowHeartBeat;
            if (!_audio.isPlaying)
            {
                //_audio.loop = true;
                _audio.Play();
                Debug.Log("slow");
            }

        }

        else if (isClose && !isFar)
        {
            _audio.clip = fastHeartBeat;
            if (!_audio.isPlaying)
            {
                //_audio.loop = true;
                _audio.Play();
                Debug.Log("fast");
            }
        }

        else if (!isClose && !isFar && _audio.isPlaying)
        {
            _enemyProximityLight.StopHeartBeatSound();
        }
    }
    public void IsClose()
    {
        isClose = true;
    }

    public void IsFar()
    {
        isFar = true;
    }

    public void IsNotFar()
    {
        isFar = false;
    }

    public void IsNotClose()
    {
        isClose = false;
    }
}
