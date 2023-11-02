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
    public bool canFreezeGhost = true;
    private float time;
    private AIPathing aiScript;
    private GameObject freezeRadiusIndication;
    private UICounters UIcounters;
    
    // Start is called before the first frame update
    void Start()
    {
        ghostEating = GetComponent<GhostEating>();
        UIcounters = GameObject.Find("Canvas").GetComponent<UICounters>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= Time.time)
        {
            canFreezeGhost = true;
            if (Input.GetKeyDown(KeyCode.Space) && canFreezeGhost)
            {
                StartCoroutine(FreezeCooldown());
                time = Time.time + freezeCooldown;
                freezeRadiusIndication = (GameObject)Instantiate(Resources.Load("FreezeRadius"),transform.position, Quaternion.Euler(0,0,0));
                freezeRadiusIndication.transform.localScale = new Vector3(freezeRange, 0.2f, freezeRange);
                StartCoroutine(DestroyFreezeIndicator());
                foreach (Collider enemy in Physics.OverlapSphere(transform.position, freezeRange))
                {
                    if (enemy.transform.tag == "Enemy")
                    {
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

    IEnumerator FreezeCooldown()
    {
        yield return StartCoroutine(UIcounters.FreezeCountdownCoroutine());
    }

    IEnumerator GhostFrozen(Collider enemy)
    {
        enemy.GetComponent<NavMeshAgent>().isStopped = true;
        aiScript = enemy.GetComponent<AIPathing>();
        aiScript.isFrozen = true;
        yield return new WaitForSeconds(freezeDuration);
        enemy.GetComponent<NavMeshAgent>().isStopped = false;
        aiScript.isFrozen = false;
    }

    IEnumerator DestroyFreezeIndicator()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(freezeRadiusIndication);
    }
}
