using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRandomPath : MonoBehaviour
{
    [SerializeField] private float checkRange = 10;
    private int randomNumber;
    [SerializeField] private List<GameObject> nearbyRoutes;
    public List<Transform> decidedRouteChildren;
    private Transform decidedRoute;

    private AIPathing AIcode;
    // Start is called before the first frame update
    void Start()
    {
        decidedRouteChildren.Clear();
        nearbyRoutes.Clear();
        AIcode = GetComponent<AIPathing>();
        foreach (Collider routes in Physics.OverlapSphere(transform.position, checkRange))
        {
            if (routes.gameObject.tag == "EnemyRoute")
            {
                nearbyRoutes.Add(routes.gameObject);
            }
        }
        decidedRoute = RandomizedRoutePosition(nearbyRoutes).transform;
        foreach (Transform child in decidedRoute)
        {
            decidedRouteChildren.Add(child);
        }
        AIcode.points.AddRange(decidedRouteChildren);
    }
    public GameObject RandomizedRoutePosition(List<GameObject> listToRandomize)
    {
        randomNumber = Random.Range(0, listToRandomize.Count);
        //Debug.Log(randomNumber);
        GameObject randomSpawn = listToRandomize[randomNumber];
        Debug.Log(randomSpawn);
        return randomSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
