using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlameProximityLight : MonoBehaviour
{
    [Header("Light")]
    [Range (0.0f, 10.0f)]
    [SerializeField] private float lightIncreaseRate;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float lightDecreaseRate;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float maxIntensity;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float minIntensity;
    [Range(0.0f, 2.0f)]
    [SerializeField] private float minFlameRange = 2.0f;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float maxFlameRange;
    public float distanceToTarget;


    List<Transform> currentFlamesList = new List<Transform>();
    public Transform flame;
    public Light playerLight;
 
    void Start()
    {
        playerLight = GetComponent<Light>();
    }
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, flame.position);
        if (distanceToTarget > maxFlameRange)
            distanceToTarget = maxFlameRange;
        else if (distanceToTarget < minFlameRange)
            distanceToTarget = minFlameRange;

        float lightScalar = ToRange(distanceToTarget, minFlameRange, maxFlameRange, 0.0f, 1.0f);
        float targetIntensity = maxIntensity * (1 - lightScalar);
        if (playerLight.intensity < targetIntensity)
        {
            playerLight.intensity += lightIncreaseRate * Time.deltaTime;
            if (playerLight.intensity > targetIntensity)
                playerLight.intensity = targetIntensity;
        }
        else if (playerLight.intensity > targetIntensity)
        {
            playerLight.intensity -= lightDecreaseRate * Time.deltaTime;
            if (playerLight.intensity < targetIntensity)
                playerLight.intensity = targetIntensity;
        }
    }
        private float ToRange(float target, float range1min, float range1max, float range2min, float range2max) {
        return range2min + (((target - range1min) * (range2max - range2min)) / (range1max - range1min));
    }


}
