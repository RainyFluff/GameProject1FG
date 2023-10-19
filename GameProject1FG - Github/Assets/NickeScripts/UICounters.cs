using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICounters : MonoBehaviour
{
    public FlameSpawning flameSpawning;
    public TextMeshProUGUI flameCounter;
    public TextMeshProUGUI soulCounter;
    public GhostEating ghostEating;
    void Update()
    {
        flameCounter.text = flameSpawning.flameNumber.ToString();
        soulCounter.text = ghostEating.ghostsEaten.ToString();
    }
}
