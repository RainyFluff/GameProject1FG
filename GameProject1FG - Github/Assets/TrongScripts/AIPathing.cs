
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIPathing : MonoBehaviour
{
    [SerializeField] private GameObject dummyPlayer;
    private NavMeshAgent _agent;
    [Header("ALL AI CAN ONLY USE 3 POINTS")]
    public List<Transform> points;
    private int nextPoint = 1;

    [SerializeField] private float viewRadius = 1.5f;
    public bool isFrozen;
    private GhostEating _ghostEating;

    private DecoyAbility decoyScript;
    // Start is called before the first frame update
    void Start()
    {
        _ghostEating = GameObject.Find("Player").GetComponent<GhostEating>();
        dummyPlayer = GameObject.Find("Player");
        decoyScript = GameObject.Find("Player").GetComponent<DecoyAbility>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        SeeingPlayer();
        SeeingDecoy();
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
        else if (distanceBetweenTargets < 6.5f && _ghostEating.isHunting)
        {
            Vector3 directToPlayer = _agent.transform.position - dummyPlayer.transform.position ;
            Vector3 newPos = _agent.transform.position + directToPlayer;
            _agent.SetDestination(newPos);
        }
        else
        {
                Pathing();
        }
    }

    void SeeingDecoy()
    {
        float distanceBetweenTargets = Vector3.Distance(_agent.transform.position, decoyScript.decoy.transform.position);
        if (distanceBetweenTargets < viewRadius)
        {
            //_agent.speed = _agent.speed * speedMultiplier;
            FollowPlayer();
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
