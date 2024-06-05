using UnityEngine;

public abstract class NPCBaseState : MonoBehaviour
{
    public abstract void EnterState(AIPeopleStateManager state);
    
    public virtual void UpdateState(AIPeopleStateManager state) { }

    public virtual void ExitState(AIPeopleStateManager state) { }

    public virtual void OnStateTriggerEnter(AIPeopleStateManager animal, Collider aiCollider) { }
}
