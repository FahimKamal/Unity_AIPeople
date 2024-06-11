using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIFarmer : MonoBehaviour
{
    [SerializeField] private AIWaypoints waypoints;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private GameObject waterCan;
    
    private WayPointKnot currentWayPoint;
    private Vector3  currentDestination;

    private const string Walking = "Walking";
    private const string Watering = "Watering";
    private const string Planting = "Kneeling Down";

    [SerializeField] private string currentAnimName = "";
    
    private bool isWalking;
    private void Start()
    {
        waterCan.SetActive(false);
        DoFarming();
    }

    private void DoFarming()
    {
        
        var destination = waypoints.GetRandomWayPoint();
        
        if (currentWayPoint != null)
        {
            currentWayPoint.isKnotSelected = false;
        }
        
        currentWayPoint = destination.selectedWaypointKnot;
        currentWayPoint.isKnotSelected = true;
        currentDestination = destination.positionValue;
        agent.SetDestination(currentDestination);
        PlayAnimation(Walking);
        isWalking = true;
    }

    private void DoWatering()
    {
        Debug.Log("Doing watering");
        waterCan.SetActive(true);
        PlayAnimation(Watering);
    }
    
    private void DoPlanting()
    {
        Debug.Log("Doing planting");
        PlayAnimation(Planting);
    }

    private void Update()
    {
        if (!isWalking)
        {
            return;
        }
        var remainingDistance = Vector3.Distance(transform.position, currentDestination);
        if (remainingDistance < 1f && isWalking)
        {
            isWalking = false;
            Random.InitState(System.DateTime.Now.Millisecond);
            var choice = Random.Range(0, 2);
            if (choice == 0)
            {
                DoPlanting();
                // DoWatering();
            }
            else
            {
                // DoPlanting();
                DoWatering();
            }
        }
    }

    // Animation Event Functions 
    public void WateringDone()
    {
        // currentWayPoint.isKnotSelected = false;
        waterCan.SetActive(false);
        DoFarming();
    }

    public void PlantingDone()
    {
        // currentWayPoint.isKnotSelected = false;
        DoFarming();
    }
    
    /// <summary>
    /// Internal method to play animation by the farmer animator component. 
    /// </summary>
    /// <param name="newState">Next animation state to play.</param>
    private void PlayAnimation(string newState)
    {
        // If the given animation state is already playing, do nothing. 
        if (currentAnimName == newState) return;
        
        currentAnimName = newState;
        animator.CrossFade(newState, 0.08f);
    }
}
