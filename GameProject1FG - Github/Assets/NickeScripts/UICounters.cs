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
    private EndObjectiveObj endObjectiveObjScript;
    public GameObject player;

    private void Start()
    {
        movementScript = player.GetComponent<ReworkedMovement>();
        ghostEating = player.GetComponent<GhostEating>();
        ghostFreeze = player.GetComponent<GhostFreeze>();

        // Gravestone object
        endObjectiveObjScript = GameObject.Find("mainpiece").GetComponent<EndObjectiveObj>();

        sprintCooldown.text = "Sprint Ready";
        freezeCooldown.text = "Freeze Ready";
    }

    void Update()
    {
        if (ghostEating.ghostsEaten >= 0)
        {
            soulCounter.text = ghostEating.ghostsEaten.ToString() + " / " + endObjectiveObjScript.GetRequiredSoulsValue().ToString();
        }
        else
        {
            soulCounter.text = "0 / " + endObjectiveObjScript.GetRequiredSoulsValue().ToString();
        }
    }

    public IEnumerator SprintCountDownCoroutine()
    {
        float sprintCooldownTime = movementScript.speedBoostCooldown;

        for (float i = sprintCooldownTime; i >= 0; i--)
        {
            sprintCooldown.text = "Sprint ready in: " + i;
            yield return new WaitForSeconds(1);
        }

        sprintCooldown.text = "Sprint Ready";
    }

    public IEnumerator FreezeCountdownCoroutine()
    {
        float freezeCooldownTime = ghostFreeze.freezeCooldown;

        for (float i = freezeCooldownTime; i >= 0; i--)
        {
            freezeCooldown.text = "Freeze ready in: " + i;
            yield return new WaitForSeconds(1);
        }

        freezeCooldown.text = "Freeze Ready";
    }
}
