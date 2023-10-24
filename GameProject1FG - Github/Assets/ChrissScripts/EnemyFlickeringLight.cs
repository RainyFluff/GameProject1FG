using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyProximityLight : MonoBehaviour
{
    [Header("FlickeringLight")]
    [Range(0.1f, 6.0f)]
    [SerializeField] private float minEnemyRange;
    [Range(2.0f, 15.0f)]
    [SerializeField] private float maxEnemyRange = 15.0f;
    [Range(0.0f, 0.2f)]
    [SerializeField] private float minTimeDelay;
    [Range(0.01f, 1.8f)]
    [SerializeField] private float maxTimeDelay;
    [SerializeField] private float midPoint = 6.0f;

    public float distanceToEnemy;

    public Transform enemy;
    public Light flickerLight;

    public bool isFlickering;
    //public float timeDelay = 0.4f;
    private float startTime;
    public float coolDown1 = 0.3f;
    public float coolDown2 = 0.25f;

    void Update()
    {
        distanceToEnemy = Vector3.Distance(transform.position, enemy.position); 
        if (startTime <= Time.time)
        {
            if (distanceToEnemy < maxEnemyRange && distanceToEnemy > midPoint)
            {
                StartCoroutine(FlickeringLightFar());
                isFlickering = true;
            }

            else if (distanceToEnemy < minEnemyRange && distanceToEnemy <= midPoint)
            {
                StartCoroutine(FlickeringLightClose());
                isFlickering = true;
            }
        }
    }

    IEnumerator FlickeringLightFar()
    {
        startTime = Time.time + coolDown1;
        flickerLight.enabled = false;
        yield return new WaitForSeconds(maxTimeDelay);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(maxTimeDelay);
        isFlickering = false;    
    }

    IEnumerator FlickeringLightClose()
    {
        startTime = Time.time + coolDown2;
        flickerLight.enabled = false;
        yield return new WaitForSeconds(minTimeDelay);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(minTimeDelay);
        isFlickering = false;
    }
}
