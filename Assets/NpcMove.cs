using UnityEngine;
using UnityEngine.AI;

public class NpcMove : MonoBehaviour
{
    public Transform target; // Assign this in the Inspector
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
