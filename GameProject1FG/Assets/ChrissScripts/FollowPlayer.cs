using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    float rotationX = 0f;
    float rotationY = 0f;

    [SerializeField] public float sensitivity = 10f;

    void Update()
    {
        transform.position = player.transform.position;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY = Mathf.Clamp(rotationY, -50f, 50f);

        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localEulerAngles = new Vector3 (rotationY, rotationX, 0);
    }
}
