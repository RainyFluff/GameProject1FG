using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostFreeze : MonoBehaviour
{
    [SerializeField] private float freezeRange = 6;
    [SerializeField] private int freezeCost = 2;
    public float freezeCooldown = 60;
    [SerializeField] private float freezeDuration = 10;
    private GhostEating ghostEating;
    public bool canFreezeGhost;
    private float time;
    private AIPathing aiScript;
    
    // Start is called before the first frame update
    void Start()
    {
        ghostEating = GetComponent<GhostEating>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= Time.time)
        {
            canFreezeGhost = true;
            if (Input.GetKeyDown(KeyCode.Space) && ghostEating.ghostsEaten >= 2 && canFreezeGhost)
            {
                ghostEating.ghostsEaten -= freezeCost;
                time = Time.time + freezeCooldown;
                foreach (Collider enemy in Physics.OverlapSphere(transform.position, freezeRange))
                {
                    if (enemy.transform.tag == "Enemy")
                    {
                        //enemy cant damage us (slight change in "GhostEating" script required)
                        StartCoroutine(GhostFrozen(enemy));
                    }
                }
            }
        }
        else
        {
            canFreezeGhost = false;
        }
    }

    IEnumerator GhostFrozen(Collider enemy)
    {
        enemy.GetComponent<NavMeshAgent>().speed = 0;
        aiScript = enemy.GetComponent<AIPathing>();
        aiScript.isFrozen = true;
        yield return new WaitForSeconds(freezeDuration);
        enemy.GetComponent<NavMeshAgent>().speed = 3.5f; // Original value
        aiScript.isFrozen = false;
    }
}
