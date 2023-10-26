using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ReworkedMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5;
    [SerializeField] private Transform orientation;
    [SerializeField] private float speedBoostMultiplier = 1.4f;
    public float speedBoostCooldown = 10f;
    [SerializeField] private float speedBoostDuration = 5f;
    private float time;
    public bool canSpeedBoost = true;
    private GhostEating ghostEating;
    private UICounters UIcounters;
    public float turnSpeed = 600;
    Quaternion qTo;

    void Start()
    {
        ghostEating = GetComponent<GhostEating>();
        rb = GetComponent<Rigidbody>();
        speedBoostCooldown = speedBoostCooldown + speedBoostDuration;
        UIcounters = GameObject.Find("Canvas").GetComponent<UICounters>();
        qTo = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        speedBoost();
        Move();
    }
    void Move()
    {
        //forward
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rb.velocity = orientation.forward * speed;
            qTo = Quaternion.Euler(0,45,0);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Vector3 vector = (orientation.forward + -orientation.right).normalized;
            qTo = Quaternion.Euler(0,0,0);
            rb.velocity = vector * speed;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.A))
        {
            Vector3 vector = (orientation.forward + orientation.right).normalized;
            qTo = Quaternion.Euler(0,90,0);
            rb.velocity = vector * speed;
        }
        //backward
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rb.velocity = -orientation.forward * speed;
            qTo = Quaternion.Euler(0,225,0);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Vector3 vector = (-orientation.forward + -orientation.right).normalized;
            rb.velocity = vector * speed;
            qTo = Quaternion.Euler(0,270,0);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.A))
        {
            Vector3 vector = (-orientation.forward + orientation.right).normalized;
            qTo = Quaternion.Euler(0,180,0);
            rb.velocity = vector * speed;
        }
        //Horizontal
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            rb.velocity = orientation.right * speed;
            qTo = Quaternion.Euler(0,135,0);
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            rb.velocity = -orientation.right * speed;
            qTo = Quaternion.Euler(0,315,0);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, turnSpeed * Time.deltaTime);
    }
    void speedBoost()
    {
        if (time <= Time.time)
        {
            canSpeedBoost = true;
        }
//Debug.Log(canSpeedBoost);
        if (Input.GetKey(KeyCode.LeftShift) && canSpeedBoost && !ghostEating.isHunting && ghostEating.ghostsEaten >= 1)
        {
            ghostEating.ghostsEaten--;
            StartCoroutine(SpeedBoost());
            StartCoroutine(UIcounters.SprintCountDownCoroutine());
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
