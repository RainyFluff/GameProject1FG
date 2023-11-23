using System.Collections;
using UnityEngine;

public class ShaderAnimator : MonoBehaviour
{
    public Material material;
    public float animationSpeed = 0.5f; // Adjust as needed
    private float currentProgress = 0;

    void Start()
    {
        material.SetFloat("_MaskSize", 0);
        material.SetFloat("_Alpha", 1);
        // Start the animation when the object spawns
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        while (currentProgress < 1.2f) // Fix the condition here
        {
            currentProgress += animationSpeed * Time.deltaTime;
            material.SetFloat("_MaskSize", currentProgress);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(EndAnimation());
    }

    IEnumerator EndAnimation()
    {
        currentProgress = 1f;
        while (currentProgress > 0)
        {
            currentProgress -= animationSpeed * Time.deltaTime;
            material.SetFloat("_Alpha", currentProgress);
            yield return null;
        }
    }
}
