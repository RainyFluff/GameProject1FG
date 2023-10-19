using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private GameSettings settings;

    private Slider volume;

    void Start()
    {
        if (settings == null)
        {
            Debug.LogError("Settings SO is NULL");
        }

        volume = gameObject.GetComponent<Slider>();
        volume.value = AudioListener.volume;
        settings.SoundVolume = volume.value;
    }

    // Update is called once per frame
    void Update()
    {
        volume.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    void ChangeVolume()
    {
        settings.SoundVolume = volume.value;
        AudioListener.volume = volume.value;
    }
}
