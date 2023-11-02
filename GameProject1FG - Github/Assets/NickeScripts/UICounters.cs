using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICounters : MonoBehaviour
{
    [Header("Soul counter")]
    [SerializeField] private Image soulCounter;
    [SerializeField] private Image soulFull;

    [Header("Decoy ability")]
    [SerializeField] private Image decoyCooldown;
    [SerializeField] private Image decoyActive;

    [Header("Freeze ability")]
    [SerializeField] private Image freezeCooldown;
    [SerializeField] private Image freezeActive;

    [Header("Flame")]
    [SerializeField] private Image inactiveFlame;
    [SerializeField] private Image activeFlame;
    [SerializeField] private Image flameFog;

    [Header("Player Obj")]
    public GameObject player;

    private GhostEating ghostEating;
    private GhostFreeze ghostFreeze;
    private DecoyAbility decoyScript;
    private EndObjectiveObj endObjectiveObjScript;

    private void Start()
    {
        decoyScript = player.GetComponent<DecoyAbility>();
        ghostEating = player.GetComponent<GhostEating>();
        ghostFreeze = player.GetComponent<GhostFreeze>();

        // Gravestone object
        endObjectiveObjScript = GameObject.Find("mainpiece").GetComponent<EndObjectiveObj>();
    }

    void Update()
    {
        AddToSoulsLantern();

        if (ghostFreeze.canFreezeGhost)
        {
            freezeCooldown.gameObject.SetActive(false);
        }
        else
        {
            freezeCooldown.gameObject.SetActive(true);
        }

        if (decoyScript.canUseDecoy)
        {
            decoyCooldown.gameObject.SetActive(false);
        }
        else
        {
            decoyCooldown.gameObject.SetActive(true);
        }

        if (ghostEating.isHunting)
        {
            activeFlame.gameObject.SetActive(true);
            flameFog.gameObject.SetActive(true);
        }
        else
        {
            activeFlame.gameObject.SetActive(false);
            flameFog.gameObject.SetActive(false);
        }
    }

    public void AddToSoulsLantern()
    {
        soulFull.fillAmount = ghostEating.ghostsEaten * 0.2f;
    }

    public IEnumerator DecoyCountDown()
    {
        float decoyCooldownTime = decoyScript.cooldown;

        float time = 1 / decoyCooldownTime;

        for (float i = decoyCooldownTime; i >= 0; i--)
        {
            decoyCooldown.fillAmount -= time;
            yield return new WaitForSeconds(1);
        }

        decoyScript.canUseDecoy = true;
        decoyCooldown.gameObject.SetActive(false);
        decoyCooldown.fillAmount = 1;
    }

    public IEnumerator FreezeCountdownCoroutine()
    {
        Debug.Log("Freeze cooldown started");
        float freezeCooldownTime = ghostFreeze.freezeCooldown;

        float time = 1 / freezeCooldownTime;

        for (float i = freezeCooldownTime; i >= 0; i--)
        {
            freezeCooldown.fillAmount -= time;
            yield return new WaitForSeconds(1);
        }

        freezeCooldown.gameObject.SetActive(false);
        freezeCooldown.fillAmount = 1;
    }
}
