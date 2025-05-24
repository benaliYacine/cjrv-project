using UnityEngine;
using UnityEngine.AI;

public class NpcMove : MonoBehaviour
{
    public Transform target; // Assign this in the Inspector
    private NavMeshAgent agent;
    private Animator animator; // Add Animator component reference

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Initialize the Animator component
        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndGoal"))
        {
            // Stop moving and clear destination
            agent.isStopped = true;
            agent.ResetPath();  // This will clear the current path/destination

            // Optional: Switch to idle animation
            if (animator != null)
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}
