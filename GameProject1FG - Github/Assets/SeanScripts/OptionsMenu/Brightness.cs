using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private GameSettings _settings;

    private Slider brightnessSlider;

    void Start()
    {
        if (_settings == null)
        {
            Debug.LogError("Settings SO is NULL");
        }

        brightnessSlider = GetComponent<Slider>();
        brightnessSlider.value = _settings.Brightness;
    }

    public void SetBrightness(float brightness)
    {
        _settings.Brightness = brightness;
    }
}
