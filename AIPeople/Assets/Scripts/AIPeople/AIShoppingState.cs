using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIShoppingState : NPCBaseState
{
    [SerializeField] private List<string> shoppingAnimNames;
    [SerializeField] private int minShoppingTime = 10;
    [SerializeField] private int maxShoppingTime = 20;

    private float _timer;
    private float _timerStopDuration;
    
    private string _lastSelectedItem = "";
    
    private AIPeopleStateManager _aiPeopleStateManager;
    public override void EnterState(AIPeopleStateManager aiState)
    {
        aiState.DebugLog("Entering shopping State");
        _aiPeopleStateManager = aiState;
        _timerStopDuration = Random.Range(minShoppingTime, maxShoppingTime);
        aiState.presentAIPeopleAction = AIPeopleActions.Shopping;
        aiState.PlayAnimation(shoppingAnimNames[Random.Range(0, shoppingAnimNames.Count)]);
    }

    public override void UpdateState(AIPeopleStateManager aiState)
    {
        _timer += Time.deltaTime;
        if (_timer >= _timerStopDuration)
        {
            aiState.SwitchState(aiState.walkingState);
        }
    }

    public override void ExitState(AIPeopleStateManager aiState)
    {
        _timer = 0;
        _timerStopDuration = 0;
        aiState.DebugLog("Exiting Shopping State");
    }

    public override void OnStateTriggerEnter(AIPeopleStateManager aiState, Collider aiCollider)
    {
        base.OnStateTriggerEnter(aiState, aiCollider);
    }

    /// <summary>
    /// Animation Event Method
    /// </summary>
    public void TalkingOver()
    {
        _aiPeopleStateManager.DebugLog("Animation switching");
        _aiPeopleStateManager.DebugLog($"Now time: {_timer}");
        if (_timer < _timerStopDuration)
        {
            _aiPeopleStateManager.PlayAnimation(GetRandomItem());
        }
    }
    
    private string GetRandomItem()
    {
        if (shoppingAnimNames.Count <= 1)
        {
            Debug.LogWarning("List must contain at least two items to ensure a different item is selected.");
            return shoppingAnimNames.Count == 1 ? shoppingAnimNames[0] : "";
        }

        string selectedItem;
        do
        {
            var randomIndex = Random.Range(0, shoppingAnimNames.Count);
            selectedItem = shoppingAnimNames[randomIndex];
        } while (selectedItem == _lastSelectedItem);

        _lastSelectedItem = selectedItem;
        return selectedItem;
    }
}
