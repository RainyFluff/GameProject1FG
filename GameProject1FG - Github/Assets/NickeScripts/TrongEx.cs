using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrongEx : MonoBehaviour
{
    public Transform flame;
    public bool isBeingUsed;
    private GameObject enemy;

    private void Update()
    {
        if (!enemy.active)
        {
            isBeingUsed = false;
        }
        Debug.Log(isBeingUsed);
    }

    public void onSpawn()
    {
        foreach (Collider enemies in Physics.OverlapSphere(transform.position, 2))
        {
            if (enemies.transform.tag == "Enemy")
            {
                isBeingUsed = true;
                enemy = enemies.transform.gameObject;
            }
        } 
    }
    
}
