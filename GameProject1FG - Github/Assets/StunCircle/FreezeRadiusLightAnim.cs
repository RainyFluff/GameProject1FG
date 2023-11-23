using System.Collections;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public float increaseTime = 0.5f; // Time to increase intensity (in seconds)
    public float decreaseTime = 1f;  // Time to decrease intensity (in seconds)

    private Light lampLight;
    private float initialIntensity;

    void Start()
    {
        lampLight = GetComponent<Light>();

        if (lampLight != null)
        {
            // Store the initial intensity of the lamp
            initialIntensity = lampLight.intensity;

            // Start the coroutine to handle intensity changes
            StartCoroutine(ChangeIntensity());
        }
        else
        {
            Debug.LogError("LampController script requires a Light component on the GameObject.");
        }
    }

    IEnumerator ChangeIntensity()
    {
        // Increase intensity over 'increaseTime' seconds
        float elapsedTime = 0f;
        while (elapsedTime < increaseTime)
        {
            elapsedTime += Time.deltaTime;
            lampLight.intensity = Mathf.Lerp(0f, initialIntensity, elapsedTime / increaseTime);
            yield return null;
        }

        // Decrease intensity over 'decreaseTime' seconds
        elapsedTime = 0f;
        while (elapsedTime < decreaseTime)
        {
            elapsedTime += Time.deltaTime;
            lampLight.intensity = Mathf.Lerp(initialIntensity, 0f, elapsedTime / decreaseTime);
            yield return null;
        }

        // Ensure the intensity is set to 0 after decreasing
        lampLight.intensity = 0f;
    }
}
