using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("Audio clips")]
    [SerializeField] private AudioClip _walking;
    [SerializeField] private AudioClip _running;
    [SerializeField] private GameSettings _settings;

    [SerializeField] public float fadeOutTime = 1f;
    [Range(0.01f, 2.0f)]

    private AudioSource _audioSource;

    private GhostEating _ghostEating;

    private bool _isWalking;
    private bool _isRunning;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _ghostEating = GameObject.Find("Player").GetComponent<GhostEating>();
    }

    void Update()
    {
        if (_isWalking && !_isRunning && !_ghostEating.isHunting)
        {
            _audioSource.volume = _settings.SoundVolume;
            if (!_audioSource.isPlaying)
            {
               
                _audioSource.clip = _walking;
                _audioSource.loop = true;
                _audioSource.Play();
                
            }
        }
        else if (_isWalking && _isRunning && _ghostEating.isHunting)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _running;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }
        else if (!_isWalking && !_isRunning)
        {
            StartCoroutine(FadeOut(0, fadeOutTime));
            _audioSource.Stop();
        }
    }

    public void StartWalking()
    {
        _isWalking = true;
    }

    public void StopWalking()
    {
        _isWalking = false;
    }

    public void StartRunning()
    {
        _isRunning = true;
    }

    public void StopRunning()
    {
        _isRunning = false;
    }
    IEnumerator FadeOut(float endValue, float duration)
    {
        float time = 0;
        float startValue = _audioSource.volume;
        while (time < duration)
        {
            _audioSource.volume = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _audioSource.volume = endValue;
    }


}
