using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEating : MonoBehaviour
{
    private RaycastHit hit;
    public bool isHunting;
    public int flamesRequiredToEat = 5;
    public float huntingModeSpeed = 1.4f;
    public float huntingTime = 5;
    public int ghostsEaten;
    private AIPathing aiScript;
    public int ghostDamage = 5;
    public float ghostHuntingRange = 2;
    private ReworkedMovement movementScript;
    private FlameSpawning flameSpawning;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Light pointLight;
    [SerializeField] private Light spotLight;
    [SerializeField] private float lightIntensityMultiplier = 2;
    [SerializeField] private float lightRangeMultiplier = 2;
    [SerializeField] private ParticleSystem huntingParticles;

    private AudioSource _audioSource;

    [Header("Audio clips")]
    [SerializeField] private AudioClip _huntedSound;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _soulCollectSound;
    //[SerializeField] private AudioClip _ghostDeathSound;

    void Start()
    {
        movementScript = GetComponent<ReworkedMovement>();
        flameSpawning = GetComponent<FlameSpawning>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flameSpawning.flameNumber >= flamesRequiredToEat && !isHunting)
        {
            StartCoroutine(HuntingMode());
        }
        if (isHunting)
        {
            ghosteating();
        }
    }
    void ghosteating()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, ghostHuntingRange))
        {
            if (collider.tag == "Enemy")
            {
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, 0.01f);
                if (Vector3.Distance(collider.transform.position, transform.position) < 1.5f)
                {
                    Destroy(collider.gameObject);
                    //_audioSource.PlayOneShot(_ghostDeathSound);
                   // yield return new WaitWhile(() => _audioSource.isPlaying);
                    _audioSource.PlayOneShot(_soulCollectSound);
                    ghostsEaten++;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        aiScript = other.collider.GetComponent<AIPathing>();
        if (other.transform.tag == "Enemy" && !aiScript.isFrozen)
        {
            ghostsEaten -= ghostDamage;
            _audioSource.PlayOneShot(_damageSound);
            if (ghostsEaten < 0)
            {
                deathScreen.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator HuntingMode()
    {
        _audioSource.clip = _huntedSound;
        _audioSource.loop = true;
        _audioSource.Play();
        movementScript.speed = movementScript.speed * huntingModeSpeed;
        isHunting = true;
        flameSpawning.flameNumber -= flamesRequiredToEat;
        pointLight.intensity = pointLight.intensity * lightIntensityMultiplier;
        pointLight.range = pointLight.range * lightRangeMultiplier;
        spotLight.intensity = spotLight.intensity * lightIntensityMultiplier;
        spotLight.range = spotLight.range * lightRangeMultiplier;
        huntingParticles.Play();
        yield return new WaitForSecondsRealtime(huntingTime);
        _audioSource.Stop();
        movementScript.speed = movementScript.speed / huntingModeSpeed;
        isHunting = false;
        pointLight.intensity = pointLight.intensity / lightIntensityMultiplier;
        pointLight.range = pointLight.range / lightRangeMultiplier;
        spotLight.intensity = spotLight.intensity / lightIntensityMultiplier;
        spotLight.range = spotLight.range / lightRangeMultiplier;
        huntingParticles.Stop();
    }
}
