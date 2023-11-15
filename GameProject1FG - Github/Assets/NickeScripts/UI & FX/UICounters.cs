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
    [SerializeField] private Image soulFullGlow;

    [Header("Decoy ability")] [SerializeField]
    private Image decoyCooldown;

    [SerializeField] private Image decoyActive;

    [Header("Freeze ability")] [SerializeField]
    private Image freezeCooldown;

    [SerializeField] private Image freezeActive;

    [Header("Player Obj")] public GameObject player;

    [Header("Player Health")] 
    [SerializeField] private Sprite healthSpriteFull;
    [SerializeField] private Sprite healthSpriteEmpty;
    [SerializeField] private Image[] heartArray;
    private int numberOfHearts;

    [Header("Main Objective")]
    [SerializeField] private GameObject _mainpiece;
    
    
    
    private GhostEating ghostEating;
    private GhostFreeze ghostFreeze;
    private DecoyAbility decoyScript;

    private void Start()
    {
        decoyScript = player.GetComponent<DecoyAbility>();
        ghostEating = player.GetComponent<GhostEating>();
        ghostFreeze = player.GetComponent<GhostFreeze>();
        numberOfHearts = ghostEating.health;

        soulFullGlow.gameObject.SetActive(false);
    }

    void Update()
    {
        AddToSoulsLantern();
        PlayerHealthToUI();

        if (ghostEating.ghostsEaten >= _mainpiece.GetComponent<EndObjectiveObj>()._requiredSouls)
        {
            soulFullGlow.gameObject.SetActive(true);
        }
        else
        {
            soulFullGlow.gameObject.SetActive(false);
        }

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

    void PlayerHealthToUI()
    {
        for (int i = 0; i < heartArray.Length; i++)
        {
            if (i < ghostEating.health)
            {
                heartArray[i].sprite = healthSpriteFull;
            }
            else
            {
                heartArray[i].sprite = healthSpriteEmpty;
            }
            if (i < numberOfHearts)
            {
                heartArray[i].enabled = true;
            }
            else
            {
                heartArray[i].enabled = false;
            }
        }
    }
}