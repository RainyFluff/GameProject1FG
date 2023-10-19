using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlameSpawning : MonoBehaviour
{
    public List<Transform> spawnPositions;
    public Transform defaultSpawn;
    private int randomNumber;
    public int flameNumber;
    private int previousSpawn;
    void Start()
    {
        Instantiate(Resources.Load("FlamePlaceholder"), defaultSpawn);
        flameNumber = 0;
    }
    public Transform RandomizedSpawnPosition(List<Transform> listToRandomize)
    {
        randomNumber = Random.Range(0, listToRandomize.Count);
        //Debug.Log(randomNumber);
        Transform randomSpawn = listToRandomize[randomNumber];
        return randomSpawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flame")
        {
            //Debug.Log("Collision");
            previousSpawn = randomNumber;
            Destroy(other.gameObject);
            spawnTimerCooldown();
            flameNumber++;
            List<Transform> transformTlist = new List<Transform>();
            foreach (Transform transformT in spawnPositions)
            {
                if (Vector3.Distance(transform.position, transformT.position) > 1.5f)
                {
                    transformTlist.Add(transformT);
                }
            }
            Transform spawnTransform = RandomizedSpawnPosition(transformTlist);
            Instantiate(Resources.Load("FlamePlaceholder"), spawnTransform);
        }
    }

    IEnumerator spawnTimerCooldown()
    {
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
