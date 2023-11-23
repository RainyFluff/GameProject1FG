using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostFreeze : MonoBehaviour
{
    [SerializeField] private float freezeRange = 6;
    [SerializeField] private int freezeCost = 2;
    public float freezeCooldown = 60;
    public float freezeDuration = 10;
    private GhostEating ghostEating;
    public bool canFreezeGhost = true;
    private float time;
    private GameObject freezeRadiusIndication;
    private GameObject freezeIceEnemy;
    private UICounters UIcounters;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _stunSound;
    [SerializeField] private AudioClip _stunActivationSound;

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
                _audioSource.PlayOneShot(_stunActivationSound);
                StartCoroutine(FreezeCooldown());
                time = Time.time + freezeCooldown;
                freezeRadiusIndication = (GameObject)Instantiate(Resources.Load("FreezeRadius"),transform.position, Quaternion.Euler(0,0,0));
                freezeRadiusIndication.transform.localScale = new Vector3(freezeRange*2, 0.2f, freezeRange*2);
                StartCoroutine(DestroyFreezeIndicator());
                foreach (Collider enemy in Physics.OverlapSphere(transform.position, freezeRange))
                {
                    if (enemy.transform.tag == "Enemy")
                    {
                        _audioSource.PlayOneShot(_stunSound);
                        //Debug.LogError(Vector3.Distance(transform.position, enemy.transform.position));
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
        AIPathing aiScript;
        enemy.GetComponent<NavMeshAgent>().isStopped = true;
        enemy.GetComponent<Rigidbody>().isKinematic = true;
        enemy.GetComponent<NavMeshAgent>().acceleration = 100;
        enemy.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        aiScript = enemy.GetComponent<AIPathing>();
        aiScript.isFrozen = true;
        freezeIceEnemy = (GameObject)Instantiate(Resources.Load("IceCrystalEnemy"), enemy.transform.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(freezeDuration);
        Destroy(freezeIceEnemy);
        enemy.GetComponent<NavMeshAgent>().isStopped = false;
        enemy.GetComponent<Rigidbody>().isKinematic = false;
        enemy.GetComponent<NavMeshAgent>().acceleration = 8;
        enemy.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        aiScript.isFrozen = false;
    }

    IEnumerator DestroyFreezeIndicator()
    {
        yield return new WaitForSeconds(2f);
        Destroy(freezeRadiusIndication);
    }
}
