using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


public enum AIPeopleActions
{
    Idle, Walking, Shopping, Fighting
}

public class AIPeopleStateManager : MonoBehaviour
{
    public static class AnimationName
    {
        public const string Idle = "Idle";
        public const string Walking = "Walking";
    }
    public NavMeshAgent agent;
    public Animator animator;
    public AIPeopleActions presentAIPeopleAction;

    private NPCBaseState _presentState;
    public AIIdleState idleState;
    public AIWalkState walkingState;
    public AIShoppingState shoppingState;
    public AIFightingState fightingState;

    public AIWaypoints AIWaypoints;
    public WayPointKnot selectedWayPointKnot;
    
    private  bool _isBusy;
    [SerializeField] private string currentAnimName = AnimationName.Idle;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        idleState = GetComponent<AIIdleState>();
        walkingState = GetComponent<AIWalkState>();
        shoppingState = GetComponent<AIShoppingState>();
        fightingState = GetComponent<AIFightingState>();
        
        // Initial State and entering the state.
        presentAIPeopleAction = AIPeopleActions.Walking;
        _presentState = walkingState;
        _presentState.EnterState(this);
    }

    private void Update()
    {
        _presentState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        _presentState.OnStateTriggerEnter(this, other);
    }
    
    public void SwitchState(NPCBaseState state)
    {
        _presentState.ExitState(this);
        _presentState = state;
        _presentState.EnterState(this);
    }
    
    [SerializeField] private bool showDebugLog;

    public void DebugLog(string log)
    {
        if (showDebugLog)
        {
            Debug.Log(log);
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
