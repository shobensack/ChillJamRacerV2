using UnityEngine;
using UnityEngine.AI;

public class EnemyCarController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform target;  // Assign the player's transform as the target

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            Debug.LogError("Target not assigned for EnemyCarController!");
        }
        else
        {
            SetDestination();
        }
    }

    void SetDestination()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }
}
