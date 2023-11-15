using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeats : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip slowHeartBeat;
    [SerializeField] private AudioClip fastHeartBeat;
    [SerializeField] private GameSettings settings;

    [SerializeField] public float fadeOutTime = 0.5f;
    [Range(0.01f, 2.0f)]


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
            _audio.volume = settings.SoundVolume;
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
            _audio.volume = settings.SoundVolume;
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
            StartCoroutine(FadeOut(0, fadeOutTime));
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

    IEnumerator FadeOut(float endValue, float duration)
    {
        float time = 0;
        float startValue = _audio.volume;
        while (time < duration)
        {
            _audio.volume = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _audio.volume = endValue;
    }

}
