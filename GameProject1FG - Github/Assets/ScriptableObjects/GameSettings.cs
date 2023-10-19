using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    private float soundVolume = 100;

    public float SoundVolume
    {
        get { return soundVolume; }
        set { soundVolume = value; }
    }
}
