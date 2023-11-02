
using System;
using System.Collections;
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

    [SerializeField] private float viewRadius = 5;
    private float startRadius = 5;
    [SerializeField] private float huntingRadiusMultiplier = 2.5f;
    [SerializeField] private float viewRadiusDecoy = 10;
    public bool isFrozen;
    private GhostEating _ghostEating;
    private Animator animator;
    
    private float speedMultiplier = 1.535f;
    private DecoyAbility decoyScript;
    [SerializeField ]private float startSpeed = 2.6f;
    private bool canSeePlayer; 
    private bool canSeeDecoy;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dummyPlayer = GameObject.Find("Player");
        decoyScript = GameObject.Find("Player").GetComponent<DecoyAbility>();
        _ghostEating = GameObject.Find("Player").GetComponent<GhostEating>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = startSpeed;
        viewRadius = startRadius;
        if (canSeePlayer && canSeeDecoy)
        {
            FollowPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenTargets = Vector3.Distance(_agent.transform.position, dummyPlayer.transform.position);

        if (distanceBetweenTargets < (viewRadius * 1.80f) && _ghostEating.isHunting)
        {
            //_agent.speed++;
            Vector3 directToPlayer = _agent.transform.position - dummyPlayer.transform.position ;
            Vector3 newPos = _agent.transform.position + directToPlayer;
            _agent.SetDestination(newPos);
        }
        else
        {
            
        SeeingPlayer();
        SeeingDecoy();
        }
    }

    private void SeeingPlayer()
    {
        

        float distanceBetweenTargets = Vector3.Distance(_agent.transform.position, dummyPlayer.transform.position);
        //Debug.Log(distanceBetweenTargets);
        if (distanceBetweenTargets < viewRadius)
        {
            _agent.transform.rotation = Quaternion.Lerp(_agent.transform.rotation, Quaternion.LookRotation(dummyPlayer.transform.position - _agent.transform.position), _agent.angularSpeed * Time.deltaTime);
            canSeePlayer = true;
            animator.SetInteger("HuntingState", 1);
            FollowPlayer();
        }
        else
        {
            canSeePlayer = false;
            Pathing();
        }

        if (!canSeeDecoy && !canSeePlayer)
        {
            animator.SetInteger("HuntingState", 0);
        }
        
        
    }

    void SeeingDecoy()
    {
        if (decoyScript.decoy != null)
        {
            float distanceBetweenTargets = Vector3.Distance(_agent.transform.position, decoyScript.decoy.transform.position);
            if (distanceBetweenTargets < viewRadiusDecoy && !canSeePlayer)
            {
                canSeeDecoy = true;
                animator.SetInteger("HuntingState", 1);
                FollowDecoy();
            }
            else
            {
                canSeeDecoy = false;
            }
        }
        else
        {
            canSeeDecoy = false;
        }
        
    }
    private void Pathing()
    {
        viewRadius = startRadius;
        _agent.speed = startSpeed;
        animator.SetBool("Hunting", false);

        float distanceBetween = Vector3.Distance(_agent.transform.position, points[nextPoint].transform.position);
        if (distanceBetween < 1.5f)
        {
            nextPoint = nextPoint != 2 ? nextPoint + 1 : 0;
        }
        _agent.destination = points[nextPoint].position;
    }

    private void FollowPlayer()
    {
        _agent.speed = startSpeed * speedMultiplier;
        viewRadius = startRadius * huntingRadiusMultiplier;
        Debug.Log(viewRadius);
        
       _agent.SetDestination(dummyPlayer.transform.position);
    }

    private void FollowDecoy()
    {
        _agent.SetDestination(decoyScript.decoy.transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _agent.speed = startSpeed ;
            
            Pathing();
            
        }
    }
    
}
