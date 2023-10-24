using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICounters : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soulCounter;
    [SerializeField] private TextMeshProUGUI sprintCooldown;
    [SerializeField] private TextMeshProUGUI freezeCooldown;
    private GhostEating ghostEating;
    private GhostFreeze ghostFreeze;
    private ReworkedMovement movementScript;
    private FlameSpawning flameSpawning;
    public GameObject player;
    private int freezeCooldwn;
    private int sprintCooldwn;

    private void Start()
    {
        movementScript = player.GetComponent<ReworkedMovement>();
        ghostEating = player.GetComponent<GhostEating>();
        ghostFreeze = player.GetComponent<GhostFreeze>();
        flameSpawning = player.GetComponent<FlameSpawning>();
    }

    void Update()
    {
        soulCounter.text = ghostEating.ghostsEaten.ToString();
        SprintCounter();
        FreezeCounter();
    }

    void SprintCounter()
    {
        if (movementScript.canSpeedBoost)
        {
            sprintCooldown.text = "Sprint Ready";
        }
        else
        {
            sprintCooldown.text = "Sprint Cooldown";
        }
    }

    void FreezeCounter()
    {
        if (ghostFreeze.canFreezeGhost)
        {
            freezeCooldown.text = "Freeze Ready";
        }
        else
        {
            freezeCooldown.text = "Freeze Cooldown";
        }
    }
}
