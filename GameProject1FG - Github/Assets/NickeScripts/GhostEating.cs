using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEating : MonoBehaviour
{
    private RaycastHit hit;
    public bool isHunting;
    public int flamesRequiredToEat = 5;
    public float huntingModeSpeed = 1.4f;
    public float huntingTime = 5;
    public int ghostsEaten;
    public int ghostDamage = 5;
    public float ghostHuntingRange = 2;
    private ReworkedMovement movementScript;
    private FlameSpawning flameSpawning;
    [SerializeField] private GameObject deathScreen;

    void Start()
    {
        movementScript = GetComponent<ReworkedMovement>();
        flameSpawning = GetComponent<FlameSpawning>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flameSpawning.flameNumber >= flamesRequiredToEat)
        {
            StartCoroutine(HuntingMode());
        }
        if (isHunting)
        {
            ghosteating();
        }
    }
    void ghosteating()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, ghostHuntingRange))
        {
            if (collider.tag == "Enemy")
            {
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, 0.01f);
                if (Vector3.Distance(collider.transform.position, transform.position) < 1.5f)
                {
                    Destroy(collider.gameObject);
                    ghostsEaten++;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            ghostsEaten -= ghostDamage;
            if (ghostsEaten < 0)
            {
                deathScreen.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator HuntingMode()
    {
        movementScript.speed = movementScript.speed * huntingModeSpeed;
        isHunting = true;
        flameSpawning.flameNumber -= flamesRequiredToEat;
        //change light in some way
        //add particle effects?
        //add sounds
        yield return new WaitForSecondsRealtime(huntingTime);
        movementScript.speed = movementScript.speed / huntingModeSpeed;
        isHunting = false;
        //change light back to standard
        //remove particle effects?
        //remove sounds
    }
}
