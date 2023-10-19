using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool canSpeedBoost;
    private float time;
    private GhostEating ghostEating;

    public float speed = 5f;
    public float speedBoostMultiplier = 1.8f;
    public float speedBoostDuration = 5f;
    public float speedBoostCooldown = 10f;
    public float rotationSpeed = 160;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ghostEating = GetComponent<GhostEating>();
        speedBoostCooldown = speedBoostCooldown + speedBoostDuration;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        speedBoost();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,-1 * rotationSpeed * Time.deltaTime,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0,1 * rotationSpeed * Time.deltaTime,0);
        }
    }

    void speedBoost()
    {
        if (time <= Time.time)
        {
            canSpeedBoost = true;
        }
//Debug.Log(canSpeedBoost);
        if (Input.GetKey(KeyCode.LeftShift) && canSpeedBoost && !ghostEating.isHunting)
        {
            StartCoroutine(SpeedBoost());
        }
    }

    IEnumerator SpeedBoost()
    {
        speed = speed * speedBoostMultiplier;
        time = speedBoostCooldown + Time.time;
        canSpeedBoost = false;
        yield return new WaitForSecondsRealtime(speedBoostDuration);
        speed = speed / speedBoostMultiplier;
    }
}
