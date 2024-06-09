using UnityEngine;

public abstract class NPCBaseState : MonoBehaviour
{
    public abstract void EnterState(AIPeopleStateManager aiState);
    
    public virtual void UpdateState(AIPeopleStateManager aiState) { }

    public virtual void ExitState(AIPeopleStateManager aiState) { }

    public virtual void OnStateTriggerEnter(AIPeopleStateManager aiState, Collider aiCollider) { }
}
