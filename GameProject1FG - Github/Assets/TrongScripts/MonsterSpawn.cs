using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawn : MonoBehaviour
{
    public bool isBeingUsed;
    [SerializeField]private GameObject enemy;
    [SerializeField]private List<Transform> _monsterSpawnPos;
<<<<<<< Updated upstream
    [SerializeField] private GameObject player;
    //[SerializeField] private Transform dummyMonster;

    [SerializeField] private int numberOfMonsters = 6;
=======
    //[SerializeField] private GameObject player;
    [SerializeField] private Transform dummyMonster;
    [SerializeField] public bool monsterExist = true;
    //[SerializeField] private int numberOfMonsters = 30;
>>>>>>> Stashed changes
    private int randomNumber;
    //private AIPathing AIPathing;
    
    // Start is called before the first frame update
    void Start()
    {
        
        //RandomSpawnMonster();
        //SpawnMonster(dummy);
    }

    // Update is called once per frame
    void Update()
    {
        //RandomSpawnMonster();
        //SpawnMonster(dummy);
<<<<<<< Updated upstream
=======
        //monsterExist = false;
        if (!monsterExist)
        {
            RandomSpawnMonster();
        }
        
>>>>>>> Stashed changes
        
    }
    public Transform RandomizedSpawnPosition(List<Transform> listToRandomize)
    {
        randomNumber = Random.Range(0, listToRandomize.Count);
        //Debug.Log(randomNumber);
        Transform randomSpawn = listToRandomize[randomNumber];
        return randomSpawn;
    }

    public void RandomSpawnMonster()
    {
       // float distance = Vector3.Distance(_monsterSpawnPos[1].transform.position, player.transform.position);
        List<Transform> secondMonsterList = new List<Transform>();

        foreach (Transform monster in _monsterSpawnPos)
        {
            secondMonsterList.Add(monster);
        }
        //Debug.Log(secondMonsterList.Count);

        //Transform spawnTransform = RandomizedSpawnPosition(secondMonsterList);
<<<<<<< Updated upstream
        
        
=======
>>>>>>> Stashed changes
        SpawnMonster(secondMonsterList);

    }

<<<<<<< Updated upstream
    public void SpawnMonster(List<Transform> spawnList)
    {
        for (int i = 0; i < numberOfMonsters; i++)
        { 
            Instantiate(Resources.Load("MonsterPlaceHolder"), spawnList[i]);
=======
    public void SpawnMonster(List<Transform> spawnTransform)
    {
        
        Transform randomSpawn = RandomizedSpawnPosition(spawnTransform);
        
        
        if (!monsterExist)
        { 
            Instantiate(Resources.Load("MonsterPlaceHolder"), randomSpawn);
            monsterExist = true;
>>>>>>> Stashed changes
        }
        
    }
    
}
