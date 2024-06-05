using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : NPCBaseState
{
    public override void EnterState(AIPeopleStateManager state)
    {
        
    }

    public override void UpdateState(AIPeopleStateManager state)
    {
        base.UpdateState(state);
    }

    public override void ExitState(AIPeopleStateManager state)
    {
        base.ExitState(state);
    }

    public override void OnStateTriggerEnter(AIPeopleStateManager animal, Collider aiCollider)
    {
        base.OnStateTriggerEnter(animal, aiCollider);
    }
}
