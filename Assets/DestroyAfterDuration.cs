using System.Collections;
using UnityEngine;

public class DestroyAfterDuration : MonoBehaviour
{
    public float duration = 3f; // Set the duration in seconds

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterDelay(duration));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destroy the game object after the specified duration
        Destroy(gameObject);
    }
}
