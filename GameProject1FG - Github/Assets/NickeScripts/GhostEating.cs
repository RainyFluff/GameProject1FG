using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private MonsterSpawner _monsterSpawner;

    private AudioSource _audioSource;

    [Header("Audio clips")]
    [SerializeField] private AudioClip _huntedSound;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip[] _ghostScreamSounds;

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
                Debug.Log("EnemyHit");

                if (_monsterSpawner.spawnedMonsterList.Contains(collider.gameObject))
                {
                    _monsterSpawner.spawnedMonsterList.Remove(collider.gameObject);
                }
               
                Destroy(collider.gameObject);
                int randomNum = Random.Range(0, _ghostScreamSounds.Length);
                _audioSource.PlayOneShot(_ghostScreamSounds[randomNum]);
                ghostsEaten++;
                _monsterSpawner.spawnedMonsters--;

            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        aiScript = other.collider.GetComponent<AIPathing>();
        if (other.transform.tag == "Enemy" && !aiScript.isFrozen && !isHunting)
        {
            Debug.Log("Collided");
            ghostsEaten -= ghostDamage;
            _audioSource.PlayOneShot(_damageSound);
            if (ghostsEaten < 0)
            {
                deathScreen.SetActive(true);
                Destroy(gameObject);
            }
            else if (ghostsEaten > 0)
            {
                Destroy(other.gameObject);
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
        yield return new WaitForSeconds(huntingTime);
        isHunting = false;
        _audioSource.Stop();
        movementScript.speed = movementScript.speed / huntingModeSpeed;
        pointLight.intensity = pointLight.intensity / lightIntensityMultiplier;
        pointLight.range = pointLight.range / lightRangeMultiplier;
        spotLight.intensity = spotLight.intensity / lightIntensityMultiplier;
        spotLight.range = spotLight.range / lightRangeMultiplier;
        huntingParticles.Stop();
        flameSpawning.SpawnFlame();
    }
}
