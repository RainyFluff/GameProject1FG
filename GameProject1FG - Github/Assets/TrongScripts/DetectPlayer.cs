using System.Collections;

using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject dummyPlayer;
    [SerializeField] private float speed = 0f;
    
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    private bool _canSeePlayer;
    [SerializeField] private float radius;
    [Range(0,360)]
    [SerializeField] private float angle;

  public float Radius
    {
        get { return radius;  }
        set { radius = value; }
    }
    public float Angle
    {
        get { return angle;  }
        set { angle = value; }
    }
    public bool CanSeePlayer
    {
        get { return _canSeePlayer;  }
        set { _canSeePlayer = value; }
    }

    public GameObject DummyPlayer
    {
        get { return dummyPlayer; }
        set { dummyPlayer = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        //FOVRoutine();
    }

    // Update is called once per frame
    void Update()
    {
        //float stepSpeed = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, dummyPlayer.transform.position, stepSpeed);
        //Pathing();
        //FOVRoutine();
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOVCheck();
        }
    }
    
    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        float stepSpeed = speed * Time.deltaTime;
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    _canSeePlayer = true;
                    transform.position = Vector3.MoveTowards(transform.position, dummyPlayer.transform.position, stepSpeed);

                }
                
                else
                {
                    _canSeePlayer = false;

                }
                
            }
            else
            {
                _canSeePlayer = false;
            }
            
        }
        
        else if (_canSeePlayer)
        {
            _canSeePlayer = false;
        }
        
    }
    
}
