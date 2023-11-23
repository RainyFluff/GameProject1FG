using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour
{
    public AudioClip _clickSound;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.ignoreListenerPause = true;
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
    }

    public void playSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}
