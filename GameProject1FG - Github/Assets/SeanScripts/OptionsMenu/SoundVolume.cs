using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private GameSettings _settings;

    [Header("Audio mixer")]
    [SerializeField] private AudioMixer _mixer;

    private Slider volume;

    void Start()
    {
        if (_settings == null)
        {
            Debug.LogError("Settings SO is NULL");
        }

        volume = gameObject.GetComponent<Slider>();
        volume.value = _settings.SoundVolume;
        _mixer.SetFloat("MasterVolume", Mathf.Log10(volume.value) * 10);
    }

    public void SetVolume(float sliderValue)
    {
        _mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 10);
        _settings.SoundVolume = sliderValue;
    }
}
