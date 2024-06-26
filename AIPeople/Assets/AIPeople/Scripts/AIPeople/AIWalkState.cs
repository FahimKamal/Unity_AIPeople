using Unity.Mathematics;
using UnityEngine;

public class AIWalkState : NPCBaseState
{
    private AIAction _nextAIAction;
    private float3 _aiDestination;
    private Quaternion  _rotation;
    public override void EnterState(AIPeopleStateManager aiState)
    {
        aiState.DebugLog("Entering walk State");
        aiState.presentAIPeopleAction = AIPeopleActions.Walking;

        if (aiState.presentAIPeopleAction == AIPeopleActions.Walking)
        {
            var destination = aiState.AIWaypoints.GetRandomWayPoint();
            _aiDestination = destination.positionValue;
            _nextAIAction = destination.action;
            _rotation = destination.rotationValue;

            if (aiState.selectedWayPointKnot != null)
            {
                aiState.selectedWayPointKnot.isKnotSelected = false;
            }

            aiState.selectedWayPointKnot = destination.selectedWaypointKnot;
            
            aiState.agent.SetDestination(_aiDestination);
            aiState.PlayAnimation(AIPeopleStateManager.AnimationName.Walking);
        }
    }

    public override void UpdateState(AIPeopleStateManager aiState)
    {
        var remainingDistance = Vector3.Distance(aiState.transform.position, _aiDestination);
        if (!(remainingDistance <= aiState.agent.stoppingDistance) ||
            aiState.presentAIPeopleAction != AIPeopleActions.Walking) return;
        
        switch (_nextAIAction)
        {
            case AIAction.None:
                aiState.SwitchState(aiState.idleState);
                break;
            case AIAction.Idle:
                aiState.SwitchState(aiState.idleState);
                break;
            case AIAction.Shopping:
                transform.rotation = _rotation;
                aiState.SwitchState(aiState.shoppingState);
                break;
            case AIAction.Farming:
                aiState.SwitchState(aiState.idleState);
                break;
            case AIAction.Fighting:
                aiState.SwitchState(aiState.fightingState);
                break;
            default:
                break;
        }
    }

    public override void ExitState(AIPeopleStateManager aiState)
    {
        aiState.DebugLog("Exiting Walking State");
        _nextAIAction = AIAction.None;
    }

    public override void OnStateTriggerEnter(AIPeopleStateManager aiState, Collider aiCollider)
    {
        base.OnStateTriggerEnter(aiState, aiCollider);
    }
}
