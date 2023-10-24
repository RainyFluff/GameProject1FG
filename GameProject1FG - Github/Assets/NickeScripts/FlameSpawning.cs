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
    [SerializeField] ParticleSystem flamePickUpParticles;
    private AudioSource _audioSource;

    [Header("Audio clips")]
    [SerializeField] private AudioClip _flameCollectSound;

    void Start()
    {
        Instantiate(Resources.Load("FlamePlaceholder"), defaultSpawn);
        flameNumber = 0;
        _audioSource = GetComponent<AudioSource>();
    }
    //public Transform RandomizedSpawnPosition(List<Transform> listToRandomize)
    //{
    //    randomNumber = Random.Range(0, listToRandomize.Count);
    //    //Debug.Log(randomNumber);
    //    Transform randomSpawn = listToRandomize[randomNumber];
    //    return randomSpawn;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flame")
        {
            //Debug.Log("Collision");
            previousSpawn = randomNumber;
            flamePickUpParticles.Play();
            Destroy(other.gameObject);
            _audioSource.clip = _flameCollectSound;
            _audioSource.Play();
            flameNumber++;            
            SpawnFlame();
        }
    }

    void SpawnFlame()
    {
        spawnTimerCooldown();
        List<Transform> transformTlist = new List<Transform>();
        foreach (Transform transformT in spawnPositions)
        {
            if (Vector3.Distance(transform.position, transformT.position) > 1.5f)
            {
                transformTlist.Add(transformT);
            }
        }
        //Transform spawnTransform = RandomizedSpawnPosition(transformTlist);
        randomNumber = Random.Range(0, transformTlist.Count);
        if (spawnPositions[randomNumber].transform.childCount == 0)
        {
            Instantiate(Resources.Load("FlamePlaceholder"), spawnPositions[randomNumber]);
        }
        else
        {
            int newSpawnNum;
            if (randomNumber == transformTlist.Count)
            {
                newSpawnNum = randomNumber - 1;
                Instantiate(Resources.Load("FlamePlaceholder"), spawnPositions[newSpawnNum]);
            }
            else if (randomNumber < transformTlist.Count && randomNumber >= 0) 
            {
                newSpawnNum = randomNumber + 1;
                Instantiate(Resources.Load("FlamePlaceholder"), spawnPositions[newSpawnNum]);
            }
        }
    }

    IEnumerator spawnTimerCooldown()
    {
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
