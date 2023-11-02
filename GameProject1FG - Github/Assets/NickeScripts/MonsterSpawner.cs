using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> monsterSpawnPosList;
    public List<GameObject> spawnedMonsterList;
    private int randomNumber;

    public int spawnedMonsters;

    [SerializeField] private int maxSpawnedMonsters = 5;


    void Start()
    {

    }
    public Transform RandomizedSpawnPosition(List<Transform> listToRandomize)
    {
        randomNumber = Random.Range(0, listToRandomize.Count);
        //Debug.Log(randomNumber);
        Transform randomSpawn = listToRandomize[randomNumber];
        return randomSpawn;
    }
    // Update is called once per frame
    void Update()
    {
        if (spawnedMonsters < maxSpawnedMonsters)
        {
            EnemySpawn();
        }
    }

    void EnemySpawn()
    {
        GameObject enemyInstance = Instantiate((GameObject)Resources.Load("EnemyPlaceholder"), RandomizedSpawnPosition(monsterSpawnPosList));
        spawnedMonsterList.Add(enemyInstance);
        spawnedMonsters++;
    }
}
