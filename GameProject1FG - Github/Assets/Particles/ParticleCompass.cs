using UnityEngine;

public class ParticleCompass : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private Transform target;

    public float minSpeed = 2f;
    public float maxSpeed = 3f;
    public float maxRandomAngle = 5f;
    public float endProbability = 0.1f; // Probability of spawning a particle per frame
    public float startProbability = 1f;
    private float spawnProbability = 1f;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        target = FindTargetObject();
        spawnProbability = startProbability;
    }

    void Update()
    {
        GameObject playerCharacter = transform.root.gameObject;

        GhostEating ghostEatingScript = playerCharacter.GetComponent<GhostEating>();

        if (target == null)
        {
            target = FindTargetObject();
        }

        if (ghostEatingScript != null && !ghostEatingScript.isHunting && Input.GetKey(KeyCode.LeftControl))
        {
            TryEmitParticle();
        }

        else
        {
            spawnProbability = startProbability;
        }
    }

    void TryEmitParticle()
    {
        if (Random.value < spawnProbability)
        {
            spawnProbability = endProbability;
            EmitParticle();
        }
    }

    void EmitParticle()
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

        if (target != null)
        {
            Vector3 baseDirection = (target.position - transform.position).normalized;
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(-maxRandomAngle, maxRandomAngle), 0f);
            Vector3 randomDirection = randomRotation * baseDirection;
            float randomSpeed = Random.Range(minSpeed, maxSpeed);

            emitParams.velocity = randomDirection * randomSpeed;
            particleSystem.Emit(emitParams, 1);
        }
    }

    Transform FindTargetObject()
    {
        return GameObject.FindWithTag("Flame")?.transform;
    }
}
