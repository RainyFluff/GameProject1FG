using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFreeze : MonoBehaviour
{
    [SerializeField] private float freezeRange = 6;
    [SerializeField] private int freezeCost = 2;
    [SerializeField] private float freezeCooldown = 60;
    [SerializeField] private float freezeDuration = 10;
    private GhostEating ghostEating;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        ghostEating = GetComponent<GhostEating>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ghostEating.ghostsEaten >= 2 && time <= Time.time)
        {
            ghostEating.ghostsEaten -= freezeCost;
            time = Time.time + freezeCooldown;
            foreach (Collider enemy in Physics.OverlapSphere(transform.position, freezeRange))
            {
                if (enemy.transform.tag == "Enemy")
                {
                    Debug.Log("enemy frozen");
                    //enemy cant damage us
                    //enemy cant move
                    //enemy should still be able to collide with us(?) - requires change in how damage is calculated
                }
            }
        }
    }
}
