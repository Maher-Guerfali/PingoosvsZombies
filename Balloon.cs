using UnityEngine;
using System.Collections.Generic;

public class Balloon : MonoBehaviour
{
    [Header("Pop Effect Settings")]
    public ParticleSystem popEffect; // The particle system to play on explosion

    [Header("GameObject Activation")]
    public List<GameObject> activatableObjects; // List of GameObjects to randomly activate
    private GameObject activeObject; // The currently active GameObject

    [Header("Mesh and Material Settings")]
    public MeshRenderer balloonMeshRenderer; // The MeshRenderer to change material
    public MeshRenderer balloon2MeshRenderer; // The MeshRenderer to change material
    public List<Material> materials; // List of materials to randomly assign

    [Header("Animator Settings")]
    public Animator animator; // Reference to the Animator component
    private static readonly int DeathBool = Animator.StringToHash("death"); // Animator parameter name

    private Collider balloonCollider; // Reference to the balloon's collider
    public bool hasExploded = false; // Tracks if the balloon has already exploded
    public AudioSource vfxsounder;
    public AudioSource vfxsounder2;
    public AudioClip deadzombiesound;
    public AudioClip popsoiund;
    public AudioClip blukhsound;
    public AudioClip attacksound;
    void Awake()
    {
        // Cache the balloon's collider
        balloonCollider = GetComponent<Collider>();
    }



    /// <summary>
    /// Triggered when the balloon collides with another object.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            // Get the contact point of the collision
            Vector3 contactPoint = collision.contacts[0].point;

            // Get the balloon collider bounds
            Collider balloonCollider = GetComponent<Collider>();
            float balloonCenterY = balloonCollider.bounds.center.y;
            float balloonHeight = balloonCollider.bounds.size.y;

            // Calculate upper and lower half thresholds
            float upperThreshold = balloonCenterY + (balloonHeight / 4); // Upper half starts here
            float lowerThreshold = balloonCenterY - (balloonHeight / 4); // Lower half ends here

            // Check if collision occurred in the upper or lower half of the balloon
            bool isUpperHalf = contactPoint.y >= upperThreshold;
            bool isLowerHalf = contactPoint.y <= lowerThreshold;

            // Check collision with Missile
            if (collision.gameObject.CompareTag("Missile"))
            {
                PopBalloon();

            }

            // Check collision with Player
            Player player = collision.gameObject.GetComponent<Player>();
            PlayerStats playerstats = collision.gameObject.GetComponent<PlayerStats>();
            if (collision.gameObject.CompareTag("Player") && player != null && playerstats !=null)
            {
                if (isUpperHalf)
                {
                    player.ApplyLiftForce(6f);
                    vfxsounder.PlayOneShot(popsoiund);
                    playerstats.AddBalloonPop();
                    playerstats.AddZombieKill();
                    PopBalloon();
                }
                else if (isLowerHalf)
                {
                    if (player.slide==true)
                    {
                       
                        PopBalloon();

                    }
                    else
                    {
                        
                        vfxsounder.PlayOneShot(attacksound);
                        player.TakeDamage();
                    }
                   
                }
                else
                {
                    
                    vfxsounder2.PlayOneShot(deadzombiesound);
                    player.ApplyLiftForce(6f);
                    playerstats.AddBalloonPop();
                    playerstats.AddZombieKill();
                    vfxsounder.PlayOneShot(popsoiund);
                    PopBalloon();
                }
            }
        }
    }

    /// <summary>
    /// Handles the balloon popping behavior.
    /// </summary>
    public void PopBalloon()
    {
        hasExploded = true; // Mark balloon as exploded

        // Play the pop effect if assigned
        if (popEffect != null)
        {
            popEffect.Play();
        }

        // Trigger the death animation
        if (animator != null)
        {
            animator.SetBool(DeathBool, true);
        }

        // Hide the balloon mesh
        if (balloonMeshRenderer != null)
        {
            balloonMeshRenderer.enabled = false;
            balloon2MeshRenderer.enabled = false;
        }

        // Disable the collider to prevent further interactions
        balloonCollider.enabled = false;

        // Optionally, disable the active object (if any)
        //if (activeObject != null)
        //{
          //  activeObject.SetActive(false);
        //}

//        Debug.Log("Balloon popped!");
    }

    /// <summary>
    /// Called when the balloon is re-enabled (for pooling).
    /// </summary>
    private void OnEnable()
    {
        ResetBalloon();
    }

    /// <summary>
    /// Resets the balloon state for reuse.
    /// </summary>
    private void ResetBalloon()
    {
        hasExploded = false; // Reset exploded state

        // Reset the collider
        if (balloonCollider != null)
        {
            balloonCollider.enabled = true;
        }

        // Reset the mesh renderer
        if (balloonMeshRenderer != null)
        {
            balloonMeshRenderer.enabled = true;
            balloon2MeshRenderer.enabled = true;
        }

        // Reset the particle system
        if (popEffect != null)
        {
            popEffect.Stop();
            popEffect.Clear();
        }

        // Reset the animator parameter
        if (animator != null)
        {
            animator.SetBool(DeathBool, false);
        }

        // Enable one random GameObject from the list
        ActivateRandomObject();

        // Assign a random material to the MeshRenderer
        AssignRandomMaterial();

    }

    /// <summary>
    /// Enables one random GameObject from the list.
    /// </summary>
    private void ActivateRandomObject()
    {
        // Disable the currently active object
        if (activeObject != null)
        {
            activeObject.SetActive(false);
        }

        // Enable a random GameObject from the list
        if (activatableObjects != null && activatableObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, activatableObjects.Count);
            activeObject = activatableObjects[randomIndex];
            activeObject.SetActive(true);
        }
    }

    /// <summary>
    /// Assigns a random material to the balloon's MeshRenderer.
    /// </summary>
    /// 
    private void AssignRandomMaterial()
    {
        if (balloonMeshRenderer != null && materials != null && materials.Count > 0)
        {
            int randomIndex = Random.Range(0, materials.Count);
            balloonMeshRenderer.material = materials[randomIndex];
        }
    }
}
