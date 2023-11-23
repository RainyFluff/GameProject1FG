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
    public int health = 3;
    private AIPathing aiScript;
    public int ghostDamage = 5;
    public float ghostHuntingRange = 2;
    private ReworkedMovement movementScript;
    private FlameSpawning flameSpawning;
    private Animator animator;
    private Rigidbody rb;
    [SerializeField] private GameObject deathScreen;
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
    [SerializeField] private AudioClip _playerDeathScream;
    [SerializeField] private AudioClip _loseSound;

    [Header("Audio settings")]
    [SerializeField] private float _huntingSoundVolume = 0.5f;

    [Header("Main objective")]
    [SerializeField] private GameObject _mainpiece;

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

        if (_mainpiece.GetComponent<EndObjectiveObj>()._gameWin)
        {
            isHunting = false;
            _audioSource.Stop();
            _audioSource.volume = 1f;
            movementScript.speed = movementScript.speed / huntingModeSpeed;
            movementScript._isRunning = false;
            movementScript.StopMovementSound();
            huntingParticles.Stop();
        }
    }

    void ghosteating()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, ghostHuntingRange))
        {
            if (collider.tag == "Enemy")
            {
                //collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, 0.01f);
                enemyAnimator = collider.gameObject.GetComponent<Animator>();
                if (_monsterSpawner.spawnedMonsterList.Contains(collider.gameObject))
                {
                    int randomNum = Random.Range(0, _ghostScreamSounds.Length);
                    _audioSource.PlayOneShot(_ghostScreamSounds[randomNum]);
                    _monsterSpawner.spawnedMonsterList.Remove(collider.gameObject);
                }
                collider.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                enemyAnimator.SetBool("HasDied", true);
                //Destroy(collider.gameObject);
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        aiScript = other.collider.GetComponent<AIPathing>();
        if (other.transform.tag == "Enemy" && !aiScript.isFrozen && !isHunting)
        {
            //Debug.LogWarning(health);
            health -= ghostDamage;
            if (health == 0)
            {
                isAlive = false;
                animator.SetBool("IsDead", true);
                StartCoroutine(PlayerDeath());
                _audioSource.PlayOneShot(_playerDeathScream);
                _audioSource.PlayOneShot(_loseSound);
                Destroy(other.gameObject);
            }
            else if (health > 0)
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
        huntingParticles.Play();
        yield return new WaitForSeconds(huntingTime);
        isHunting = false;
        _audioSource.Stop();
        _audioSource.volume = 1f;
        movementScript.speed = movementScript.speed / huntingModeSpeed;
        movementScript._isRunning = false;
        movementScript.StopMovementSound();
        huntingParticles.Stop();
        flameSpawning.SpawnFlame();
        _audioSource.PlayOneShot(_afterChaseSound);
    }
    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(3.2f);
        deathScreen.SetActive(true);
    }
}
