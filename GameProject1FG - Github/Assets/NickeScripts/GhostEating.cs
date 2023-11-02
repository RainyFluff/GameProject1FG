using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostEating : MonoBehaviour
{
    private RaycastHit hit;
    public bool isHunting;
    public bool isAlive;
    public int flamesRequiredToEat = 5;
    public float huntingModeSpeed = 1.4f;
    public float huntingTime = 5;
    public int ghostsEaten;
    private AIPathing aiScript;
    public int ghostDamage = 5;
    public float ghostHuntingRange = 2;
    private ReworkedMovement movementScript;
    private FlameSpawning flameSpawning;
    private Animator animator;
    private Rigidbody rb;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Light pointLight;
    [SerializeField] private Light spotLight;
    [SerializeField] private float lightIntensityMultiplier = 2;
    [SerializeField] private float lightRangeMultiplier = 2;
    [SerializeField] private ParticleSystem huntingParticles;
    [SerializeField] private MonsterSpawner _monsterSpawner;
    private Animator enemyAnimator;
    private GameObject gmRef;

    private AudioSource _audioSource;

    [Header("Audio clips")]
    [SerializeField] private AudioClip _huntedSound;
    [SerializeField] private AudioClip[] _playerDamageSounds;
    [SerializeField] private AudioClip _afterChaseSound;
    [SerializeField] private AudioClip[] _ghostScreamSounds;

    [Header("Audio settings")]
    [SerializeField] private float _huntingSoundVolume = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movementScript = GetComponent<ReworkedMovement>();
        flameSpawning = GetComponent<FlameSpawning>();
        _audioSource = GetComponent<AudioSource>();

        isAlive = true;
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
        if (isHunting && rb.velocity.magnitude > 0)
        {
            animator.SetBool("IsHunting", true);
        }
        else if (!isHunting)
        {
            animator.SetBool("IsHunting", false);
        }
        
    }
    void ghosteating()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, ghostHuntingRange))
        {
            if (collider.tag == "Enemy")
            {
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, 0.01f);
                enemyAnimator = collider.gameObject.GetComponent<Animator>();
                if (_monsterSpawner.spawnedMonsterList.Contains(collider.gameObject))
                {
                    _monsterSpawner.spawnedMonsterList.Remove(collider.gameObject);
                }
                int randomNum = Random.Range(0, _ghostScreamSounds.Length);
                _audioSource.PlayOneShot(_ghostScreamSounds[randomNum]);
                ghostsEaten++;
                _monsterSpawner.spawnedMonsters--;
                StartCoroutine(GhostDeath());
                Destroy(collider.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        aiScript = other.collider.GetComponent<AIPathing>();
        if (other.transform.tag == "Enemy" && !aiScript.isFrozen && !isHunting)
        {
            ghostsEaten -= ghostDamage;
            if (ghostsEaten < 0)
            {
                isAlive = false;
                animator.SetBool("IsDead", true);
                deathScreen.SetActive(true);
                Destroy(other.gameObject);
            }
            else if (ghostsEaten >= 0)
            {
                int randomNum = Random.Range(0, _playerDamageSounds.Length);
                _audioSource.PlayOneShot(_playerDamageSounds[randomNum]);
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator HuntingMode()
    {
        _audioSource.clip = _huntedSound;
        _audioSource.volume = _huntingSoundVolume;
        _audioSource.loop = true;
        _audioSource.Play();
        movementScript.speed = movementScript.speed * huntingModeSpeed;
        movementScript.StopMovementSound();
        movementScript._isRunning = true;
        isHunting = true;
        flameSpawning.flameNumber -= flamesRequiredToEat;
        //pointLight.intensity = pointLight.intensity * lightIntensityMultiplier;
        //pointLight.range = pointLight.range * lightRangeMultiplier;
       // spotLight.intensity = spotLight.intensity * lightIntensityMultiplier;
       // spotLight.range = spotLight.range * lightRangeMultiplier;
        huntingParticles.Play();
        yield return new WaitForSeconds(huntingTime);
        isHunting = false;
        _audioSource.Stop();
        _audioSource.volume = 1f;
        movementScript.speed = movementScript.speed / huntingModeSpeed;
        movementScript._isRunning = false;
        movementScript.StopMovementSound();
       // pointLight.intensity = pointLight.intensity / lightIntensityMultiplier;
       // pointLight.range = pointLight.range / lightRangeMultiplier;
       // spotLight.intensity = spotLight.intensity / lightIntensityMultiplier;
      //  spotLight.range = spotLight.range / lightRangeMultiplier;
        huntingParticles.Stop();
        flameSpawning.SpawnFlame();
        _audioSource.PlayOneShot(_afterChaseSound);
    }

    IEnumerator GhostDeath()
    {
        Debug.Log("cour started");
        enemyAnimator.SetBool("HasDied",true);
        yield return new WaitForSeconds(1);
    }
}
