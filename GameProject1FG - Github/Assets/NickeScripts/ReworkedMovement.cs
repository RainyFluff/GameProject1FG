using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReworkedMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //forward
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Debug.Log("forward");
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Debug.Log("left forward");
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.A))
        {
            Debug.Log("right forward");
        }
        //backward
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Debug.Log("backward");
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Debug.Log("left backward");
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.A))
        {
            Debug.Log("right backward");
        }
        //Horizontal
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            Debug.Log("right");
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            Debug.Log("left");
        }
    }
}
