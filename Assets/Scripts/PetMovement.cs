using UnityEngine;
using UnityEngine.AI;

public class PetMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    public float desiredDistance = 1.25f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = desiredDistance;
    }
    void Update()
    {
        if (player != null) {
            navMeshAgent.SetDestination(player.position);
        }
    }
}