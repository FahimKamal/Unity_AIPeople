using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIIdleState : NPCBaseState
{
    [SerializeField] private List<string> idleAnimNames;
    [SerializeField] private float maxStandTime = 2f;
    [SerializeField] private float minStandTime = 3f;
    private float _timer;
    private float _timerStopDuration;
    public override void EnterState(AIPeopleStateManager aiState)
    {
        _timerStopDuration = Random.Range(minStandTime, maxStandTime);
        aiState.DebugLog("Entering Idle State");
        aiState.presentAIPeopleAction = AIPeopleActions.Idle;
        aiState.PlayAnimation(idleAnimNames[Random.Range(0, idleAnimNames.Count)]);
    }

    public override void UpdateState(AIPeopleStateManager aiState)
    {
        _timer += Time.deltaTime;
        if (_timer > _timerStopDuration)
        {
            aiState.SwitchState(aiState.walkingState);
        }
    }

    public override void ExitState(AIPeopleStateManager aiState)
    {
        _timer = 0;
        _timerStopDuration = 0;
        aiState.DebugLog("Exiting Idle State");
    }

    public override void OnStateTriggerEnter(AIPeopleStateManager aiState, Collider aiCollider)
    {
        base.OnStateTriggerEnter(aiState, aiCollider);
    }
}
