using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform partToRotate; // Assign the front part of the car in the Inspector
    NavMeshAgent agent;
    public float rotationSpeed = 5f;
    public float speed = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (partToRotate == null)
        {
            Debug.LogError("Part to Rotate not assigned! Please assign it in the Inspector.");
        }
    }

    private void Update()
    {
        // Set destination for NavMeshAgent
        agent.SetDestination(target.position);

        // Calculate direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Calculate angle in degrees along the Z-axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation for the entire object
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), rotationSpeed * Time.deltaTime);

        // Set the rotation for the specific part to rotate (front of the car)
        if (partToRotate != null)
        {
            partToRotate.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
