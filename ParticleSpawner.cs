using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public ParticleSystem particlePrefab; // Assign this in the Inspector
    private ParticleSystem spawnedParticle;

    private void OnEnable()
    {
        if (particlePrefab != null)
        {
            // Instantiate the particle system as a child of this object
            spawnedParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity, transform);
            spawnedParticle.Play();

            // Destroy after 5 seconds
            Destroy(spawnedParticle.gameObject, 5f);
        }
        else
        {
            Debug.LogWarning("ParticlePrefab is not assigned.");
        }
    }
}
