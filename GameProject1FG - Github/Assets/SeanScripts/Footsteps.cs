using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("Audio clips")]
    [SerializeField] private AudioClip _walking;
    [SerializeField] private AudioClip _running;

    private AudioSource _audioSource;

    private bool _isWalking;
    private bool _isRunning;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_isWalking && !_isRunning)
        {
            if(!_audioSource.isPlaying)
            {
                _audioSource.clip = _walking;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }
        else if (_isWalking && _isRunning)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _running;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }
        else
        {
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
}
