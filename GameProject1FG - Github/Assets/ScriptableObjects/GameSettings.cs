using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    private float soundVolume = 0.5f;
    private float brightness = 0f;

    public float SoundVolume
    {
        get { return soundVolume; }
        set { soundVolume = value; }
    }

    public float Brightness
    {
        get { return brightness; } 
        set { brightness = value; }
    }
}
