
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
    private float decoyToPlayerRadius = 3;
    [SerializeField] private float huntingRadiusMultiplier = 2.5f;
    [SerializeField] private float viewRadiusDecoy = 10;
    public bool isFrozen;
    private GhostEating _ghostEating;
    private Animator animator;

    [SerializeField] private AudioClip enemySobbing;
    [SerializeField] private AudioClip enemyHunting;


    [SerializeField] private AudioSource audioSource1;

    [SerializeField] private AudioSource audioSource2;

    private float speedMultiplier = 1.535f;
    private DecoyAbility decoyScript;
    [SerializeField] private float startSpeed = 2.4f;
    private bool canSeePlayer;
    private bool canSeeDecoy;
    private RaycastHit hit;
    private MonsterSpawner _monsterSpawner;
    private bool deathState;
    private bool isRoaming;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dummyPlayer = GameObject.Find("Player");
        _monsterSpawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        decoyScript = GameObject.Find("Player").GetComponent<DecoyAbility>();
        _ghostEating = GameObject.Find("Player").GetComponent<GhostEating>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = startSpeed;
        viewRadius = startRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathState)
        {
            _agent.enabled = false;
        }
        if (isFrozen)
        {
            animator.SetBool("IsFrozen", true);
        }
        else
        {
            animator.SetBool("IsFrozen", false);
        }
        deathState = animator.GetBool("HasDied");
        float distanceBetweenTargets = Vector3.Distance(_agent.transform.position, dummyPlayer.transform.position);
        if (distanceBetweenTargets < (viewRadius + 0.8f) && _ghostEating.isHunting)
        {
            //_agent.speed++;
            Vector3 directToPlayer = _agent.transform.position - dummyPlayer.transform.position;
            Vector3 newPos = _agent.transform.position + directToPlayer;
            _agent.SetDestination(newPos);
            _agent.speed = startSpeed / speedMultiplier;
        }
        else
        {
            _agent.speed = startSpeed;
            SeeingPlayer();
            SeeingDecoy();
        }
        if (isFrozen)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    private void SeeingPlayer()
    {


        float distanceBetweenTargets = Vector3.Distance(_agent.transform.position, dummyPlayer.transform.position);
        //Debug.Log(distanceBetweenTargets);
        if (distanceBetweenTargets < viewRadius && _ghostEating.isAlive)
        {
            Debug.DrawLine(transform.position, dummyPlayer.transform.position, Color.red);
            canSeePlayer = true;
            animator.SetInteger("HuntingState", 1);
            if (!isFrozen)
            {
                _agent.transform.rotation = Quaternion.Lerp(_agent.transform.rotation, Quaternion.LookRotation(dummyPlayer.transform.position - _agent.transform.position), _agent.angularSpeed * Time.deltaTime);
                FollowPlayer();
            }
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

        if (canSeePlayer && !audioSource1.isPlaying && isRoaming)
        {
            audioSource2.Stop();    
            //audioSource1.clip = enemyHunting;
            audioSource1.PlayOneShot(enemyHunting);
            isRoaming = false;
        }

        else if (!canSeePlayer && !audioSource2.isPlaying && !isFrozen)
        {
            isRoaming = true;
            audioSource1.Stop();
            audioSource2.clip = enemySobbing;
            audioSource2.Play();
            audioSource2.loop = true;
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
        Quaternion lookRotation =
            Quaternion.LookRotation(points[nextPoint].transform.position - _agent.transform.position);
        viewRadius = startRadius;
        _agent.speed = startSpeed;
        //_agent.speed = Mathf.Lerp(startSpeed, _agent.velocity.magnitude, Time.deltaTime);
        float distanceBetween = Vector3.Distance(_agent.transform.position, points[nextPoint].transform.position);
        if (distanceBetween < 1.5f)
        {
            nextPoint = nextPoint != 2 ? nextPoint + 1 : 0;
            //StartCoroutine(wait());
        }

        _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, lookRotation, _agent.angularSpeed * Time.deltaTime);
        _agent.destination = points[nextPoint].position;

    }

    private void FollowPlayer()
    {
        _agent.speed = startSpeed * speedMultiplier;
        //_agent.speed = Mathf.Lerp(startSpeed * speedMultiplier, _agent.velocity.magnitude, Time.deltaTime);
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
            _agent.speed = startSpeed;

            Pathing();

        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        _monsterSpawner.spawnedMonsters--;
        _ghostEating.ghostsEaten++;
    }

    IEnumerator wait()
    {
        _agent.isStopped = true;
        yield return new WaitForSeconds(2);
        _agent.isStopped = false;
    }
}
