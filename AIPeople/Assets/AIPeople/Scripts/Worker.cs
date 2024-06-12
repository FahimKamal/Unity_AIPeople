using System;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    public const string Walking = "Walking";
    [SerializeField] private string currentAnimName;

    private Transform _workerDestination;
    
    public void GoLocation(Transform location)
    {
        _workerDestination = location;
        agent.SetDestination(_workerDestination.position);
        PlayAnimation(Walking);
    }

    private void Update()
    {
        // if (_workerDestination == null)
        // {
        //     return;
        //     Debug.LogError("Worker destination is null");
        // }
        // var remainingDistance = Vector3.Distance(transform.position, _workerDestination.position);
        // if (remainingDistance <= agent.stoppingDistance)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _workerDestination)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Internal method to play animation by the farmer animator component. 
    /// </summary>
    /// <param name="newState">Next animation state to play.</param>
    internal void PlayAnimation(string newState)
    {
        // If the given animation state is already playing, do nothing. 
        if (currentAnimName == newState) return;
        
        currentAnimName = newState;
        animator.CrossFade(newState, 0.08f);
    }
}
