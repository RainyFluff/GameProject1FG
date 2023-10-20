using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField]private List<Transform> _monsterSpawnPos;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform dummyMonster;

    [SerializeField] private int numberOfMonsters = 6;
    private int randomNumber;
    //private AIPathing AIPathing;
    
    // Start is called before the first frame update
    void Start()
    {
        RandomSpawnMonster();
        //SpawnMonster(dummy);
    }

    // Update is called once per frame
    void Update()
    {
        //RandomSpawnMonster();
        //SpawnMonster(dummy);
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
        Debug.Log(secondMonsterList.Count);

        Transform spawnTransform = RandomizedSpawnPosition(secondMonsterList);
        
        SpawnMonster(spawnTransform);

    }

    public void SpawnMonster(Transform spawnTransform)
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            Instantiate(Resources.Load("MonsterPlaceHolder"), spawnTransform);

        }
    }
}
