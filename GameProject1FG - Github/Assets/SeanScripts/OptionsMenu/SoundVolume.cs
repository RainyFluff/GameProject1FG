using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private GameSettings _settings;

    [Header("Audio mixer")]
    [SerializeField] private AudioMixer _mixer;

    [Header("Sound_Volume_Slider")]
    [SerializeField] private GameObject _volumeSlider;

    private Slider volume;

    void Start()
    {
        if (_settings == null)
        {
            Debug.LogError("Settings SO is NULL");
        }

        volume = _volumeSlider.GetComponent<Slider>();
        volume.value = _settings.SoundVolume;
        _mixer.SetFloat("MasterVolume", Mathf.Log10(volume.value) * 20);
    }

    void Update()
    {
        _mixer.SetFloat("MasterVolume", Mathf.Log10(volume.value) * 20);
        _settings.SoundVolume = volume.value;
    }
}
