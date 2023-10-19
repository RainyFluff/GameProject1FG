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
    public float ghostHuntingRange = 2;
    private MovementScript movementScript;
    private FlameSpawning flameSpawning;

    void Start()
    {
        movementScript = GetComponent<MovementScript>();
        flameSpawning = GetComponent<FlameSpawning>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && flameSpawning.flameNumber >= flamesRequiredToEat)
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
