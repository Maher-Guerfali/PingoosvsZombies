using UnityEngine;
using System.Collections;

public class AutoToggleObject : MonoBehaviour
{
    [Header("Time Range Settings")]
    [Tooltip("Minimum and maximum time the object stays deactivated.")]
    public float minDeactivateTime = 2f;
    public float maxDeactivateTime = 8f;

    [Tooltip("Minimum and maximum time the object stays activated.")]
    public float minActivateTime = 4f;
    public float maxActivateTime = 20f;

    private void Start()
    {
        StartCoroutine(ToggleObjectLoop());
    }

    private IEnumerator ToggleObjectLoop()
    {
        while (true)
        {
            // Randomly deactivate the object
            gameObject.SetActive(false);
            float deactivateDuration = Random.Range(minDeactivateTime, maxDeactivateTime);
            yield return new WaitForSeconds(deactivateDuration);

            // Randomly activate the object
            gameObject.SetActive(true);
            float activateDuration = Random.Range(minActivateTime, maxActivateTime);
            yield return new WaitForSeconds(activateDuration);
        }
    }
}
