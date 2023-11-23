using System.Collections;
using UnityEngine;

public class IcicleController : MonoBehaviour
{
    private float freezeDuration = 1f;
    public Animator icicleAnimator;

    private void Start()
    {
        GhostFreeze ghostFreezeScript = FindObjectOfType<GhostFreeze>();
        freezeDuration = ghostFreezeScript.freezeDuration - 3.5f;
        icicleAnimator = GetComponent<Animator>();
        StartCoroutine(PlayAnimationSequence());
    }

    private IEnumerator PlayAnimationSequence()
    {
        // Wait for the freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Play the animation backward
        icicleAnimator.SetTrigger("ReverseTrigger");
    }
}
