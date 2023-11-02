using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyAbility : MonoBehaviour
{
    public GameObject decoy;
    private float time;
    public float cooldown = 5;
    public bool canUseDecoy;
    [SerializeField] private int decoyCost = 2;
    [SerializeField] private float decoyLifetime = 5;

    [SerializeField] private UICounters uiScript;

    private GhostEating _ghostEating;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = cooldown + decoyLifetime;
        _ghostEating = GetComponent<GhostEating>();
        canUseDecoy = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && time <= Time.time && _ghostEating.ghostsEaten >= decoyCost)
        {
            canUseDecoy = false;
            StartCoroutine(uiScript.DecoyCountDown());
            time = Time.time + cooldown;
            SpawnDecoy();
            StartCoroutine(DestroyDecoy());
        }
    }

    void SpawnDecoy()
    {
       decoy = Instantiate((GameObject)Resources.Load("MCALPHADecoy"), transform.position,
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
       _ghostEating.ghostsEaten -= decoyCost;
    }

    IEnumerator DestroyDecoy()
    {
        yield return new WaitForSeconds(decoyLifetime);
        Destroy(decoy);
    }
}
