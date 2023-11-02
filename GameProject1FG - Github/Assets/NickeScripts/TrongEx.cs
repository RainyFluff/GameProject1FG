using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrongEx : MonoBehaviour
{
    [SerializeField] private List<float> nearbyNumbers;
    private Transform enemies;

    private void Update()
    {
        var sorted = nearbyNumbers.OrderBy(enemy => (transform.position - enemies.transform.position));
        var nearestEnemy = sorted.FirstOrDefault();
        nearbyNumbers.Sort();
    }

    
}
