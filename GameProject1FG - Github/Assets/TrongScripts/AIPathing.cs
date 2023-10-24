
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIPathing : MonoBehaviour
{
    [SerializeField] private GameObject dummyPlayer;
    private NavMeshAgent _agent;
    [SerializeField] private Transform[] points = new Transform[3];
    private int nextPoint = 1;
    
    //[SerializeField] private LayerMask targetMask;
    //[SerializeField] private LayerMask obstructionMask;
    private bool _canSeePlayer;
    //[SerializeField] private float radius;
    //[Range(0,360)]
    //[SerializeField] private float angle;

    public float speedMultiplier = 2; 

    [SerializeField] private float viewRadius = 8f;
    public bool isFrozen;
    
    public NavMeshAgent _Agent
    {
        get { return _agent; }
        set { _agent = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        dummyPlayer = GameObject.Find("Player");
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        SeeingPlayer();
        //Pathing();
        //DetectingPlayer();
    }

    public void FixedUpdate()
    {
        
    }

    private void SeeingPlayer()
    {
        float distanceBetweenTargets = Vector3.Distance(_agent.transform.position, dummyPlayer.transform.position);
        //Debug.Log(distanceBetweenTargets);
        if (distanceBetweenTargets < viewRadius)
        {
            //_agent.speed = _agent.speed * speedMultiplier;
            FollowPlayer();
        }
        else
        {
                Pathing();
        }
    }

    private void Pathing()
    {
        float distanceBetween = Vector3.Distance(_agent.transform.position, points[nextPoint].transform.position);
        if (distanceBetween < 1.5f)
        {
            nextPoint = nextPoint != 2 ? nextPoint + 1 : 0;
        }

        _agent.destination = points[nextPoint].position;
    }

    private void FollowPlayer()
    {
        _agent.SetDestination(dummyPlayer.transform.position);
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Pathing();
        }
    }
}
