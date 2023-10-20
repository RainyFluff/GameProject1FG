using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICounters : MonoBehaviour
{
    public TextMeshProUGUI soulCounter;
    public GhostEating ghostEating;
    void Update()
    {
        soulCounter.text = ghostEating.ghostsEaten.ToString();
    }
}
