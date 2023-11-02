using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public Light flickerLight;
    public bool isFar;
    public bool isClose;
    private float startTime;
    public float coolDown1 = 0.3f;
    public float coolDown2 = 0.25f;
    [SerializeField] private MonsterSpawner monsterSpawner;

    [SerializeField] private GameObject heartBeatsManager;

    public GameObject[] allEnemy;
    float distance;
    float nearestDistance = 10000;
    GameObject nearestEnemy;

    void FindEnemys()
    {
        nearestDistance = float.MaxValue;
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < allEnemy.Length; i++)
        {
            distance = Vector3.Distance(transform.position, allEnemy[i].transform.position);

            if (distance <= nearestDistance)
            {
                nearestEnemy = allEnemy[i];
                nearestDistance = distance;
            }
        }
    }

    void Update()
    {
        FindEnemys();


        distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
        if (startTime <= Time.time)
        {
            if (distanceToEnemy < maxEnemyRange && distanceToEnemy > midPoint)
            {
                isFar = true;
                isClose = false;
                StartCoroutine(FlickeringLightFar());
            }

            else if (distanceToEnemy < minEnemyRange && distanceToEnemy <= midPoint)
            {
                isClose = true;
                isFar = false;
                StartCoroutine(FlickeringLightClose());
            }

            else if (distanceToEnemy > maxEnemyRange)
            {
                isClose = false;
                isFar = false;
                Debug.Log("bruh kek");
            }
        }

        if (isFar && !isClose)
        {
            heartBeatsManager.GetComponent<HeartBeats>().IsFar();
            heartBeatsManager.GetComponent<HeartBeats>().IsNotClose();
        }
        else if (isClose && !isFar)
        {
            heartBeatsManager.GetComponent<HeartBeats>().IsClose();
            heartBeatsManager.GetComponent<HeartBeats>().IsNotFar();
        }
        else
        {
            heartBeatsManager.GetComponent<HeartBeats>().IsNotFar();
            heartBeatsManager.GetComponent<HeartBeats>().IsNotClose();
        }
    }

    IEnumerator FlickeringLightFar()
    {
        startTime = Time.time + coolDown1;
        flickerLight.enabled = false;
        yield return new WaitForSeconds(maxTimeDelay);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(maxTimeDelay);
    }

    IEnumerator FlickeringLightClose()
    {
        startTime = Time.time + coolDown2;
        flickerLight.enabled = false;
        yield return new WaitForSeconds(minTimeDelay);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(minTimeDelay);
 
    }

    public void StopHeartBeatSound()
    {
        heartBeatsManager.GetComponent<AudioSource>().Stop();
    }
}
